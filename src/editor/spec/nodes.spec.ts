import { FlooqNode } from '../components/graph/node'
import { updateNodeParameter } from '../helper/nodes'

describe( 'Nodes Helper', () => {
  it( 'should update node parameters for a given id', () => {
    const node: FlooqNode = {
      id: '1',
      data: {
        title: 'Any Node',
        incomingHandles: [],
        outgoingHandles: [],
        params: {}
      },
      position: { x: 0, y: 0 }
    }

    const newParams = { hello: 'world' }
    const updatedNode = updateNodeParameter( node, '1', newParams )

    expect( updatedNode.data.params ).toBe( newParams )
  } )

  it( 'should not update node parameters if id does not match', () => {
    const node: FlooqNode = {
      id: '1',
      data: {
        title: 'Any Node',
        incomingHandles: [],
        outgoingHandles: [],
        params: {}
      },
      position: { x: 0, y: 0 }
    }

    const newParams = { hello: 'world' }
    const updatedNode = updateNodeParameter( node, '2', newParams )

    expect( updatedNode.data.params ).toBe( node.data.params )
  } )

} )
