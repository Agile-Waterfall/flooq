import { NextApiRequest, NextApiResponse } from 'next'
import { getRequest } from '../../../helper/api-base'
import { FlooqIdentityApi } from '../../../helper/flooq-identity'

const get = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await getRequest( FlooqIdentityApi, req, `/api/user/${req.query.userId}` )
  if ( !response.ok ) {
    res.status( response.status ).end()
    return
  }

  console.log(await response.text())

  res.status( response.status ).json( await response.json() )
}

export default get
