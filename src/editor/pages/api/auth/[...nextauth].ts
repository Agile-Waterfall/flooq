import type { NextApiRequest, NextApiResponse } from 'next'
import NextAuth from 'next-auth'
import GitHubProvider from 'next-auth/providers/github'


export default async function auth( req: NextApiRequest, res: NextApiResponse ): Promise<any> {
  return await NextAuth( req, res, {
    providers: [
      GitHubProvider( {
        clientId: process.env.GITHUB_CLIENT_ID,
        clientSecret: process.env.GITHUB_CLIENT_SECRET
      } )
    ],
    session: {
      strategy: 'jwt',
      maxAge: 30 * 24 * 60 * 60, // 30 days
      updateAge: 24 * 60 * 60, // 24 hours
    }
  } )
}
