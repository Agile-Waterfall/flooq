import { Dataflow } from '../Dataflow'
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
 * @param username of the user that created the dataflow
 * @param dataFlowID of the desired dataflow
 * @returns The desired dataflow if present
 */
export async function getDataFlow( username: string, dataFlowID: string ): Promise<Dataflow> {
  return API.get( `/${username}/${dataFlowID}` )  // TODO: adjust to the proper URL
}

