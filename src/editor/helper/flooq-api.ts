import { getToken } from 'next-auth/jwt'

export const getRequest = async ( req: any, url: any, headers?: any ): Promise<Response> => {
  return request( req, 'GET', url, headers )
}

export const deleteRequest = async ( req: any, url: any, headers?: any ): Promise<Response> => {
  return request( req, 'DELETE', url, headers )
}

export const postRequest = async ( req: any, url: any, headers?: any, body?: any ): Promise<Response> => {
  return request( req, 'POST', url, headers, body )
}

export const putRequest = async ( req: any, url: any, headers?: any, body?: any ): Promise<Response> => {
  return request( req, 'PUT', url, headers, body )
}

const request = async ( req: any, method: string, url: string, headers?: any, body?: any ): Promise<Response> => {
  const token = await getToken( { req } )
  return fetch( `${process.env.API_BASE_URL}${url}`,
    {
      method,
      headers: {
        Authorization: `Bearer ${token?.accessToken}`,
        ...headers
      },
      body
    }
  )
}
