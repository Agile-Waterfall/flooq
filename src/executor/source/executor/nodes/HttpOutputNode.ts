import { Method, AxiosRequestHeaders } from 'axios'
import { Node } from '../../Dataflow'
import { webRequest } from '../../request/WebRequest'

export interface HttpOutputNode {
  url: string;
  method: Method;
  headers: AxiosRequestHeaders;
  body: string;
}

/**
 * Executes a HTTP request with config data stored in the node.
 *
 * If the node body is empty, the whole node input is transmitted as the request body. Otherwise, the node body is.
 *
 * If the node body contains double curly brackets ("{{<some.object.path>}}"), this string is replaced by the content
 * of the node input with the specified path. If the input is a primitive data type, it is transformed into an object
 * ({'input': <input>})
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
export async function executeHttpOutputNode( node: Node<HttpOutputNode>, inputs: Record<string, any> ): Promise<any> {
  if ( Object.keys( inputs ).length > 1 ) return Promise.reject( 'HTTP Output Node should only get 1 input' )
  const input = Object.values( inputs )[0]
  const dataFieldName = ['GET', 'DELETE'].includes( node.data.params.method.toUpperCase() ) ? 'params' : 'data'

  const body = JSON.parse( replaceBody( node.data.params.body, objectify( input, 'input' ) ) )

  return webRequest( {
    url: node.data.params.url,
    method: node.data.params.method,
    headers: node.data.params.headers,
    [dataFieldName]: Object.keys( body ).length > 0 ? body : objectify( input, 'result' ),
  } )
}

function objectify ( maybeObj: any, key: string ): object {
  return typeof maybeObj === 'object' ? maybeObj : { [key]: maybeObj }
}

function replaceBody( body: string, input: Record<any, any> ): string {
  return body.replaceAll(
    /['"]?\{\{\w*([^{}]+)\w*\}\}['"]?/gm,
    ( _fullMatch, path ) => JSON.stringify( input[path] || 'undefined' )
  )
}
