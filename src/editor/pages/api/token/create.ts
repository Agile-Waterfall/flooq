import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: any, res: any ): Promise<void> => {
  const response = await FlooqApi.postRequest( req, '/api/Token', { 'Content-Type': 'application/json' }, req.body )
  res.status( response.status ).json( await response.json() )
}

export default handler
