import { DataflowInput, Node } from '../../Dataflow'
import { getDataFieldNameForMethod } from '../../request/HttpHelpers'

export interface HttpInputNode {
  method?: string;
}

/**
 * Executes the HTTP Input node. It handles the incoming data according to the defined node parameters.
 *
 * @param node to execute
 * @param input to be handled
 * @returns the data to execute the next node
 */
export const executeHttpInputNode = async ( node: Node<HttpInputNode>, input: DataflowInput ): Promise<any> => {
  return input[getDataFieldNameForMethod( node.data.params.method || 'GET' )]
}
