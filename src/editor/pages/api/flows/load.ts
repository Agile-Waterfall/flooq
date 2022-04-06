const handler = async ( _req: any, res: any ): Promise<void> => {

  const url = `${process.env.API_BASE_URL}/api/DataFlow`
  const request = {
    headers: {
      accept: 'text/plain'
    },
  }

  const response = await fetch( url, request )
  const payload = await response.json()

  res.status( response.status ).json( payload )
}

export default handler
