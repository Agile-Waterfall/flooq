import { NextApiRequest, NextApiResponse } from 'next'
import { getRequest } from '../../../helper/api-base'
import { FlooqApi } from '../../../helper/flooq-api'

const handler = async ( req: NextApiRequest, res: NextApiResponse ): Promise<void> => {
  try {
    const response = await getRequest( FlooqApi, req, `/api/DataFlow/user/${req.query.id}` )
    const flow = await response.json()

    let flowElements = {
      nodes: [],
      edges: []
    }

    if( flow.definition ) {
      flowElements = JSON.parse( flow.definition )
    }

    res.status( response.status ).json( {
      ...flow,
      nodes: flowElements.nodes,
      edges: flowElements.edges
    } )
  } catch ( e ) {
    res.status( 500 ).json( { error: 'Failed to load data flow.', exception: e } )
  }
}

export default handler
