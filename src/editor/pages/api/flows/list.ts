const handler = async ( req: any, res: any ): Promise<void> => {
  const response = await fetch( `${process.env.API_BASE_URL}/api/DataFlow`,
    {
      headers: req.headers
    }
  )

  if ( !response.ok ) {
    res.status( response.status ).json( [] )
    return
  }

  const dataFlows = await response.json()

  res.status( response.status ).json( dataFlows )
}

export default handler
