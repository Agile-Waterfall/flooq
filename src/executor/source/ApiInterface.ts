import axios, { AxiosError, AxiosResponse } from 'axios'

const apiBaseAddress = process.env.API_URL

/**
 * Gets the current version-number from the api. Can throw an error when the connection fails
 * @returns A Promise<string> containing representing the version-number
 */
export async function getApiVersion(): Promise<string> {
  try {
    const response: AxiosResponse = await axios.get( `${apiBaseAddress}/version` )
    return response.data
  } catch ( error ) {
    if ( axios.isAxiosError( error ) ) {
      throw new Error( `Axios encountered an error with status code ${error.code}\nBody: ${error.response}\nRequest: ${error.request}\n\nError-object: ${error}` )
    } else {
      throw new Error( `An unknown error occured when getting api version\nError-object: ${error}` )
    }
  }
}
