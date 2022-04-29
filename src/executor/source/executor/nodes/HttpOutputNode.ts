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

/**
 * Wraps the first param in an object, if it isn't already.
 * @param maybeObj to possibly wrap
 * @param key of the wrapping object
 * @returns an object
 */
function objectify ( maybeObj: any, key: string ): object {
  return typeof maybeObj === 'object' ? maybeObj : { [key]: maybeObj }
}

/**
 * Replaces all occurrences of `"{{<path>}}"` in `str` with the stringified object in `data` at the `<path>`.
 *
 * Ignores white-spaces to the immediate left and right of `<path>` and single and double quotes immediately
 * outside the double brackets.
 *
 * @param str to replace in
 * @param data to replace with
 * @returns str with all replacements
 */
function replaceBody( str: string, data: Record<any, any> ): string {
  return str.replaceAll(
    /['"]?\{\{\w*([^{}]+)\w*\}\}['"]?/gm,
    ( _fullMatch, path ) => JSON.stringify( data[path] || 'undefined' )
  )
}
