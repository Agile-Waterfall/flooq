import { APIDataflowResponse, APILinearizedDataflow } from '../Dataflow'
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

/**
 * Gets the linearised graph specified by the provided id from the api.
 * Can throw an error if the connection fails.
 *
 * @param graphId of the desired dataflow
 * @returns The desired graph if present
 */
export async function getLinearizedDataflow( graphId: string ): Promise<APILinearizedDataflow> {
  return API.get( `LinearizedGraph/${graphId}` )
}

/**
 * posts the linearised graph by the provided id to the api.
 * Can throw an error if the connection fails.
 *
 * @param graph stringifyed APIGraphResponse object
 * @returns The desired graph if present
 */
export async function postLinearizedDataflow( graph: string ): Promise<any> {
  return API.post( `LinearizedGraph`, graph )
}

/**
 * Retrieves a list of tokens for a specified user from the database
 * @param userId The user for whom the tokens should be retrieved
 * @returns A index-signature object containing the users tokens
 */
export async function getUserTokens( userId: string ): Promise<Record<any, any>> {
  return API.get( `Token/${userId}` )
}
