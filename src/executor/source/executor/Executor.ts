import { executeHttpOutputNode } from './nodes/HttpOutputNode'
import { executeHttpInputNode } from './nodes/HttpInputNode'
import { Dataflow, DataflowInput, Node } from '../Dataflow'
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

  const linearizedNodes = linearizedDataflow.linearized
  const inputNode = linearizedNodes.shift()
  if ( !inputNode ) {
    return
  }
  results[inputNode.id] = await executeNode( inputNode, input )

  for ( const node of linearizedNodes ) {
    const inputs = linearizedDataflow.edges
      .filter( e => e.toNode === node.id )
      .map( e => ( { [e.toHandle]: results[e.fromNode] } ) )
      .reduce( ( acc, cur ) => ( { ...acc, ...cur } ), {} )

    results[node.id] = await executeNode( node, inputs )
  }
  return results // temporary, see issue #69

}

const nodeExecutions = [
  { type: 'httpIn', execute: executeHttpInputNode },
  { type: 'httpOut', execute: executeHttpOutputNode }
]

/**
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the result of the node
 */
async function executeNode( node: Node<any>, input: any ): Promise<any> {
  return nodeExecutions.find( n => n.type === node.type )?.execute( node, input )
}
