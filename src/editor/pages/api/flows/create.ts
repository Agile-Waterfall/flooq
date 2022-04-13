import { v4 as uuidv4 } from 'uuid'

const handler = async ( _req: any, res: any ): Promise<void> => {

  const newFlow = {
    name: 'DataFlow-' + uuidv4().toString().substring( 0, 8 )
  }

  const url = `${process.env.API_BASE_URL}/api/DataFlow`
  const request = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify( newFlow )
  }

  const response = await fetch( url, request )
  const payload = await response.json()

  res.status( response.status ).json( payload )
}

export default handler
