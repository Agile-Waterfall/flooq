import { Dataflow, DataflowInput, Edge, Node } from '../../source/Dataflow'
import { execute } from '../../source/executor/Executor'
import { HttpInputNode } from '../../source/executor/nodes/HttpInputNode'
import { HttpOutputNode } from '../../source/executor/nodes/HttpOutputNode'
import { ScriptNode } from '../../source/executor/nodes/ScriptNode'
import * as Linearization from './../../source/executor/Linearization'
import * as Web from './../../source/request/WebRequest'

const linearizationSpy = jest.spyOn( Linearization, 'linearize' )
const webRequest = jest.spyOn( Web, 'webRequest' ).mockResolvedValue( { status: 200 } )

const httpInputNode: Node<HttpInputNode> = {
  id: '1',
  type: 'httpIn',
  data: {
    outgoingHandles: [{ name: 'a', id: 'a' }],
    incomingHandles: [],
    params: {
      method: 'POST'
    }
  }
}

const scriptNode: Node<ScriptNode> = {
  id: '3',
  type: 'script',
  data: {
    incomingHandles: [{ name: 'a', id: 'a' }],
    outgoingHandles: [{ name: 'b', id: 'b' }],
    params: {
      function: 'const handler = (a) => {\nreturn 2*a.num\n}'
    }
  }
}

const multipleOutputScriptNode: Node<ScriptNode> = {
  id: '5',
  type: 'script',
  data: {
    incomingHandles: [{ name: 'a', id: 'a' }],
    outgoingHandles: [{ name: 'b', id: 'b' }, { name: 'c', id: 'c' }],
    params: {
      function: 'const handler = (a) => {\nreturn 2*a.num\n}'
    }
  }
}

const httpOutputNodeA: Node<HttpOutputNode> = {
  id: '2',
  type: 'httpOut',
  data: {
    outgoingHandles: [],
    incomingHandles: [{ name: 'a', id: 'a' }],
    params: {
      url: 'http://localhost:8080/xyz',
      method: 'POST',
      headers: {},
      body: '{}'
    }
  },
}

const httpOutputNodeB: Node<HttpOutputNode> = {
  id: '4',
  type: 'httpOut',
  data: {
    outgoingHandles: [],
    incomingHandles: [{ name: 'b', id: 'b' }],
    params: {
      url: 'http://localhost:8080/xyz',
      method: 'POST',
      headers: {},
      body: '{}'
    }
  },
}

