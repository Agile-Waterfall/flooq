import { Edge, toFlooqEdge, toReactFlowEdge } from '../helper/edges'

describe( 'Edges Helper', () => {
  it( 'can convert react-flow edge to flooq edge', () => {
    const reactFlowEdge = {
      id: '1',
      source: 's1',
      sourceHandle: 's1h',
      target: 't1',
      targetHandle: 't1h'
    }

    const flooqEdge = toFlooqEdge( reactFlowEdge )

    expect( flooqEdge.id ).toBe( '1' )
    expect( flooqEdge.fromNode ).toBe( 's1' )
    expect( flooqEdge.fromHandle ).toBe( 's1h' )
    expect( flooqEdge.toNode ).toBe( 't1' )
    expect( flooqEdge.toHandle ).toBe( 't1h' )
  } )

  it( 'can convert flooq edge to react-flow edge', () => {
    const flooqEdge: Edge = {
      id: '1',
      fromNode: 's1',
      fromHandle: 's1h',
      toNode: 't1',
      toHandle: 't1h'
    }

    const reactFlowEdge = toReactFlowEdge( flooqEdge )

    expect( reactFlowEdge.id ).toBe( '1' )
    expect( reactFlowEdge.source ).toBe( 's1' )
    expect( reactFlowEdge.sourceHandle ).toBe( 's1h' )
    expect( reactFlowEdge.target ).toBe( 't1' )
    expect( reactFlowEdge.targetHandle ).toBe( 't1h' )
  } )
} )
