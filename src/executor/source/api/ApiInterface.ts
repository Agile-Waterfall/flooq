import { APIDataflowResponse } from '../Dataflow'
import * as API from './ApiWrapper'

/**
 * Gets the current version-number from the api. Can throw an error when the connection fails.
 *
 * @returns A Promise<string> containing the version-number
 */
export async function getApiVersion(): Promise<string> {
  return API.get( '/version' )
}

/**
 * Gets the dataflow specified by the provided username and string from the api. Can throw an error when the connection fails.
 *
 * @param dataflowID of the desired dataflow
 * @returns The desired dataflow if present
 */
export async function getDataflow( dataflowID: string ): Promise<APIDataflowResponse> {
  return API.get( `/${dataflowID}` )  // TODO: adjust to the proper URL
}

