import client from 'prom-client'

const prefix = 'executor_'

const executeDataflowMetric = new client.Counter( {
  name: `${prefix}execute_dataflow`,
  help: 'Amount of successfully executed dataflows',
  labelNames: ['status']
} )

const dataflowDurationMetric = new client.Gauge( {
  name: `${prefix}dataflow_duration`,
  help: 'Duration of executed dataflows'
} )

export function collectDefaultMetrics(): void {
  client.collectDefaultMetrics( { prefix } )
}

export function executeDataflowSuccess(): void {
  executeDataflowMetric.labels( { status: 'SUCCESS' } ).inc()
}

export function executeDataflowError(): void {
  executeDataflowMetric.labels( { status: 'ERROR' } ).inc()
}

export function dataflowDuration(): () => void {
  return dataflowDurationMetric.startTimer()
}
