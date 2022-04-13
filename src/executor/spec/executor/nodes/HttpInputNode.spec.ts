import { Node } from '../../../source/Dataflow'
import { executeHttpInputNode, HttpInputNode } from '../../../source/executor/nodes/HttpInputNode'

describe( 'HttpInputNode', () => {
  it( 'should return the query on a GET request', async () => {
    const node: Node<HttpInputNode> = {
      id: '',
      type: 'httpIn',
      data: {
        input: {
          method: 'GET'
        }
      },
      incomingHandles: [],
      outgoingHandles: []
    }
    const data = {
      body: 'a',
      query: 'b',
      method: 'GET'
    }

    expect( await executeHttpInputNode( node, data ) ).toEqual( data.query )
  } )

  it( 'should return the body on a POST request', async () => {
    const node: Node<HttpInputNode> = {
      id: '',
      type: 'httpIn',
      data: {
        input: {
          method: 'POST'
        }
      },
      incomingHandles: [],
      outgoingHandles: []
    }
    const data = {
      body: 'a',
      query: 'b',
      method: 'GET'
    }

    expect( await executeHttpInputNode( node, data ) ).toEqual( data.body )
  } )

  it( 'should return the query if the node does not specify an input method', async () => {
    const node: Node<HttpInputNode> = {
      id: '',
      type: 'httpIn',
      data: {
        input: {}
      },
      incomingHandles: [],
      outgoingHandles: []
    }
    const data = {
      body: 'a',
      query: 'b',
      method: 'GET'
    }

    expect( await executeHttpInputNode( node, data ) ).toEqual( data.query )
  } )
} )
