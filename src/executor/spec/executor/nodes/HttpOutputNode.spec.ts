import { Node } from '../../../source/Dataflow'
import { executeHttpOutputNode, HttpOutputNode } from '../../../source/executor/nodes/HttpOutputNode'
import * as WebRequest from '../../../source/request/WebRequest'

const defaultReturnFunction: () => any = () => Promise.resolve( 'Some data form an API' )
const defaultErrorFunction: () => any = () => Promise.reject( 'Some error message' )
const getRequestNode = ( data?: any ): Node<HttpOutputNode> => {
  return {
    data: {
      incomingHandles: [],
      outgoingHandles: [],
      params: Object.assign( {
        url: '',
        method: 'get',
        headers: undefined,
        body: '{}'
      }, data ),
    },
    id: '',
    type: 'httpIn',
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
  it( 'sends data', async () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: 'b' }
    await executeHttpOutputNode( getRequestNode(), { 'Handle1': sentData } )
    expect( getReceived().params ).toEqual( sentData )
  } )

  it ( 'sends node body', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const bodyData = { 'a': 'b' }
    executeHttpOutputNode( getRequestNode( { body: JSON.stringify( bodyData ) } ), { 'Handle1': {} } )
    expect( getReceived().params ).toEqual( bodyData )
  } )

  it ( 'replaces text', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: 'b' }
    executeHttpOutputNode( getRequestNode( {
      body: JSON.stringify( { replacedData: '{{a}}' } ) } ),
    { 'Handle1': sentData }
    )
    expect( getReceived().params ).toEqual( { 'replacedData': sentData.a } )
  } )

  it ( 'replaces objects', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: { b: 'c' } }
    executeHttpOutputNode( getRequestNode( { body: JSON.stringify( { replacedData: '{{a}}' } ) } ), { 'Handle1': sentData } )
    expect( getReceived().params ).toEqual( { 'replacedData': sentData.a } )
  } )

  it ( 'replaces undefined', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: { b: 'c' } }
    executeHttpOutputNode( getRequestNode( { body: JSON.stringify( { replacedData: '{{nonPresentKey}}' } ) } ), { 'Handle1': sentData } )
    expect( getReceived().params ).toEqual( { 'replacedData': 'undefined' } )
  } )

  it ( 'replaces text', () => {
    const getReceived = setReturn( defaultReturnFunction )
    const sentData = { a: 'b' }
    executeHttpOutputNode( getRequestNode( { body: JSON.stringify( { replacedData: '{{a}}' } ) } ), { 'Handle1': sentData } )
    expect( getReceived().params ).toEqual( { 'replacedData': sentData.a } )
  } )

  it( 'configures request ', async () => {
    const getReceived = setReturn( defaultReturnFunction )
    const requestNode = getRequestNode()
    const config = requestNode.data.params
    executeHttpOutputNode( requestNode, {} )
    const sentData = getReceived()
    expect( sentData.method ).toEqual( config.method )
    expect( sentData.url ).toEqual( config.url )
    expect( sentData.headers ).toEqual( config.headers )
    expect( sentData.params ).toBeDefined()
  } )

  it( 'configures method', async () => {
    const getReceived = setReturn( defaultReturnFunction )
    const requestNode = getRequestNode( { method: 'post' } )
    const config = requestNode.data.params
    executeHttpOutputNode( requestNode, {} )
    const sentData = getReceived()
    expect( sentData.method ).toEqual( config.method )
    expect( sentData.url ).toEqual( config.url )
    expect( sentData.headers ).toEqual( config.headers )
    expect( sentData.data ).toBeDefined()
  } )

  it( 'rethrows exception', async () => {
    setReturn( defaultErrorFunction )
    const errorMessage = await defaultErrorFunction().catch( ( e: any ) => e )
    expect( executeHttpOutputNode( getRequestNode(), {} ) ).rejects.toEqual( errorMessage )
  } )

  it ( 'throws on multiple inputs', () => {
    setReturn( defaultErrorFunction )
    expect( executeHttpOutputNode( getRequestNode(), { Handle1: { a: 1 }, Handle2: { b: 2 } } ) ).rejects.toBeTruthy()
  } )

  it( 'throws if body is not an object', () => {
    setReturn( defaultErrorFunction )
    expect( executeHttpOutputNode( getRequestNode( { body: JSON.stringify( undefined ) } ), { Handle1: { a: 1 } } ) ).rejects.toBeTruthy()
  } )
} )
