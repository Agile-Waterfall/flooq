// Next.js API route support: https://nextjs.org/docs/api-routes/introduction

import flows from './flows.json'

const handler = ( req: any, res: any ): void => {
  res.status( 200 ).json( flows )
}

export default handler
