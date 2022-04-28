import { NextApiRequest, NextApiResponse } from 'next'
import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await FlooqApi.deleteRequest( req, `/api/DataFlow/${req.body.id}` )
  res.status( response.status ).json( {} )
}

export default handler
