export const getRequest = async ( req, url, headers ) => {
  return request( req, 'GET', url, headers )
}

export const deleteRequest = async ( req, url, headers ) => {
  return request( req, 'DELETE', url, headers )
}

export const postRequest = async ( req, url, headers, body ) => {
  return request( req, 'POST', url, headers, body )
}

export const putRequest = async ( req, url, headers, body ) => {
  return request( req, 'PUT', url, headers, body )
}

const request = async ( _req, method, url, headers, body ) => {
  return fetch( `${process.env.API_BASE_URL}${url}`,
    {
      method,
      headers: {
        ...headers
      },
      body
    }
  )
}
