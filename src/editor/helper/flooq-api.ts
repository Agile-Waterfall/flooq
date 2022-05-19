import { NextApiRequest } from 'next'
import { getToken } from 'next-auth/jwt'
import { BaseApi } from './api-base'

export class FlooqApi extends BaseApi {
  async request( req: NextApiRequest, method: string, url: string, headers?: any, body?: any ): Promise<any> {
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
}
