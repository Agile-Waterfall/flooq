import { DataflowInput, Node } from '../../Dataflow'

export interface HttpInputNode {
  input: {
    method?: string;
  }
}

/**
 * Executes the HTTP Input node. It handles the incoming data according to the defined node parameters.
 *
 * @param node to execute
 * @param input to be handled
 * @returns the data to execute the next node
 */
export const executeHttpInputNode = async ( node: Node<HttpInputNode>, input: DataflowInput ): Promise<any> => {
  const method = node.data.input.method || 'GET'
  switch ( method.toUpperCase() ) {
    case 'GET':
    case 'DELETE':
      return input.query
    case 'POST':
    case 'PUT':
    case 'PATCH':
      return input.body
  }
}
