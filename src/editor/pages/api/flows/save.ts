const handler = ( req: any, res: any ): void => {
  res.status( 200 ).json( { status: 'ok', body: JSON.parse( req.body ) } )
}

export default handler
