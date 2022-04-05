import express from 'express'
import { getDataFlow } from '../api/ApiInterface'
import Logger from '../utils/logging/Logger'
import bodyParser from 'body-parser'
import { execute } from '../executor/Executor'

const FlowRouter = express.Router()

FlowRouter.use( bodyParser.urlencoded( { extended: true } ) )
FlowRouter.all( ':username/:dataFlowID', async ( req, res ) => {
  const data = {
    method: req.method,
    query: req.query,
    body: req.body
  }

  try {
    const dataFlow = await getDataFlow( req.params.username, req.params.dataFlowID ) // TODO: Relay 404 to user – prevent brute force detection of existing flows/users
    const result = await execute( dataFlow, data ) // TODO: Proper error handling during execution
    res.status( 200 ).send( result )
  } catch ( error ) {
    Logger.error( error )
    res.sendStatus( 500 )
  }
} )

export default FlowRouter
