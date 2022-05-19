import { NextApiRequest } from 'next'
import { getToken } from 'next-auth/jwt'
import { BaseApi } from './api-base'

export class FlooqIdentityApi extends BaseApi {
  async request( req: NextApiRequest, method: string, url: string, headers?: any, body?: any ): Promise<any> {
    const token = await getToken( { req } )
    return fetch( `${process.env.IDENTITY_SERVER_ISSUER}${url}`,
      {
        method,
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token?.accessToken}`,
          ...headers
        },
        body
      }
    )
  }
}
