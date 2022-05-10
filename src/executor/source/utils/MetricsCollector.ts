import client from 'prom-client'
import promBundle, { Middleware } from 'express-prom-bundle'
import express from 'express'

const prefix = 'executor_'

/**
 * Gets the middleware from express and adds the required parameters.
 *
 * @returns middleware from express with the required parameters
 */
export function getPromBundleConfig(): Middleware {
  return promBundle(
    {
      includeMethod: true,
      autoregister: true,
      bypass: ( req: express.Request<any, any, any, any, Record<string, any>> ): boolean => {
        return req.url.includes( 'metrics' )
      },
      promClient: {
        collectDefaultMetrics: {
          prefix: 'executor_'
        }
      }
    }
  )
}

// ====Metrics====

export const ExecuteDataflowMetric = new client.Counter( {
  name: `${prefix}execute_dataflow`,
  help: 'Amount of executed dataflows',
  labelNames: ['status']
} )

export const DataflowDurationMetric = new client.Histogram( {
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
