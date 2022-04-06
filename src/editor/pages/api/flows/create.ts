import { v4 as uuidv4 } from 'uuid'

const handler = async ( _req: any, res: any ): Promise<void> => {
  const date = new Date().toISOString()
  const id = uuidv4()

  const newFlow = {
    id: id,
    name: 'DataFlow-' + id.toString().substring( 0, 8 ),
    status: 'Active',
    lastEdited: date,
    definition: ''
  }

  const url = `${process.env.API_BASE_URL}/api/DataFlow`
  const request = {
    method: 'POST',
    headers: {
      'accept': 'text/plain',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify( newFlow )
  }

  const response = await fetch( url,  request )
  const payload = await response.json()

  res.status( response.status ).json( payload )
}

export default handler
