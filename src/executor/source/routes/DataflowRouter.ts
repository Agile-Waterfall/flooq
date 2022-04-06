import express from 'express'
import { getDataflow as getDataflow } from '../api/ApiInterface'
import Logger from '../utils/logging/Logger'
import bodyParser from 'body-parser'
import * as Executor from '../executor/Executor'

const DataflowRouter = express.Router()

DataflowRouter.use( bodyParser.urlencoded( { extended: true } ) )
DataflowRouter.all( ':dataflowID', async ( req, res ) => {
  let dataflowResponse = undefined
  try {
    dataflowResponse = await getDataflow( req.params.dataflowID ) // TODO: Relay 404 to user – prevent brute force detection of existing flows/users
  } catch ( error ) {
    Logger.error( error )
    res.status( 500 ).send( 'Could not get Dataflow from API.' )
    return
  }

  let result = undefined
  try {
    result = await Executor.execute(
      JSON.parse( dataflowResponse.definition ),
      {
        method: req.method,
        query: req.query,
        body: req.body
      }
    )
  } catch ( error ){
    Logger.error( error )
    res.status( 500 ).send( error )
    return
  }
  if ( result ) res.status( 200 ).send( result )
  else res.sendStatus( 200 )
} )

export default DataflowRouter
