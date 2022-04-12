import { Method } from 'axios'
import { Node } from '../../Dataflow'
import { webRequest } from '../../request/WebRequest'

export interface RequestNode extends Node {
  data: {
    output: {
      url: string;
      method: Method;
      header: any;
      body: any;
    }
  }
}

/**
 * Executes a HTTP request with config data stored in the node and the merged inputs attached in the body.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
export async function executeRequestNode( node: RequestNode, inputs: Record<string, any> ): Promise<any> {
  const mergedInputs = Object.assign( JSON.parse( node.data.output.body ), ...Object.values( inputs ) )
  return webRequest( Object.assign( node.data.output, { data: mergedInputs } ) )
}
