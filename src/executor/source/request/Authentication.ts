import { Issuer, TokenSet, BaseClient } from 'openid-client'

let token: TokenSet
let client: BaseClient

async function getClient(): Promise<BaseClient> {
  if ( !client ) {
    const client_id = process.env.AUTHORIZATION_CLIENT_ID
    const client_secret = process.env.AUTHORIZATION_CLIENT_SECRET
    const issuerURL = process.env.IDENTITY_SERVER_ISSUER

    if ( client_id === undefined || client_secret === undefined || issuerURL === undefined )
      return Promise.reject(
        'AUTHORIZATION_CLIENT_ID, AUTHORIZATION_CLIENT_SECRET or IDENTITY_SERVER_ISSUER not set'
      )

    client = await Issuer.discover( issuerURL ).then( issuer => new issuer.Client( { client_id, client_secret } ) )
  }
  return client
}

export async function getJWT(): Promise<string> {
  if ( !token )
    token = await getClient().then( c => c.grant( { grant_type: 'client_credentials' } ) )

  if ( token.expired() ) {
    if ( token.refresh_token ) {
      token = await getClient().then( c => c.refresh( token ) )
    } else {
      token = await getClient().then( c => c.grant( { grant_type: 'client_credentials' } ) )
    }
  }

  if ( !token.access_token ) return Promise.reject( 'Could not retrieve token' )
  return token.access_token
}
