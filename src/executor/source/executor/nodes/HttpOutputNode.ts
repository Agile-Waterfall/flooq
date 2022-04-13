import { Method } from 'axios'
import { Node } from '../../Dataflow'
import { webRequest } from '../../request/WebRequest'
import Logger from '../../utils/logging/Logger'

export interface HttpOutputNode {
    output: {
      url: string;
      method: Method;
      header: any;
      body: any;
  }
}

/**
 * Executes a HTTP request with config data stored in the node and the merged inputs attached in the body.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
export async function executeHttpOutputNode( node: Node<HttpOutputNode>, inputs: any ): Promise<any> {
  let body = {}

  try {
    body = JSON.parse( node.data.output.body )
  } catch( error ) {
    Logger.error( error )
  }

  const mergedInputs = Object.assign( {}, ...Object.values( inputs ) )
  return webRequest( {
    ...node.data.output,
    data: {
      ...body,
      ...mergedInputs
    }
  } )
}
