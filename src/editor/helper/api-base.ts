import { NextApiRequest } from 'next'

export const getRequest = async <T extends BaseApi>( Api: new() => T, req: any, url: any, headers?: any ): Promise<Response> => {
  return new Api().request( req, 'GET', url, headers )
}

export const deleteRequest = async <T extends BaseApi>( Api: new() => T, req: any, url: any, headers?: any ): Promise<Response> => {
  return new Api().request( req, 'DELETE', url, headers )
}

export const postRequest = async <T extends BaseApi>( Api: new() => T, req: any, url: any, headers?: any, body?: any ): Promise<Response> => {
  return new Api().request( req, 'POST', url, headers, body )
}

export const putRequest = async <T extends BaseApi>( Api: new() => T, req: any, url: any, headers?: any, body?: any ): Promise<Response> => {
  return new Api().request( req, 'PUT', url, headers, body )
}

export abstract class BaseApi {
  abstract request( req: NextApiRequest, method: string, url: string, headers?: any, body?: any ): Promise<any>
}
