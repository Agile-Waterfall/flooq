import express from 'express'
import MorganMiddleware from './utils/logging/MorganMiddleware'
import version from './routes/VersionRouter'
import Logger from './utils/logging/Logger'
import 'dotenv/config'


const app = express()
app.disable( 'x-powered-by' )

app.use( MorganMiddleware )

app.get( '/status', ( req: any, res: any ): void => {
  res.send( 'running' )
} )

app.use( '/version', version )

export const server = app.listen( 3500, '', () => {
  Logger.info( 'Started Webserver at http://localhost:3500' )
} )

export default app
