import express from 'express'
import { getDataFlow } from '../ApiInterface'
import Logger from '../utils/logging/Logger'

const FlowRouter = express.Router()

FlowRouter.get( ':username/:dataFlowID', async ( req, res ) => {
  try {
    const dataFlow = await getDataFlow( req.params.username, req.params.dataFlowID )

    res.status( 200 ).send( 'Flow done' )
  } catch ( error ) {
    Logger.error( error )
    res.sendStatus( 500 )
  }
} )

export default FlowRouter
