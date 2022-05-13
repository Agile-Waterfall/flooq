const handler = async ( _req: any, res: any ): Promise<void> => {

  let status = 'not responding'
  let version = { tag: 'unknown', name: 'unknown', notes: 'unknown' }

  try {
    const statusResponse = await fetch( `${process.env.API_BASE_URL}/api/status` )
    status = await statusResponse.text()
  } catch ( e ) {
    console.error( e )
  }

  try {
    const versionResponse = await fetch( `${process.env.API_BASE_URL}/api/version` )
    version = await versionResponse.json()
  } catch ( e ) {
    console.error( e )
  }

  res.status( 200 ).json( { status, version } )
}

export default handler
