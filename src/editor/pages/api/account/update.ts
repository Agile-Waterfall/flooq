import { NextApiRequest, NextApiResponse } from 'next'
import { putRequest } from '../../../helper/api-base'
import { FlooqIdentityApi } from '../../../helper/flooq-identity'

const updateAccount = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await putRequest( FlooqIdentityApi, req, `/api/user/${req.query.userId}`, null, JSON.stringify( req.body ) )
  if ( !response.ok ) {
    res.status( response.status ).end()
    return
  }
  res.status( response.status ).json( await response.json() )
}

export default updateAccount
