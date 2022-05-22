import fs from 'fs/promises'
import { NextApiRequest, NextApiResponse } from 'next'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  const path = './components/graph/docs/' + req.query.path
  try {
    res.send( await fs.readFile( path, { encoding: 'utf-8' } ) )
  } catch ( e ) {
    res.status( 404 ).json( e )
  }
}

export default handler
