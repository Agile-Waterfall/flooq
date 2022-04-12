import { executeRequestNode, RequestNode } from '../../../source/executor/nodes/RequestNode'
import * as WebRequest from '../../../source/request/WebRequest'

const defaultReturnFunction: () => any = () => Promise.resolve( 'Some data form an API' )
const defaultErrorFunction: () => any = () => Promise.reject( 'Some error message' )
const getRequestNode = ( data?: any ): RequestNode => {
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

describe( 'RequestNode', () => {
  it( 'sends data', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: 'b' }
    executeRequestNode( getRequestNode(), { 'Handle1': sentData } )
    expect( getReceived().data ).toEqual( sentData )
  } )

  it( 'merges data', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { handle1: { a: 'b' }, handle2: { c: 'd' } }
    const receivedData = { a: 'b', c: 'd' }

    executeRequestNode( getRequestNode(), sentData )
    expect( getReceived().data ).toEqual( receivedData )
  } )

  it( 'configures request ', async () => {
    const getReceived = setReturn( defaultReturnFunction )
    const requestNode = getRequestNode()
    const config = requestNode.data.output
    executeRequestNode( requestNode, {} )
    const sentData = getReceived()
    delete sentData.data
    expect( sentData ).toEqual( config )
  } )

  it( 'rethrows exception', async () => {
    const getReceived = setReturn( defaultErrorFunction )
    const errorMessage = await defaultErrorFunction().catch( ( e: any ) => e )
    expect( executeRequestNode( getRequestNode(), {} ) ).rejects.toEqual( errorMessage )
  } )
} )
