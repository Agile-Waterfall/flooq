import { executeInitialNode } from '../../../source/executor/nodes/InputNode'

test( 'InitialNode returns input', () => {
  const data = {
    body: 'a',
    query: 'b',
    method: 'GET'
  }
  expect( executeInitialNode( {
    id: 'id',
    type: 'httpIn', // type of the node (e.g. httpIn, httpOut, script, etc.)
    data: '', // any data that is required by the node
    incomingHandles: [],
    outgoingHandles: []
  }, data ) ).toEqual( data )
} )
