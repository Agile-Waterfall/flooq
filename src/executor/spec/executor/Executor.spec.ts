import { Dataflow, Edge, Handle, LinearizedDataflow, Node } from '../../source/Dataflow'
import { linearize } from '../../source/executor/Executor'


test( 'Linearization of linear graph', () => {

  const testNodes: Node[] = getNodes( 3 )

  const testEdges: Edge[] = getEdges(
    testNodes,
    [0, 1],
    [1, 2]
  )

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
    initialNode: testNodes[0]
  }

  const linearizedFlow = linearize( testDataflow )

  expect( isLinearized( linearizedFlow ) ).toBe( true )

} )

test( 'Linearization of non-linear graph', () => {

  const testNodes: Node[] = getNodes( 2 )

  const testEdges: Edge[] = getEdges( testNodes, [0, 1], [1, 0] )

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
    initialNode: testNodes[0]
  }

  expect( () => linearize( testDataflow ) ).toThrow()

} )
test( 'Linearization of non-linear graph with two input-nodes', () => {

  const testNodes: Node[] = getNodes( 5 )

  const testEdges: Edge[] = getEdges(
    testNodes,
    [0, 3],
    [1, 2],
    [2, 3],
    [3, 4]
  )

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
    initialNode: testNodes[0]
  }

  const linearizedFlow = linearize( testDataflow )

  expect( isLinearized( linearizedFlow ) ).toBe( true )

} )

test( 'Linearization of circular graph with one input node', () => {

  const testNodes: Node[] = getNodes( 5 )

  const testEdges: Edge[] = getEdges(
    testNodes,
    [0, 1],
    [1, 2],
    [2, 3],
    [3, 4],
    [4, 1]
  )

  const testDataflow: Dataflow = {
    nodes: testNodes,
    edges: testEdges,
    initialNode: testNodes[0]
  }

  expect( () => linearize( testDataflow ) ).toThrow()
} )

function isLinearized( linearizedFlow: LinearizedDataflow ): boolean {
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

function getNodes( n: number ): Node[] {
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

function getEdges( nodes: Node[], ...connections: [number, number][] ): Edge[] {
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
