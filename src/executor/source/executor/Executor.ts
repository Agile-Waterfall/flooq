import axios from 'axios'
import { Dataflow, FilterNode, LinearizedDataflow, Node, RequestNode } from '../Dataflow'

export async function execute( dataflow: Dataflow, input: any ): Promise<any> {
  return executeLinearized( linearize( dataflow ), input )
}

function linearize( dataFlow: Dataflow ): LinearizedDataflow {
  throw new Error( 'not implemented' )
}

async function executeLinearized ( dataflow: LinearizedDataflow, input: any ): Promise<any> {
  if ( !dataflow.linearized ) return Promise.reject( 'dataflow not linearized' )

  const results: Record<string, any> = dataflow
    .linearized
    .reduce( ( acc, cur ) => Object.assign( acc, { [cur.id]: undefined } ), {} )

  results[dataflow.initialNode.id] = input

  for ( const node of dataflow.linearized.filter( e => e.id !== dataflow.initialNode.id ) ) {
    const inputs = dataflow
      .edges
      .filter( e => e.toNode.id === node.id )
      .map( e => ( { [e.toHandle.id]: results[e.fromNode.id] } ) )
      .reduce( ( acc, cur ) => Object.assign( acc, cur ), {} )
    results[node.id] = await executeNode( node, inputs )
  }

  return results // temporary, see issue #69
}

async function executeNode( node: Node, inputs: Record<string, any> ): Promise<any> {
  switch( node.type ) {
    case 'httpIn':
      return Promise.reject( 'Should handle httpIn node outside this function' )
    case 'httpOut':
    case 'request':
      return executeRequestNode( node, inputs )
    case 'filter':
      return executeFilterNode( node, inputs )
    default:
      return Promise.reject( 'Node type not implemented' )
  }
}

async function executeRequestNode( node: RequestNode, inputs: Record<string, any> ): Promise<any> {
  const mergedInputs =  Object.assign( {}, ...Object.values( inputs ) )
  const config = {
    url: node.data.url || mergedInputs.url,
    method: node.data.method || mergedInputs.method,
    headers: node.data.header|| mergedInputs.header,
    data: node.data.body || mergedInputs.body,
  }
  return axios( config )
}

function executeFilterNode( node: FilterNode, inputs: Record<string, Record<string, any>[]> ): any {
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
      return Object.values( inputs )[0].filter( e => new RegExp( node.data.filterValue ).test( e[node.data.fieldName] ) )
  }
}
