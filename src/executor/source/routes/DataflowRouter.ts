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
import { DataflowDurationMetric, ExecuteDataflowMetric, LABELS } from '../utils/MetricsCollector'

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
    linearizedDataflow = JSON.parse( graph.dataflow )
  } catch ( requestError ) {
    Logger.error( requestError + 'could not get linearised DataFlow' )

    try {
      linearizedDataflow = linearize( JSON.parse( dataflowResponse.definition ) )
    } catch ( error ){
      Logger.error( error )
      res.status( HttpStatusCode.INTERNAL_SERVER_ERROR ).send( { error } )
      return
    }
    const linearisedGraph: APILinearizedDataflow = {
      id: req.params.dataflowID,
      dataflow: JSON.stringify( linearizedDataflow )
    }

    try {
      await postLinearizedDataflow( JSON.stringify( linearisedGraph ) )
    } catch ( error ){
      Logger.error( error + 'could not post linearized graph' )
    }
  }

  const end = DataflowDurationMetric.startTimer()
  try {
    await Executor.execute( req, linearizedDataflow )
    // Stop execution duration timer
    end( { status: LABELS.DATAFLOW_DURATION.SUCCESS } )
    // Increment counter of successfully executed dataflows
    ExecuteDataflowMetric.labels( LABELS.EXECUTE_DATAFLOW.SUCCESS ).inc()
  } catch ( error ) {
    Logger.error( error )
    // Stop execution duration timer
    end( { status: LABELS.DATAFLOW_DURATION.ERROR } )
    // Increment counter of successfully executed dataflows
    ExecuteDataflowMetric.labels( LABELS.EXECUTE_DATAFLOW.ERROR ).inc()
    res.status( HttpStatusCode.INTERNAL_SERVER_ERROR ).send( { error } )
    return
  }
  res.status( HttpStatusCode.OK ).send()
} )

export default DataflowRouter
