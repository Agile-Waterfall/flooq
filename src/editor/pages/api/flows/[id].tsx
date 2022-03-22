import flows from '../flows.json'

const handler = ( req: any, res: any ): void => {
  const id = Number.parseInt( req.query.id )
  const flow = flows.find( x => x.id === id )
  res.status( 200 ).json( flow )
}

export default handler
