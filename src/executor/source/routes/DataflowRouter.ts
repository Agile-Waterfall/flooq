import express from 'express'
import { getDataflow as getDataflow } from '../api/ApiInterface'
import Logger from '../utils/logging/Logger'
import bodyParser from 'body-parser'
import { execute } from '../executor/Executor'

const DataflowRouter = express.Router()

DataflowRouter.use( bodyParser.urlencoded( { extended: true } ) )
DataflowRouter.all( ':dataflowID', async ( req, res ) => {
  const data = {
    method: req.method,
    query: req.query,
    body: req.body
  }
  let dataflow = undefined
  try {
    dataflow = await getDataflow( req.params.dataflowID ) // TODO: Relay 404 to user – prevent brute force detection of existing flows/users
  } catch ( error ) {
    Logger.error( error )
    res.status( 500 ).send( 'Could not get Dataflow from API.' )
    return
  }

  let result = undefined

  if ( dataflow && dataflow.status === 'active' ) {
    try {
      result = await execute( dataflow, data )
    } catch ( error ){
      Logger.error( error )
      res.status( 500 ).send( error )
      return
    }
  }

  res.status( 200 ).send( result )
} )

export default DataflowRouter
