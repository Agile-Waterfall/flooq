import axios, { AxiosError, AxiosRequestConfig } from 'axios'
import { getJWT } from '../request/Authentication'
import { webRequest } from '../request/WebRequest'
import Logger from '../utils/logging/Logger'

/**
 * Requests the provided path from the API with a HTTP GET request. Can throw an error when the connection fails.
 *
 * @param path to get
 * @returns the parsed response
 */
export async function get( path: string ): Promise<any> {
  return requestHandler( {
    method: 'GET',
    url: `${process.env.API_BASE_URL}/api/${path}`,
    headers: { Authorization: `Bearer ${await getJWT() }` }
  } )
}

/**
 * Posts the provided data to the API with a HTTP POST request. Can throw an error when the connection fails.
 *
 * @param path to post
 * @param data data to post
 * @returns the parsed response
 */
export async function post( path: string, data: string ): Promise<any> {
  return requestHandler( {
    method: 'POST',
    url: `${process.env.API_BASE_URL}/api/${path}`,
    data: data,
    headers: { Authorization: `Bearer ${await getJWT() }` }
  } )
}

async function requestHandler( config: AxiosRequestConfig ): Promise<any>{
  return webRequest( config )
    .then( res => res.data )
    .catch( error => {
      if ( axios.isAxiosError( error ) ) {
        const ax = ( error as AxiosError ).toJSON()
        Logger.error( error.response )
        return Promise.reject( ax )
      } else {
        Logger.error( error )
        return Promise.reject( error )
      }
    } )
}

