import { Node } from '../../../source/Dataflow'
import { executeHttpOutputNode, HttpOutputNode } from '../../../source/executor/nodes/HttpOutputNode'
import * as WebRequest from '../../../source/request/WebRequest'

const defaultReturnFunction: () => any = () => Promise.resolve( 'Some data form an API' )
const defaultErrorFunction: () => any = () => Promise.reject( 'Some error message' )
const getRequestNode = ( data?: any ): Node<HttpOutputNode> => {
  return {
    data: {
      output: Object.assign( {
        url: '',
        method: 'get',
        header: undefined,
        body: undefined
      }, data ),
    },
    id: '',
    type: 'httpIn',
    incomingHandles: [],
    outgoingHandles: []
  }
}

const mock = jest.spyOn( WebRequest, 'webRequest' )

function setReturn( returnFunction: () => any ): () => any {
  let received: any
  mock.mockImplementation( ( data ) => {
    received = data
    return returnFunction()
  } )
  return () => received
}

afterEach( mock.mockReset )

describe( 'HttpOutputNode', () => {
  it( 'sends data', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: 'b' }
    executeHttpOutputNode( getRequestNode(), { 'Handle1': sentData } )
    expect( getReceived().data ).toEqual( sentData )
  } )

  it( 'merges data', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { handle1: { a: 'b' }, handle2: { c: 'd' } }
    const receivedData = { a: 'b', c: 'd' }

    executeHttpOutputNode( getRequestNode(), sentData )
    expect( getReceived().data ).toEqual( receivedData )
  } )

  it( 'configures request ', async () => {
    const getReceived = setReturn( defaultReturnFunction )
    const requestNode = getRequestNode()
    const config = requestNode.data.output
    executeHttpOutputNode( requestNode, {} )
    const sentData = getReceived()
    delete sentData.data
    expect( sentData ).toEqual( config )
  } )

  it( 'rethrows exception', async () => {
    const getReceived = setReturn( defaultErrorFunction )
    const errorMessage = await defaultErrorFunction().catch( ( e: any ) => e )
    expect( executeHttpOutputNode( getRequestNode(), {} ) ).rejects.toEqual( errorMessage )
  } )
} )
