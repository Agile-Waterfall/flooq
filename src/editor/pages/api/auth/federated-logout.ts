import type { NextApiRequest, NextApiResponse } from 'next'
import { getToken } from 'next-auth/jwt'

const redirectUrl: any = `${process.env.NEXTAUTH_URL}/logout-done`

export const federatedLogout = async ( req: NextApiRequest, res: NextApiResponse ): Promise<any> => {
  try {
    const token = await getToken( { req } )
    if ( !token ) {
      return res.redirect( redirectUrl )
    }

    const endsessionURL = `${process.env.IDENTITY_SERVER_ISSUER}/connect/endsession`

    const params: any = {
      id_token_hint: token?.idToken,
      post_logout_redirect_uri: redirectUrl,
    }

    const endsessionParams = new URLSearchParams( params )
    return res.redirect( `${endsessionURL}?${endsessionParams}` )
  } catch ( error ) {
    res.redirect( redirectUrl )
  }
}

export default federatedLogout
