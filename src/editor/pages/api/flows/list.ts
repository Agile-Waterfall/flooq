import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: any, res: any ): Promise<void> => {
  const response = await FlooqApi.getRequest( req, '/api/DataFlow' )

  if ( !response.ok ) {
    res.status( response.status ).json( [] )
    return
  }

  const dataFlows = await response.json()
  res.status( response.status ).json( dataFlows )
}

export default handler
