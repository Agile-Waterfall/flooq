import axios, { AxiosResponse } from 'axios'

const apiBaseAddress = process.env.API_URL

/**
 * Gets the current version-number from the api. Can throw an error when the connection fails
 * @returns A Promise<string> containing representing the version-number
 */
export async function getApiVersion(): Promise<string> {
  const response: AxiosResponse = await axios.get( `${apiBaseAddress}/version` )
  if( response.status === 200 ) {
    return response.data
  } else {
    throw new Error( `Axios encountered an error with status code ${response.status}\nBody: ${response.data}\nHeaders: ${response.headers}\n\n Response-object: ${response}` )
  }
}
