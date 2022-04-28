import { Method, AxiosRequestHeaders } from 'axios'
import { Node } from '../../Dataflow'
import { getDataFieldNameForMethod } from '../../request/HttpHelpers'
import { webRequest } from '../../request/WebRequest'

export interface HttpOutputNode {
  url: string;
  method: Method;
  headers: AxiosRequestHeaders;
  body: any;
}

/**
 * Executes a HTTP request with config data stored in the node and the input attached in the body.
 *
 * If the body contains double curly brackets ("{{<some.object.path>}}"), this string is replaced by the content
 * of the input with the specified path. If the input is a primitive data type, it is transformed into an object
 * ({'input': <input>})
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
export async function executeHttpOutputNode( node: Node<HttpOutputNode>, inputs: Record<string, any> ): Promise<any> {
  if ( Object.keys( inputs ).length > 1 ) return Promise.reject( 'HTTP Output Node should only get 1 input' )
  if ( typeof node.data.params.body !== 'object' ) return Promise.reject( 'provided body must be an object' )
  const input = Object.values( inputs )[0]

  return webRequest( {
    url: node.data.params.url,
    method: node.data.params.method,
    headers: node.data.params.headers,
    [getDataFieldNameForMethod( node.data.params.method )]: {
      ...objectify( input, 'result' ),
      ...JSON.parse( replaceBody( JSON.stringify( node.data.params.body ), objectify( input, 'input' ) ) )
    }
  } )
}

function objectify ( maybeObj: any, key: string ): object {
  return typeof maybeObj === 'object' ? maybeObj : { [key]: maybeObj }
}

function replaceBody( body: string, input: Record<any, any> ): string {
  return body.replaceAll(
    /(['"]?)\{\{([^{}]+)\}\}(['"]?)/gm,
    ( fullMatch, _, path ) => JSON.stringify( input[path] )
  )
}
