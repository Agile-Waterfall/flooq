import { Method } from 'axios'
import { Node } from '../../Dataflow'
import { webRequest } from '../../request/WebRequest'
import Logger from '../../utils/logging/Logger'

export interface RequestNode extends Node {
  data: {
    // The output object has been added by me (saarofel)
    // because the HttpOutput node uses this object to store the data.
    // This is subject to change!
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
  let body = {}

  try {
    body = JSON.parse( node.data.output.body )
  } catch( error ) {
    Logger.error( error )
  }

  const mergedInputs = Object.assign( body, ...Object.values( inputs ) )
  return webRequest( Object.assign( node.data.output, { data: mergedInputs } ) )
}
