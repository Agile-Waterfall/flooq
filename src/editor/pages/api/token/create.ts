import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: any, res: any ): Promise<void> => {
  req.body = JSON.parse( req.body )
  const token = {
    name: req.body.name,
    value: req.body.value
  }
  const response = await FlooqApi.postRequest( req, '/api/Token', { 'Content-Type': 'application/json' }, JSON.stringify( token ) )
  res.status( response.status ).json( await response.json() )
}

export default handler
