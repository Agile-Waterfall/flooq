const handler = ( req: any, res: any ): void => {
  /**
   * Here the real call to the api will be defined
   */
  res.status( 200 ).json( {
    id: 69,
    name: 'newFlow',
    status: 'ok'
  } )
}

export default handler
