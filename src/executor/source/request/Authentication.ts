import { Issuer, BaseClient, ClientMetadata } from 'openid-client'
import 'dotenv/config'

let client: BaseClient

const issuerURL = process.env.IDENTITY_SERVER_ISSUER || 'DEFAULT_IDENTITY_SERVER_ISSUER'
const config: ClientMetadata = {
  client_id: process.env.IDENTITY_SERVER_CLIENT_ID || 'DEFAULT_CLIENT_ID',
  client_secret: process.env.IDENTITY_SERVER_CLIENT_SECRET || 'DEFAULT_CLIENT_SECRET',
}

/**
 * @returns a JWT to provide to the API to get restricted data.
 */
export async function getJWT(): Promise<string> {
  if ( !client ) client = await Issuer.discover( issuerURL ).then( issuer => new issuer.Client( config ) )
  const token = await client.grant( { grant_type: 'client_credentials' } )
  if ( !token.access_token ) return Promise.reject( 'Could not retrieve token' )
  return token.access_token
}
