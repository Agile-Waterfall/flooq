import axios from 'axios'
import { APIGraphResponse } from '../Dataflow'
import { webRequest } from '../request/WebRequest'
import Logger from '../utils/logging/Logger'

/**
 * Requests the provided path from the API with a HTTP GET request. Can throw an error when the connection fails.
 *
 * @param path to get
 * @returns the parsed response
 */
export async function get( path: string ): Promise<any> {
  return webRequest( { method: 'GET', url: `${process.env.API_BASE_URL}/api/${path}` } )
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

/**
 * posts the provided data to the API with a HTTP POST request. Can throw an error when the connection fails.
 *
 * @param path to post
 * @param data data to post
 * @returns the parsed response
 */
export async function post( path: string, data: string ): Promise<any> {
  return webRequest( { method: 'POST', url: `${process.env.API_BASE_URL}/api/${path}`, data: `${data}` } )
    .then( res => res.data )
    .catch( error => {
      Logger.error( error )
      if ( axios.isAxiosError( error ) ) {
        return Promise.reject( Object.assign( error, { 'message':
        `Axios encountered an error with status code ${error.code}\nResponse: ${
          JSON.stringify( error.response )}\nRequest: ${ error.request }\n\nError-object: ${error}` } ) )
      } else {
        return Promise.reject( Object.assign( error, { 'message':
        `An unknown error occurred when posting "${path}"\nError-object: ${JSON.stringify( error )}` } ) )
      }
    } )
}
