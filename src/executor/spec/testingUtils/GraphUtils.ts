import { Edge, Handle, LinearizedDataflow, Node } from '../../source/Dataflow'

export function isLinearized( linearizedFlow: LinearizedDataflow ): boolean {
  let acc = true
  linearizedFlow.linearized.forEach( ( curr, index ) => {
    const incomingEdges = linearizedFlow.edges.filter( ( edge ) => edge.toNode.id === curr.id )
    if ( incomingEdges.length > 0 ) {
      acc = acc && incomingEdges.map( incoming => {
        return linearizedFlow.linearized.findIndex( n => n.id === incoming.fromNode.id ) < index
      } ).reduce( ( curr, prev ) => curr && prev, true )
    }
  } )
  return acc
}

export function getNodes( n: number ): Node[] {
  const res: Node[] = []
  for ( let i = 0; i < n; i++ ) {
    res.push( {
      id: `Node: ${String( i )}`,
      data: '',
      incomingHandles: [],
      outgoingHandles: [],
      type: 'filter'
    } )
  }
  return res
}

export function getEdges( nodes: Node[], ...connections: [number, number][] ): Edge[] {
  const res: Edge[] = connections.map( ( val, ind ) => {
    const fromHandle: Handle = {
      id: `fromHandle, fromNode: ${String( val[0] )}, toNode: ${String( val[1] )}, id: ${String( ind )}`,
      name: ''
    }
    const toHandle: Handle = {
      id: `toHandle, fromNode: ${String( val[0] )}, toNode: ${String( val[1] )}, id: ${String( ind )}`,
      name: ''
    }
    nodes[val[0]].outgoingHandles.push( fromHandle )
    nodes[val[1]].incomingHandles.push( toHandle )
    return {
      id: `Edge: ${String( ind )}`,
      fromNode: nodes[val[0]],
      toNode: nodes[val[1]],
      fromHandle: fromHandle,
      toHandle: toHandle
    }
  } )
  return res
}
