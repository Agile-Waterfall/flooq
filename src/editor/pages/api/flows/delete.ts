import { NextApiRequest, NextApiResponse } from 'next'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const response = await fetch( `${process.env.API_BASE_URL}/api/DataFlow/${req.body.id}`, {
    method: 'DELETE',
    body: JSON.stringify( req.body ),
    headers: { 'Content-Type': 'application/json' }
  } )
  res.status( response.status ).json( {} )
}

export default handler
