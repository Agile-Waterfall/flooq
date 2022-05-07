import type { NextApiRequest, NextApiResponse } from 'next'
import NextAuth from 'next-auth'
import { JWT } from 'next-auth/jwt'

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
        profile( profile, tokens ): any {
          console.log( tokens )
          return {
            id: profile.sub,
            name: profile.username,
            email: profile.email
          }
        },
      }
    ],
    session: {
      strategy: 'jwt',
      maxAge: 60 * 60, // 1 hour
      updateAge: 60 * 60 - 60, // 59 min
    },
    callbacks: {
      async jwt( { token, account } ): Promise<JWT> {
        console.log( account )
        if ( account ) {
          token.accessToken = account.access_token
          token.refreshToken = account.refresh_token
          token.expires_at = account.expires_at
          token.idToken = account.id_token
        }

        return token
      }
    }
  } )
}
