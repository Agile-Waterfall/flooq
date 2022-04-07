import axios from 'axios'
import Logger from '../utils/logging/Logger'

/**
 * Requests the provided path from the API with a HTTP GET request. Can throw an error when the connection fails.
 *
 * @param path to get
 * @returns the parsed response
 */
export async function get( path: string ): Promise<any> {
  return axios.get( `${process.env.API_BASE_URL}/api/${path}` )
    .then( res => res.data )
    .catch( error => {
      Logger.error( error )
      if ( axios.isAxiosError( error ) ) {
        return Promise.reject( Object.assign( error, { 'message':
        `Axios encountered an error with status code ${error.code}\nResponse: ${
          JSON.stringify( error.response )}\nRequest: ${ error.request }\n\nError-object: ${error}` } ) )
      } else {
        return Promise.reject( Object.assign( error, { 'message':
        `An unknown error occurred when getting "${path}"\nError-object: ${JSON.stringify( error )}` } ) )
      }
    } )
}
