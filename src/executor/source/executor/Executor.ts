import { executeFilterNode } from './nodes/FilterNode'
import { executeInputNode } from './nodes/InputNode'
import { executeRequestNode } from './nodes/RequestNode'
import { Dataflow, DataflowInput, LinearizedDataflow, Node, Edge } from '../Dataflow'
import { linearize } from './Linearization'

/**
 * @param dataflow to execute
 * @param input from the request triggering the dataflow execution.
 * @returns the data to be returned to the request triggering the dataflow execution.
 */
export async function execute( dataflow: Dataflow, input: DataflowInput ): Promise<any> {
  const linearizedDataflow = linearize( dataflow )

  const results: Record<string, any> = linearizedDataflow
    .linearized
    .reduce( ( acc, cur ) => Object.assign( acc, { [cur.id]: undefined } ), {} )

  for ( const node of linearizedDataflow.linearized ) {
    const inputs = linearizedDataflow
      .edges
      .filter( e => e.toNode.id === node.id )
      .map( e => ( { [e.toHandle.id]: results[e.fromNode.id] } ) )
      .reduce( ( acc, cur ) => Object.assign( acc, cur ), {} )
    results[node.id] = await executeNode( node, inputs, input )
  }

  return results // temporary, see issue #69

}

/**
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the result of the node
 */
async function executeNode( node: Node, inputs: Record<string, any>, dataflowInput: DataflowInput ): Promise<any> {
  switch( node.type ) {
    case 'httpIn':
      return executeInputNode( dataflowInput )
    case 'httpOut':
    case 'request':
      return executeRequestNode( node, inputs )
    case 'filter':
      return executeFilterNode( node, inputs )
    default:
      return Promise.reject( 'Node type not implemented' )
  }
}
