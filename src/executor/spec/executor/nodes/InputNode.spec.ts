import { executeInputNode } from '../../../source/executor/nodes/InputNode'

test( 'InitialNode returns input', () => {
  const data = {
    body: 'a',
    query: 'b',
    method: 'GET'
  }
  expect( executeInputNode( data ) ).toEqual( data )
} )
