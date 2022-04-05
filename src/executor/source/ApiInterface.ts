import axios, { AxiosResponse } from 'axios'
import { Response } from 'express'
import Logger from './utils/logging/Logger'

/**
 * Gets the current version-number from the api. Can throw an error when the connection fails
 * @returns A Promise<string> containing representing the version-number
 */
export async function getApiVersion(): Promise<string> {
  return ( await getFromAPI( '/api/version' ) ).data
}

export async function getDataFlow( username: string, dataFlowID: string ): Promise<JSON> {
  return ( await getFromAPI( `/api/${username}/${dataFlowID}` ) ).data // TODO: adjust to the proper URL
}

async function getFromAPI( path: string ): Promise<AxiosResponse> {
  try {
    return await axios.get( `${process.env.API_BASE_URL}${path}` )
  } catch ( error ) {
    Logger.error( error )
    if ( axios.isAxiosError( error ) ) {
      throw new Error( `Axios encountered an error with status code ${error.code}\nBody: ${error.response}\nRequest: ${error.request}\n\nError-object: ${error}` )
    } else {
      throw new Error( `An unknown error occurred when getting "${path}"\nError-object: ${error}` )
    }
  }
}
