import { Condition, executeFilterNode, FilterNode } from '../../../source/executor/nodes/FilterNode'

const defaultKey = 'key'

test( 'test not equal', () => {
  expect( runFilterNode( defaultKey, 'ne', 'removeMe',
    gOA( 'leaveMe', 'removeMe', 'leaveMe', 'removeMe', 'leaveMe' )
  ) ).toEqual( gOA( 'leaveMe', 'leaveMe', 'leaveMe' ) )
} )

test( 'test equal', () => {
  expect( runFilterNode( defaultKey, 'eq', 'removeMe',
    gOA( 'leaveMe', 'removeMe', 'leaveMe', 'removeMe', 'leaveMe' )
  ) ).toEqual( gOA( 'removeMe', 'removeMe' ) )
} )

test( 'test greater than', () => {
  expect( runFilterNode( defaultKey, 'gt', '1',
    gOA( '-1', '0', '1', '2', '3' )
  ) ).toEqual( gOA( '2', '3' ) )
} )

test( 'test less than', () => {
  expect( runFilterNode( defaultKey, 'lt', '1',
    gOA( '-1', '0', '1', '2', '3' )
  ) ).toEqual( gOA( '-1', '0' ) )
} )

test( 'test greater or equal than', () => {
  expect( runFilterNode( defaultKey, 'ge', '1',
    gOA( '-1', '0', '1', '2', '3' )
  ) ).toEqual( gOA( '1', '2', '3' ) )
} )

test( 'test less or equal than', () => {
  expect( runFilterNode( defaultKey, 'le', '1',
    gOA( '-1', '0', '1', '2', '3' )
  ) ).toEqual( gOA( '-1', '0', '1' ) )
} )

test( 'test not null or empty', () => {
  expect( runFilterNode( defaultKey, 'nn', 'ignored',
    gOA( '', null, undefined, 0, 'a', 2 )
  ) ).toEqual( gOA( 'a', 2 ) )
} )

test( 'test regex', () => {
  expect( runFilterNode( defaultKey, 're', '\\d\\w\\d',
    gOA( '0a0', '1_3', '11', '1a', 'b3', 'a0a0', 'a000aa000a' )
  ) ).toEqual( gOA( '0a0', '1_3', 'a0a0', 'a000aa000a' ) )
} )

test( 'throws on multiple inputs', () => {
  expect( () => executeFilterNode( {
    id: 'nodeID',
    type: 'filter',
    data: {
      fieldName: 'fieldName',
      condition: 'lt',
      filterValue: 'filterValue'
    },
    incomingHandles: [],
    outgoingHandles: []

  },
  { 'a': [{ 'a': 'b' }], 'b': [{ 'a': 'b' }] } )
  ).toThrow()
} )


// getObjectArray
function gOA( ...values: any[] ): Record<string, any>[] {
  return values.map( e => ( { [defaultKey]: e } ) )
}

function runFilterNode(
  fieldName: string,
  condition: Condition,
  filterValue: string,
  input: Record<string, any>[]
): Record<string, any>[]{
  return executeFilterNode( {
    id: 'nodeID',
    type: 'filter',
    data: {
      fieldName,
      condition,
      filterValue
    },
    incomingHandles: [],
    outgoingHandles: []
  }, { a: input } )
}


