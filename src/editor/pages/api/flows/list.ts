import { getRequest } from '../../../helper/api-base'
import { FlooqApi } from '../../../helper/flooq-api'

const handler = async ( req: any, res: any ): Promise<void> => {
  const response = await getRequest( FlooqApi, req, '/api/DataFlow/user' )
  if ( !response.ok ) {
    res.status( response.status ).end()
    return
  }

  const dataFlows = await response.json()
  res.status( response.status ).json( dataFlows )
}

export default handler
