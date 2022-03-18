const handler = async ( req: any, res: any ): Promise<void> => {
  try {
    const statusResponse = await fetch( `${process.env.API_BASE_URL}/api/status` )
    const status = await statusResponse.text()

    const versionResponse = await fetch( `${process.env.API_BASE_URL}/api/version` )
    const version = await versionResponse.json()

    res.status( 200 ).json( { status, version } )
  } catch( e ) {
    res.status( 200 ).json( { status: 'Not Running', version: { name: 'Unknown' } } )
  }
}

export default handler
