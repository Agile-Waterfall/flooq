import express from 'express'
import { getApiVersion } from '../api/ApiInterface'
import Logger from '../utils/logging/Logger'

const VersionRouter = express.Router()

VersionRouter.get( '', async ( req, res ) => {
  try {
    const apiResponse = await getApiVersion()
    res.status( 200 ).send( apiResponse )
  } catch ( error ) {
    Logger.error( error )
    res.status( 500 ).send( 'Internal Error: API connection error' )
  }
} )

export default VersionRouter
