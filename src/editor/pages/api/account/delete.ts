import { NextApiRequest, NextApiResponse } from 'next'
import { deleteRequest } from '../../../helper/api-base'
import { FlooqIdentityApi } from '../../../helper/flooq-identity'

const deleteAccount = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await deleteRequest( FlooqIdentityApi, req, `/api/user/${req.query.userId}` )
  if ( !response.ok ) {
    res.status( response.status ).end()
    return
  }
  res.status( response.status ).json( await response.json() )
}

export default deleteAccount
