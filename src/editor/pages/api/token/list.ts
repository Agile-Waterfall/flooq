import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: any, res: any ): Promise<void> => {
  const response = await FlooqApi.getRequest( req, '/api/Token/user' )
  if ( !response.ok ) {
    res.status( response.status ).end()
    return
  }
  res.status( response.status ).json( await response.json() )
}

export default handler
