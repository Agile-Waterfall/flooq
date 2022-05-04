import express from 'express'
import MorganMiddleware from './utils/logging/MorganMiddleware'
import versionRouter from './routes/VersionRouter'
import dataflowRouter from './routes/DataflowRouter'
import Logger from './utils/logging/Logger'
import 'dotenv/config'
import { Server } from 'http'
import { getPromBundleConfig } from './utils/MetricsCollector'

const metricsMiddleware = getPromBundleConfig()

const app = express()
app.disable( 'x-powered-by' )

app.use( metricsMiddleware )

app.use( MorganMiddleware )

app.get( '/status', ( _req: any, res: any ): void => res.send( 'running' ) )

app.use( '/version', versionRouter )
app.use( '/flow', dataflowRouter )

export let server: Server

if ( require.main === module ) {
  server = app.listen( 3500, '', () => {
    Logger.info( 'Started Webserver at http://localhost:3500' )
  } )
}

export default app
