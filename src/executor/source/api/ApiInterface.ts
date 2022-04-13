import { APIDataflowResponse } from '../Dataflow'
import * as API from './ApiWrapper'

/**
 * Gets the current version-number from the api. Can throw an error if the connection fails.
 *
 * @returns A Promise<string> containing the version-number
 */
export async function getApiVersion(): Promise<string> {
  return API.get( 'version' )
}

/**
 * Gets the dataflow specified by the provided id from the api.
 * Can throw an error if the connection fails.
 *
 * @param dataflowId of the desired dataflow
 * @returns The desired dataflow if present
 */
export async function getDataflow( dataflowId: string ): Promise<APIDataflowResponse> {
  return API.get( `DataFlow/${dataflowId}` )
}

