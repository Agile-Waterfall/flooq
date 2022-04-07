import axios from 'axios'
import { Dataflow, DataflowInput, FilterNode, JavacriptNode, LinearizedDataflow, Node, RequestNode, Edge } from '../Dataflow'

/**
 * @param dataflow to execute
 * @param input from the request triggering the dataflow execution.
 * @returns the data to be returned to the request triggering the dataflow execution.
 */
export async function execute( dataflow: Dataflow, input: DataflowInput ): Promise<any> {
  return executeLinearized( linearize( dataflow ), input )
}

/**
 * @param dataflow to order in such a way that a iterative execution of the nodes
 * is possible where all inputs are calculated at the time of the execution
 * @returns the linearized dataflow
 */
export function linearize( dataflow: Dataflow ): LinearizedDataflow {
  // add nodes that don't have any edges to the start
  const linearized: Node[] = dataflow
    .nodes
    .filter( e => dataflow.edges
      .map( f => [f.fromNode.id, f.toNode.id] )
      .flat()
      .indexOf( e.id ) < 0
    )
  let edges: Edge[] = dataflow.edges
  let noIncoming: Node[] = []

  // iteratively remove nodes that don't have any incoming edges and the edges going out from them.
  do {
    // nodes that appear in fromNode, but not in toNode
    noIncoming = edges
      .map( e => e.fromNode )
      .filter( e => edges
        .map( f => f.toNode.id )
        .indexOf( e.id ) < 0 )
    linearized.push( ...noIncoming )
    // remove edges that originate in one of those
    edges = edges
      .filter( e => noIncoming
        .map( f => f.id )
        .indexOf( e.fromNode.id ) < 0 )
  } while( noIncoming.length > 0 )

  // if not all edges were removed, there exists a circular dependencies.
  if ( edges.length > 0 ) throw new Error( 'could not linearize' )

  return Object.assign( dataflow, { linearized } )
}

/**
 * @param dataflow to execute
 * @param input from the request triggering the dataflow execution.
 * @returns the data to be returned to the request triggering the dataflow execution.
 */
async function executeLinearized ( dataflow: LinearizedDataflow, input: DataflowInput ): Promise<any> {
  const results: Record<string, any> = dataflow
    .linearized
    .reduce( ( acc, cur ) => Object.assign( acc, { [cur.id]: undefined } ), {} )

  for ( const node of dataflow.linearized ) {
    const inputs = dataflow
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
      return executeInitialNode( node, dataflowInput )
    case 'httpOut':
    case 'script':
    case 'request':
      return executeRequestNode( node, inputs )
    case 'filter':
      return executeFilterNode( node, inputs )
    default:
      return Promise.reject( 'Node type not implemented' )
  }
}

/**
 * Executes a HTTP request.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the response from the request
 */
async function executeRequestNode( node: RequestNode, inputs: Record<string, any> ): Promise<any> {
  const mergedInputs = Object.assign( {}, ...Object.values( inputs ) )
  const config = {
    url: node.data.url || mergedInputs.url,
    method: node.data.method || mergedInputs.method,
    headers: node.data.header || mergedInputs.header,
    data: node.data.body || mergedInputs.body,
  }
  return axios( config )
}

/**
 * Filters an array of objects by an attribute.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values.
 *               Must only have one entry. The entry must be an array of objects.
 * @returns the filtered array
 */
function executeFilterNode( node: FilterNode, inputs: Record<string, Record<string, any>[]> ): Record<string, any>[] {
  if ( Object.values( inputs ).length !== 1 ) throw new Error( 'Filter node can only handle one input' )
  switch( node.data.condition ) {
    case 'ne':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] !== node.data.filterValue )
    case 'eq':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] === node.data.filterValue )
    case 'gt':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] > node.data.filterValue )
    case 'lt':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] < node.data.filterValue )
    case 'ge':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] >= node.data.filterValue )
    case 'le':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] <= node.data.filterValue )
    case 'nn':
      return Object.values( inputs )[0].filter( e => e[node.data.fieldName] )
    case 're':
      return Object.values( inputs )[0]
        .filter( e => new RegExp( node.data.filterValue ).test( e[node.data.fieldName] ) )
  }
}

function executeInitialNode( node: Node, dataflowInput: DataflowInput ): any {
  return dataflowInput
}
