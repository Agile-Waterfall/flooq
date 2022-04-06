import { Dataflow, Edge, LinearizedDataflow, Node } from '../../source/Dataflow'
import { linearize } from '../../source/executor/Executor'

test( 'Linearization of linear graph', () => {

  const testNodes: Node[] = [
    {
      type: 'httpIn',
      id: '1',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
    {
      type: 'filter',
      id: '2',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
    {
      type: 'httpOut',
      id: '3',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
  ]

  const testEdges: Edge[] = [
    {
      id: 'Edge1',
      fromHandle: {
        id: '',
        name: ''
      },
      toHandle: {
        id: '',
        name: ''
      },
      fromNode: testNodes[0],
      toNode: testNodes[1]
    },
    {
      id: 'Edge2',
      fromHandle: {
        id: '',
        name: ''
      },
      toHandle: {
        id: '',
        name: ''
      },
      fromNode: testNodes[1],
      toNode: testNodes[2]
    }
  ]

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
    initialNode: testNodes[0]
  }

  const linearizedFlow = linearize( testDataflow )

  expect( isLinearized( linearizedFlow ) ).toBe( true )

} )

test( 'Linearization of non-linear graph', () => {

  const testNodes: Node[] = [
    {
      type: 'httpIn',
      id: '2',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
    {
      type: 'request',
      id: '1',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
    {
      type: 'filter',
      id: '3',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
    {
      type: 'filter',
      id: '4',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
    {
      type: 'httpOut',
      id: '5',
      incomingHandles: [],
      outgoingHandles: [],
      data: ''
    },
  ]

  const testEdges: Edge[] = [
    {
      id: 'Edge1',
      fromHandle: {
        id: '',
        name: ''
      },
      toHandle: {
        id: '',
        name: ''
      },
      fromNode: testNodes[0],
      toNode: testNodes[2]
    },
    {
      id: 'Edge2',
      fromHandle: {
        id: '',
        name: ''
      },
      toHandle: {
        id: '',
        name: ''
      },
      fromNode: testNodes[1],
      toNode: testNodes[3]
    },
    {
      id: 'Edge3',
      fromHandle: {
        id: '',
        name: ''
      },
      toHandle: {
        id: '',
        name: ''
      },
      fromNode: testNodes[2],
      toNode: testNodes[3]
    },
    {
      id: 'Edge4',
      fromHandle: {
        id: '',
        name: ''
      },
      toHandle: {
        id: '',
        name: ''
      },
      fromNode: testNodes[3],
      toNode: testNodes[4]
    }
  ]

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
    initialNode: testNodes[0]
  }

  const linearizedFlow = linearize( testDataflow )

  expect( isLinearized( linearizedFlow ) ).toBe( true )

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
