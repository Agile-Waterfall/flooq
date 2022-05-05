import { DataflowInput, Node } from '../../Dataflow'

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
  console.log(input)
  const dataFieldName = ['GET', 'DELETE'].includes( node.data.params.method?.toUpperCase() || 'GET' ) ? 'query' : 'body'
  if(node.data.outgoingHandles.length > 0){
    const results = {}
    for (const outgoingHandle of node.data.outgoingHandles){
      Object.assign( results , { [outgoingHandle.id]: input[dataFieldName] } )
    }
    console.log(results)
    return results
  }
  return input[dataFieldName]
}
