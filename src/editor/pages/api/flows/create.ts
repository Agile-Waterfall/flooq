import { v4 as uuidv4 } from 'uuid'
import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: any, res: any ): Promise<void> => {
  const newFlow = { name: 'DataFlow-' + uuidv4().toString().substring( 0, 8 ) }
  const response = await FlooqApi.postRequest( req, '/api/DataFlow', { 'Content-Type': 'application/json' }, JSON.stringify( newFlow ) )
  const payload = await response.json()

  res.status( response.status ).json( payload )
}

export default handler
