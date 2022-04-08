import { Dataflow, Edge, Node } from '../../source/Dataflow'
import { linearize } from '../../source/executor/Linearization'
import { getEdges, getNodes, isLinearized } from '../testingUtils/GraphUtils'

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
  }

  expect( () => linearize( testDataflow ) ).toThrow()
} )

test( 'Linearization of non-linear graph with x-crossover', () => {

  const testNodes: Node[] = getNodes( 6 )

  const testEdges: Edge[] = getEdges(
    testNodes,
    [0, 1],
    [0, 2],
    [1, 4],
    [1, 3],
    [2, 3],
    [2, 4],
    [3, 5],
    [4, 5]
  )

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
  }

  expect( isLinearized( linearize( testDataflow ) ) ).toBe( true )

} )

test( 'Linearization of non-linear graph with edge to self', () => {

  const testNodes: Node[] = getNodes( 1 )

  const testEdges: Edge[] = getEdges( testNodes, [0, 0] )

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
  }

  expect( () => linearize( testDataflow ) ).toThrow()

} )

test( 'Linearization of graph with two loose nodes', () => {

  const testNodes: Node[] = getNodes( 2 )

  const testEdges: Edge[] = getEdges( testNodes )

  const testDataflow: Dataflow = {
    edges: testEdges,
    nodes: testNodes,
  }

  expect( isLinearized( linearize( testDataflow ) ) ).toBe( true )

} )
