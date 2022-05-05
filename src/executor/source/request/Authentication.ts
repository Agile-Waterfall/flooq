import { Issuer, BaseClient, ClientMetadata } from 'openid-client'
import 'dotenv/config'

let client: BaseClient

if ( !process.env.IDENTITY_SERVER_ISSUER
  || process.env.IDENTITY_SERVER_ISSUER.length === 0
  || !process.env.IDENTITY_SERVER_CLIENT_ID
  || process.env.IDENTITY_SERVER_CLIENT_ID.length === 0
  || !process.env.IDENTITY_SERVER_CLIENT_SECRET
  || process.env.IDENTITY_SERVER_CLIENT_SECRET.length === 0 )
  throw new Error ( 'IDENTITY_SERVER_CLIENT_ID, IDENTITY_SERVER_CLIENT_SECRET or IDENTITY_SERVER_ISSUER not set' )

const issuerURL = process.env.IDENTITY_SERVER_ISSUER
const config: ClientMetadata = {
  client_id: process.env.IDENTITY_SERVER_CLIENT_ID,
  client_secret: process.env.IDENTITY_SERVER_CLIENT_SECRET,
}

export async function getJWT(): Promise<string> {
  if ( !client ) client = await Issuer.discover( issuerURL ).then( issuer => new issuer.Client( config ) )
  const token = await client.grant( { grant_type: 'client_credentials' } )
  if ( !token.access_token ) return Promise.reject( 'Could not retrieve token' )
  return token.access_token
}