describe( 'Executor', () => {
  it( 'should execute an empty data flow', async () => {

    const dataFlow: Dataflow = {
      nodes: [],
      edges: []
    }
    const input: DataflowInput = {
      method: 'GET',
      query: '',
      body: {}
    }

    const linearisedGraph = Linearization.linearize ( dataFlow )

    expect( linearizationSpy ).toBeCalledWith( dataFlow )

    const result = await execute( input, linearisedGraph )

    expect( result ).toBeUndefined()
  } )

  it( 'should execute a data flow with only a single input node', async () => {
    const dataFlow: Dataflow = {
      nodes: [httpInputNode],
      edges: []
    }
    const input: DataflowInput = {
      method: 'POST',
      query: '',
      body: { hello: 'world' }
    }

    const linearisedGraph = Linearization.linearize( dataFlow )

    expect( linearizationSpy ).toBeCalledWith( dataFlow )

    const result = await execute( input, linearisedGraph )

    expect( result ).not.toBeUndefined()
    expect( result[httpInputNode.id] ).toStrictEqual( { 'a': input.body } )
  } )

  it( 'should execute a data flow with input and output node', async () => {
    const edge: Edge = {
      id: 'EDGE 1',
      fromNode: '1',
      toNode: '2',
      fromHandle: 'a',
      toHandle: 'a'
    }

    const dataFlow: Dataflow = {
      nodes: [httpInputNode, httpOutputNodeA],
      edges: [edge]
    }
    const input: DataflowInput = {
      method: 'POST',
      query: '',
      body: { hello: 'world' }
    }

    const linearisedGraph = Linearization.linearize( dataFlow )

    expect( linearizationSpy ).toBeCalledWith( dataFlow )

    const result = await execute( input, linearisedGraph )

    expect( result ).not.toBeUndefined()
    expect( result[httpInputNode.id] ).toStrictEqual( { 'a': input.body } )
    expect( webRequest ).toBeCalledWith( {
      data: input.body,
      headers: {},
      method: 'POST',
      url: 'http://localhost:8080/xyz',
    } )
  } )

  it( 'should execute a data flow with input, script and output node', async () => {
    const edge: Edge = {
      id: 'EDGE 1',
      fromNode: '1',
      toNode: '3',
      fromHandle: 'a',
      toHandle: 'a'
    }

    const edge2: Edge = {
      id: 'EDGE 2',
      fromNode: '3',
      toNode: '2',
      fromHandle: 'b',
      toHandle: 'b'
    }

    const dataFlow: Dataflow = {
      nodes: [httpInputNode, scriptNode, httpOutputNodeA],
      edges: [edge, edge2]
    }
    const input: DataflowInput = {
      method: 'POST',
      query: '',
      body: { num: 2 }
    }

    const linearisedGraph = Linearization.linearize( dataFlow )

    expect( linearizationSpy ).toBeCalledWith( dataFlow )

    const result = await execute( input, linearisedGraph )

    expect( result ).not.toBeUndefined()
    expect( result[httpInputNode.id] ).toStrictEqual( { 'a': input.body } )
    expect( webRequest ).toBeCalledWith( {
      data: { result: input.body.num * 2 },
      headers: {},
      method: 'POST',
      url: 'http://localhost:8080/xyz',
    } )
  } )

  it( 'should execute a data flow with input, script and output node with multipe outputHandles', async () => {
    const edge: Edge = {
      id: 'EDGE 1',
      fromNode: '1',
      toNode: '5',
      fromHandle: 'a',
      toHandle: 'a'
    }

    const edge2: Edge = {
      id: 'EDGE 2',
      fromNode: '5',
      toNode: '2',
      fromHandle: 'b',
      toHandle: 'b'
    }

    const edge3: Edge = {
      id: 'EDGE 3',
      fromNode: '5',
      toNode: '4',
      fromHandle: 'c',
      toHandle: 'c'
    }

    const dataFlow: Dataflow = {
      nodes: [httpInputNode, multipleOutputScriptNode, httpOutputNodeA, httpOutputNodeB],
      edges: [edge, edge2, edge3]
    }
    const input: DataflowInput = {
      method: 'POST',
      query: '',
      body: { num: 2 }
    }

    const linearisedGraph = Linearization.linearize( dataFlow )

    expect( linearizationSpy ).toBeCalledWith( dataFlow )

    const result = await execute( input, linearisedGraph )

    expect( result ).not.toBeUndefined()
    expect( result[httpInputNode.id] ).toStrictEqual( { 'a': input.body } )
    expect( webRequest ).toBeCalledWith( {
      data: { result: input.body.num * 2 },
      headers: {},
      method: 'POST',
      url: 'http://localhost:8080/xyz',
    } )
    expect( webRequest ).toBeCalledTimes( 2 )
  } )

  it( 'should nod execute anything for an unknown node type', async () => {

    const nodeWithWrongType: Node<any> = {
      id: '1',
      // @ts-ignore
      type: 'WRONG',
      data: {
        outgoingHandles: [{ name: 'a', id: 'a' }],
        incomingHandles: [],
        params: {}
      }
    }

    const dataFlow: Dataflow = {
      nodes: [nodeWithWrongType],
      edges: []
    }
    const input: DataflowInput = {
      method: 'POST',
      query: '',
      body: { hello: 'world' }
    }
    const linearisedGraph = Linearization.linearize( dataFlow )

    expect( linearizationSpy ).toBeCalledWith( dataFlow )

    const result = await execute( input, linearisedGraph )

    expect( result ).not.toBeUndefined()
    expect( result[httpInputNode.id] ).toBe( undefined )
  } )
} )
