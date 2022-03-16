// Next.js API route support: https://nextjs.org/docs/api-routes/introduction

import * as flows from '../flows.json'

const handler = ( req: any, res: any ): void => {
  const flow = flows.find( x => x.id === req.query.id )
  res.status( 200 ).json( flow )
}

export default handler
