import axios, { AxiosRequestConfig, Method } from 'axios'

/**
 * Wrapper around axios' axios() method.
 *
 * @param config to use
 * @returns the response
 */
export async function webRequest( config: AxiosRequestConfig ): Promise<any>{
  return axios( config )
}
