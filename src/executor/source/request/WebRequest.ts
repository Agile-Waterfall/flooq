import axios, { AxiosPromise, AxiosRequestConfig } from 'axios'

/**
 * Wrapper around axios' axios() method.
 *
 * @param config to use
 * @returns the response
 */
export function webRequest( config: AxiosRequestConfig ): AxiosPromise<any>{
  return axios( config )
}
