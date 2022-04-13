import { Edge, Handle, LinearizedDataflow, Node, NodeType } from '../../source/Dataflow'

export function isLinearized( linearizedFlow: LinearizedDataflow ): boolean {
  let acc = true
  linearizedFlow.linearized.forEach( ( curr, index ) => {
    const incomingEdges = linearizedFlow.edges.filter( ( edge ) => edge.toNode === curr.id )
    if ( incomingEdges.length > 0 ) {
      acc = acc && incomingEdges.map( incoming => {
        return linearizedFlow.linearized.findIndex( n => n.id === incoming.fromNode ) < index
      } ).reduce( ( curr, prev ) => curr && prev, true )
    }
  } )
  return acc
}

export function getNodesFromTypes( ...types: NodeType[] ): Node<any>[] {
  return types.map( ( type, i ) => ( {
    id: `Node: ${String( i )}`,
    data: '',
    incomingHandles: [],
    outgoingHandles: [],
    type
  } ) )
}

export function getNodes( n: number ): Node<any>[] {
  return getNodesFromTypes( ...Array( n ).fill( 'filter' ) )
}

export function getEdges( nodes: Node<any>[], ...connections: [number, number][] ): Edge[] {
  return connections.map( ( val, ind ) => {
    const fromHandle: Handle = {
      id: `fromHandle, fromNode: ${val[0]}, toNode: ${val[1]}, id: ${String( ind )}`,
      name: ''
    }
    const toHandle: Handle = {
      id: `toHandle, fromNode: ${val[0]}, toNode: ${val[1]}, id: ${String( ind )}`,
      name: ''
    }

    nodes[val[0]].outgoingHandles.push( fromHandle )
    nodes[val[1]].incomingHandles.push( toHandle )

    return {
      id: `Edge: ${String( ind )}`,
      fromNode: nodes[val[0]].id,
      toNode: nodes[val[1]].id,
      fromHandle: fromHandle.id,
      toHandle: toHandle.id
    }
  } )
}
