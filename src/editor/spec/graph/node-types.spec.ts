import { nodeTypes } from '../../components/graph/node-types'

describe( 'Node Types', () => {
  it( 'Can create nodes', () => {

    nodeTypes.forEach( type => {
      const node = type.create()

      expect( node.type ).toBe( type.type )
      expect( node.data.title ).toBe( type.data.title )
    } )
  } )
} )

export { }
