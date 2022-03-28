import express from 'express'
import { getApiVersion } from '../ApiInterface'
import Logger from '../utils/logging/Logger'

const VersionRouter = express.Router()

VersionRouter.get( '', async ( req, res ) => {

  try {
    const apiResponse = await getApiVersion()
    res.status( 200 ).send( apiResponse )
  } catch ( error ) {
    Logger.error( error )
    res.sendStatus( 500 )
  }
} )

export default VersionRouter
