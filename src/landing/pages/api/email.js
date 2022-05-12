import * as FlooqApi from '../../helper/flooq-api'

const handler = async ( req, res ) => {
  const response = await FlooqApi.postRequest( req, '/api/Contact', { 'Content-Type': 'application/json' }, JSON.stringify(req.body) )
  const payload = await response.json()
  
  res.status( response.status ).json( payload )
}

export default handler