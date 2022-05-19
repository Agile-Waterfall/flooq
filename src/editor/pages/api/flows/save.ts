import { NextApiRequest, NextApiResponse } from 'next'
import { putRequest } from '../../../helper/api-base'
import { FlooqApi } from '../../../helper/flooq-api'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  try {
    const response = await putRequest( FlooqApi, req, `/api/DataFlow/${req.body.id}`, { 'Content-Type': 'application/json' }, JSON.stringify( req.body ) )
    res.status( response.status ).json( {} )
  } catch ( e ) {
    res.status( 500 ).json( { error: 'Failed to save data flow.', exception: e } )
  }
}

export default handler
