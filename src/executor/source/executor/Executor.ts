import { executeHttpOutputNode } from './nodes/HttpOutputNode'
import { executeHttpInputNode } from './nodes/HttpInputNode'
import { DataflowInput, LinearizedDataflow, Node } from '../Dataflow'
import { executeScriptNode } from './nodes/ScriptNode'

/**
 * @param input from the request triggering the dataflow execution.
 * @param linearizedDataflow to execute
 * @returns the data to be returned to the request triggering the dataflow execution.
 */
export async function execute( input: DataflowInput, linearizedDataflow: LinearizedDataflow ): Promise<any> {
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
      .map( e => ( { [e.toHandle]: results[e.fromNode][e.toHandle] } ) )
      .reduce( ( acc, cur ) => ( { ...acc, ...cur } ), {} )

    results[node.id] = await executeNode( node, inputs )
  }
  return results // temporary, see issue #69

}

const nodeExecutions = [
  { type: 'httpIn', execute: executeHttpInputNode },
  { type: 'httpOut', execute: executeHttpOutputNode },
  { type: 'script', execute: executeScriptNode }
]

/**
 * @param node to execute
 * @param input of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the result of the node
 */
async function executeNode( node: Node<any>, input: any ): Promise<any> {
  return nodeExecutions.find( n => n.type === node.type )?.execute( node, input )
}
