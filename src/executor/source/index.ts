import express from 'express'
import MorganMiddleware from './utils/logging/MorganMiddleware'
import version from './routes/VersionRouter'
import Logger from './utils/logging/Logger'
import dotenv from 'dotenv'

dotenv.config()

const app = express()
app.disable( 'x-powered-by' )

app.use( MorganMiddleware )

app.use( '/version', version )

app.listen( 3500, '', () => {
  Logger.info( 'Started Webserver at http://localhost:3500' )
} )
