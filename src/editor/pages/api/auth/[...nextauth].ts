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
        authorization: { params: { scope: 'openid profile read write' } },
        idToken: true,
        checks: ['pkce', 'state'],
        clientId: process.env.IDENTITY_SERVER_CLIENT_ID,
        clientSecret: process.env.IDENTITY_SERVER_CLIENT_SECRET,
        profile( profile, tokens ): any {
          console.log( { profile, tokens } )
          return {
            id: profile.sub,
            name: profile.name,
            email: profile.email,
            image: profile.picture,
          }
        },
      }
    ],
    session: {
      strategy: 'jwt',
      maxAge: 30 * 24 * 60 * 60, // 30 days
      updateAge: 24 * 60 * 60, // 24 hours
    },
    callbacks: {
      async jwt( { token, account } ): Promise<JWT> {
        if ( account ) {
          token.accessToken = account.access_token
        }

        if ( account?.id_token ) {
          token.idToken = account.id_token
        }

        return token
      },
    }
  } )
}
