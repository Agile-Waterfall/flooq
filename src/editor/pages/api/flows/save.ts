import { NextApiRequest, NextApiResponse } from 'next'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  try {
    const response = await fetch( `${process.env.API_BASE_URL}/api/DataFlow/${req.body.id}`, {
      method: req.method,
      body: JSON.stringify( req.body ),
      headers: {
        'Content-Type': 'application/json'
      }
    } )

    res.status( response.status ).json( {} )
  } catch ( e ) {
    res.status( 500 ).json( { error: 'Failed to save data flow.', exception: e } )
  }
}

export default handler
