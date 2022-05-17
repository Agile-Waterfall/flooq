import { NextApiRequest, NextApiResponse } from 'next'
import { deleteRequest } from '../../../helper/api-base'
import { FlooqApi } from '../../../helper/flooq-api'
import { FlooqIdentityApi } from '../../../helper/flooq-identity'

const deleteAccount = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const deleteFlowsResponse = await deleteRequest( FlooqApi, req, '/api/DataFlow/all' )
  if( !deleteFlowsResponse.ok ) {
    res.status( deleteFlowsResponse.status ).json( { error: 'Could not delete all data flows.' } )
    return
  }

  const deleteTokenResponse = await deleteRequest( FlooqApi, req, '/api/Token/all' )
  if( !deleteTokenResponse.ok ) {
    res.status( deleteTokenResponse.status ).json( { error: 'Could not delete all tokens.' } )
    return
  }

  const response = await deleteRequest( FlooqIdentityApi, req, `/api/user/${req.query.userId}` )
  if ( !response.ok ) {
    res.status( response.status ).json( { error: 'Could not delete user.' } )
    return
  }
  res.status( response.status ).json( await response.json() )
}

export default deleteAccount
