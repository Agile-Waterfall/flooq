import { NextApiRequest, NextApiResponse } from 'next'
import * as FlooqApi from '../../../helper/flooq-api'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  try {
    const response = await FlooqApi.putRequest( req, `/api/DataFlow/${req.body.id}`, { 'Content-Type': 'application/json' }, JSON.stringify( req.body ) )
    res.status( response.status ).json( {} )
  } catch ( e ) {
    res.status( 500 ).json( { error: 'Failed to save data flow.', exception: e } )
  }
}

export default handler
