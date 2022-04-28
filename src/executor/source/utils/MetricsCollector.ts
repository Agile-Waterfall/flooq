import client from 'prom-client'

const prefix = 'executor_'

export function collectDefaultMetrics(): void {
  client.collectDefaultMetrics( { prefix } )
}

// ====Metrics====

export const executeDataflowMetric = new client.Counter( {
  name: `${prefix}execute_dataflow`,
  help: 'Amount of executed dataflows',
  labelNames: ['status']
} )

export const dataflowDurationMetric = new client.Histogram( {
  name: `${prefix}dataflow_duration`,
  help: 'Duration of executed dataflows',
  labelNames: ['status']
} )

// ====LABELS====

enum DATAFLOW_ERROR_SUCCESS {
  SUCCESS = 'SUCCESS',
  ERROR = 'ERROR'
}

export const LABELS = {
  EXECUTE_DATAFLOW: DATAFLOW_ERROR_SUCCESS,
  DATAFLOW_DURATION: DATAFLOW_ERROR_SUCCESS,
}
