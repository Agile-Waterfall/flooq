import flows from './flows.json'

const handler = ( req: any, res: any ): void => {
  res.status( 200 ).json( flows )
}

export default handler
