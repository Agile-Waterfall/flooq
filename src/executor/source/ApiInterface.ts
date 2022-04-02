import axios, { AxiosResponse } from 'axios'
import Logger from './utils/logging/Logger'

/**
 * Gets the current version-number from the api. Can throw an error when the connection fails
 * @returns A Promise<string> containing representing the version-number
 */
export async function getApiVersion(): Promise<string> {
  try {
    const response: AxiosResponse = await axios.get( `${process.env.API_URL}/api/version` )

    return response.data
  } catch ( error ) {
    Logger.error( error )
    if ( axios.isAxiosError( error ) ) {
      throw new Error( `Axios encountered an error with status code ${error.code}\nBody: ${error.response}\nRequest: ${error.request}\n\nError-object: ${error}` )
    } else {
      throw new Error( `An unknown error occurred when getting api version\nError-object: ${error}` )
    }
  }
}
