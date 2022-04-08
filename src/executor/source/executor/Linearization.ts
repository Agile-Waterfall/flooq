import { Dataflow, Edge, LinearizedDataflow, Node } from '../Dataflow'

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
