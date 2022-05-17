import { NextApiRequest, NextApiResponse } from 'next'
import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await FlooqApi.deleteRequest( req, `/api/Token/${ req.query.id }` )
  res.status( response.status ).end()
}

export default handler
