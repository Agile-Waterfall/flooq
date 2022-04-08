import { Dataflow, DataflowInput, Edge, Node } from '../../source/Dataflow'
import { execute } from '../../source/executor/Executor'
import { getEdges, getNodes, getNodesFromTypes, isLinearized } from '../testingUtils/GraphUtils'
import * as Linearization from './../../source/executor/Linearization'
import * as InputNode from './../../source/executor/nodes/InputNode'
import * as FilterNode from './../../source/executor/nodes/FilterNode'
import * as RequestNode from './../../source/executor/nodes/RequestNode'

const defaultRequestNodeResponse = 'SOME API RESPONSE'
const defaultInputNodeResponse = [{ a: 1, b: 2 }, { a: 2, b: 2 }]

const linearizationSpy = jest.spyOn( Linearization, 'linearize' )
const inputNodeSpy = jest.spyOn( InputNode, 'executeInputNode' ).mockReturnValue( defaultInputNodeResponse )
const filterNodeSpy = jest.spyOn( FilterNode, 'executeFilterNode' )
const requestNodeSpy = jest.spyOn( RequestNode, 'executeRequestNode' ).mockResolvedValue( defaultRequestNodeResponse )

const defaultNodes = getNodesFromTypes( 'httpIn', 'filter', 'httpOut' )
const defaultDataflow: Dataflow = {
  nodes: defaultNodes,
  edges: getEdges( defaultNodes, [0, 1], [1, 2] ),
}

defaultDataflow.nodes[1].data = {
  fieldName: 'a',
  condition: 'ne',
  filterValue: 1
}

const defaultInput: DataflowInput = {
  method: '',
  body: undefined,
  query: undefined
}

afterEach( () => {
  linearizationSpy.mockClear()
  inputNodeSpy.mockClear()
  filterNodeSpy.mockClear()
  requestNodeSpy.mockClear()
} )

describe( 'Executor', () => {

  it ( 'linearizes incoming dataflow', async () => {
    expect( execute( defaultDataflow, defaultInput ) ).resolves
    expect( linearizationSpy ).toBeCalledWith( defaultDataflow )
  } )

  it( 'calls the first node with the inputs', async () => {
    await execute( defaultDataflow, defaultInput )
    expect( inputNodeSpy ).toBeCalledWith( defaultInput )
  } )

  it ( 'calls the second node with the output of the first node', async () => {
    const edgeID = defaultDataflow.nodes[1].incomingHandles[0].id
    await execute( defaultDataflow, defaultInput )
    expect( filterNodeSpy ).toBeCalledWith( defaultDataflow.nodes[1], { [edgeID]: defaultInputNodeResponse } )
  } )

  it ( 'calls the third node with the output of the second node', async () => {
    const edgeID = defaultDataflow.nodes[2].incomingHandles[0].id
    await execute( defaultDataflow, defaultInput )
    expect( requestNodeSpy ).toBeCalledWith( defaultDataflow.nodes[2], { [edgeID]: defaultInputNodeResponse.slice( 1 ) } )
  } )

  it ( 'returns a combination of all results', async () => {
    const res = {
      [defaultDataflow.nodes[0].id]: defaultInputNodeResponse,
      [defaultDataflow.nodes[1].id]: defaultInputNodeResponse.slice( 1 ),
      [defaultDataflow.nodes[2].id]: defaultRequestNodeResponse,
    }
    expect( execute( defaultDataflow, defaultInput ) ).resolves.toEqual( res )
  } )

  it ( 'rethrows errors during a node execution', () => {
    requestNodeSpy.mockRejectedValueOnce( defaultRequestNodeResponse )
    expect( execute( defaultDataflow, defaultInput ) ).rejects.toEqual( defaultRequestNodeResponse )
  } )

  it( 'rejects on unknown nodes', async () => {
    const dataflow: any = JSON.parse( JSON.stringify( defaultDataflow ) ) // deep copy
    dataflow.nodes[1].type = 'NotImplementedType'
    expect( execute( dataflow, defaultInput ) ).rejects.toMatch( /.+/ ) // contain any string
  } )
} )
