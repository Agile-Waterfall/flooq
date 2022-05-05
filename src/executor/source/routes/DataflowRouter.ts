import express from 'express'
import {
  getDataflow,
  postLinearizedDataflow,
  getLinearizedDataflow as getLinearizedDataflow
} from '../api/ApiInterface'
import Logger from '../utils/logging/Logger'
import bodyParser from 'body-parser'
import * as Executor from '../executor/Executor'
import { HttpStatusCode } from '../utils/HttpStatusCode'
import { linearize } from '../executor/Linearization'
import { APILinearizedDataflow } from '../Dataflow'

const DataflowRouter = express.Router()

DataflowRouter.use( bodyParser.json() )
DataflowRouter.use( bodyParser.urlencoded( { extended: true } ) )

DataflowRouter.all( '/:dataflowID', async ( req, res ) => {
  let dataflowResponse = undefined
  try {
    dataflowResponse = await getDataflow( req.params.dataflowID )
  } catch ( error ) {
    Logger.error( error )
    res.status( HttpStatusCode.NOT_FOUND ).send( { message: 'Could not get Dataflow from API.' } )
    return
  }

  let linearizedDataflow = undefined
  let graph = undefined
  try {
    graph = await getLinearizedDataflow( req.params.dataflowID )
    linearizedDataflow = JSON.parse( graph.graph )
  } catch ( requestError ) {
    Logger.error( requestError + '\nCould not get linearised DataFlow' )

    try {
      linearizedDataflow = linearize( JSON.parse( dataflowResponse.definition ) )
    } catch ( error ){
      Logger.error( error )
      res.status( HttpStatusCode.INTERNAL_SERVER_ERROR ).send( { error } )
      return
    }
    const linearisedGraph: APILinearizedDataflow = {
      id: req.params.dataflowID,
      graph: JSON.stringify( linearizedDataflow )
    }

    try {
      await postLinearizedDataflow( JSON.stringify( linearisedGraph ) )
    } catch ( error ){
      Logger.error( error + '\nCould not post linearized graph' )
    }
  }

  try {
    await Executor.execute( req, linearizedDataflow )
  } catch ( error ) {
    Logger.error( error )
    res.status( HttpStatusCode.INTERNAL_SERVER_ERROR ).send( { error } )
    return
  }
  res.status( HttpStatusCode.OK ).send()
} )

export default DataflowRouter
