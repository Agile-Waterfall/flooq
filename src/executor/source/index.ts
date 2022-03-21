import express from 'express'
import morganMiddleware from './utils/logging/morganMiddleware'
import version from './routes/VersionRouter'
import Logger from './utils/logging/Logger'

const app = express()

app.use(morganMiddleware)

app.use('/version', version)

app.listen(3500, '', () => {
  Logger.info('Started Webserver at http://localhost:3500')
})