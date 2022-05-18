import { NextApiRequest, NextApiResponse } from 'next'
import { deleteRequest } from '../../../helper/api-base'
import { FlooqApi } from '../../../helper/flooq-api'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await deleteRequest( FlooqApi, req, `/api/DataFlow/${req.body.id}` )
  res.status( response.status ).end()
}

export default handler
