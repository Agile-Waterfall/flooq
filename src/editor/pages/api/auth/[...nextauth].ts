import type { NextApiRequest, NextApiResponse } from 'next'
import NextAuth, { User } from 'next-auth'
import { JWT } from 'next-auth/jwt'
import { Provider } from 'next-auth/providers'
import GitHubProvider from 'next-auth/providers/github'
import IdentityServer4Provider from 'next-auth/providers/identity-server4'
import FlooqAuthAdapter from './FlooqAuthAdapter'


export default async function auth( req: NextApiRequest, res: NextApiResponse ): Promise<any> {
  const test: Provider = {

  }
  return await NextAuth( req, res, {
    providers: [
      GitHubProvider( {
        clientId: process.env.GITHUB_CLIENT_ID,
        clientSecret: process.env.GITHUB_CLIENT_SECRET
      } ),
      {
        id: 'flooq',
        name: 'Flooq',
        type: 'oauth',
        wellKnown: 'https://localhost:5001/.well-known/openid-configuration',
        authorization: { params: { scope: 'openid profile flooqapi' } },
        idToken: true,
        checks: ['pkce', 'state'],
        profile( profile ): any {
          console.log( 'GET PROFILE', profile )
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
    pages: {
      signIn: '/auth/signin',
    },
    // adapter: FlooqAuthAdapter(),
    callbacks: {
      async jwt( params ): Promise<JWT> {
        console.log( 'JWT', params )

        return params.token
      }
    }
  } )
}
