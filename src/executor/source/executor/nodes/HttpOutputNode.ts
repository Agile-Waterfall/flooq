import { Method } from 'axios'
import { Node } from '../../Dataflow'
import { webRequest } from '../../request/WebRequest'
import Logger from '../../utils/logging/Logger'

export interface HttpOutputNode {
  url: string;
  method: Method;
  header: any;
  body: any;
}

/**
 * Executes a HTTP request with config data stored in the node and the merged inputs attached in the body.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
export async function executeHttpOutputNode( node: Node<HttpOutputNode>, input: Record<string, any> ): Promise<any> {
  let body = {}

  try {
    body = JSON.parse( node.data.params.body )
  } catch ( error ) {
    Logger.error( error )
  }

  const objInputs = Object.values( input ).map( ( val, ind ) => {
    if( typeof val === 'object' ) {
      return val
    } else {
      return { result: val }
    }
  } )

  const mergedInputs = Object.assign( {}, ...Object.values( objInputs ) )

  return webRequest( {
    ...node.data.params,
    data: {
      ...body,
      ...mergedInputs
    }
  } )
}
