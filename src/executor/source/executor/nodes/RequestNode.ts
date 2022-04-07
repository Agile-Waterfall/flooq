import axios, { Method } from 'axios'
import { Node } from '../../Dataflow'

export interface RequestNode extends Node {
  data: {
      url: string;
      method: Method;
      header: any;
      body: any;
  }
}

/**
 * Executes a HTTP request.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
export async function executeRequestNode( node: RequestNode, inputs: Record<string, any> ): Promise<any> {
  const mergedInputs = Object.assign( {}, ...Object.values( inputs ) )
  const config = {
    url: node.data.url || mergedInputs.url,
    method: node.data.method || mergedInputs.method,
    headers: node.data.header || mergedInputs.header,
    data: node.data.body || mergedInputs.body,
  }
  return axios( config )
}
