import type { NextApiRequest, NextApiResponse } from 'next'
import NextAuth from 'next-auth'
import { JWT } from 'next-auth/jwt'
import { getRequest } from '../../../helper/api-base'
import { FlooqIdentityApi } from '../../../helper/flooq-identity'

export default async function auth( req: NextApiRequest, res: NextApiResponse ): Promise<any> {
  return NextAuth( req, res, {
    providers: [
      {
        id: 'flooq',
        name: 'Flooq',
        type: 'oauth',
        wellKnown: `${process.env.IDENTITY_SERVER_ISSUER}/.well-known/openid-configuration`,
        authorization: { params: { scope: 'openid profile read write offline_access' } },
        idToken: true,
        checks: ['pkce', 'state'],
        clientId: process.env.IDENTITY_SERVER_CLIENT_ID,
        clientSecret: process.env.IDENTITY_SERVER_CLIENT_SECRET,
        profile( profile ): any {
          return {
            id: profile.sub,
            test: profile.sub,
            name: profile.username,
            email: profile.email
          }
        },
      }
    ],
    pages: {
      signIn: '/login'
    },
    session: {
      strategy: 'jwt',
      maxAge: 60 * 50, // 50 min
      updateAge: 60 * 5, // 5 min
    },
    callbacks: {
      session: async ( { session, token } ) => {
        if ( session?.user ) {
          session.user.id = token.uid
          session.user.name = token.name
        }
        return session
      },
      async jwt( { token, account, user } ): Promise<JWT> {
        if ( req.url === '/api/auth/session?update' ) {
          const request = await getRequest( FlooqIdentityApi, req, `/api/user/${token.uid}` )
          const payload = await request.json()
          token.name = payload.userName
        }
        if ( account ) {
          token.accessToken = account.access_token
          token.refreshToken = account.refresh_token
          token.expires_at = account.expires_at
          token.idToken = account.id_token
        }
        if ( user ) {
          token.uid = user.id
        }

        return token
      }
    }
  } )
}
