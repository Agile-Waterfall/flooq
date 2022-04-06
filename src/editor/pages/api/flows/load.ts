const handler = async ( _req: any, res: any ): Promise<void> => {
  const response = await fetch( `${process.env.API_BASE_URL}/api/DataFlow` )
  const dataFlows = await response.json()

  res.status( response.status ).json( dataFlows )
}

export default handler
