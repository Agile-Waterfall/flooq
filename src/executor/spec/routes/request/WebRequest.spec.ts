import axios, { AxiosRequestConfig, Method } from 'axios'
import MockAdapter from 'axios-mock-adapter'
import { config } from 'dotenv'
import { webRequest } from '../../../source/request/WebRequest'

const defaultResponse = 'This is a response'
const defaultErrorCode = 500
const defaultUnknownError = 'TestingError'

const getConfigForMethod = ( method: Method ): AxiosRequestConfig<any> => {
  return {
    method,
    url: 'https://flooq.io',
    data: { 'a': 'b', 'c': 1 },
    params: { 'd': 'e', 'f': 2 },
  }
}

const methods: Method[] = ['DELETE', 'GET', 'PATCH', 'POST', 'PUT']

const mock = new MockAdapter( axios )
mock.onAny().reply( 200, defaultResponse )


function getAllRequests(): AxiosRequestConfig<any>[] {
  return Object.values( mock.history ).flat( 1 )
}

describe( 'WebRequest', () => {
  afterEach( ( done ) => {
    mock.resetHistory()
    done()
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'data is passed', ( config ) => {
    webRequest( config )
    expect( getAllRequests().length ).toBe( 1 ) // times called
    expect( getAllRequests()[0].data ).toEqual( JSON.stringify( config.data ) )
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'params are passed', ( config ) => {
    webRequest( config )
    expect( getAllRequests().length ).toBe( 1 )
    expect( getAllRequests()[0].params ).toEqual( config.params )
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'url matches', ( config ) => {
    webRequest( config )
    expect( getAllRequests().length ).toBe( 1 )
    expect( getAllRequests()[0].url ).toEqual( config.url )
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'method matches', ( config ) => {
    webRequest( config )
    expect( getAllRequests().length ).toBe( 1 )
    expect( getAllRequests()[0].method ).toEqual( config.method?.toLowerCase() )
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'result matches', async( config ) => {
    const res = await webRequest( config )
    expect( getAllRequests().length ).toBe( 1 )
    expect( res.data ).toEqual( defaultResponse )
  } )
} )

describe( 'WebRequest error handling', () => {
  afterEach( done => {
    mock.reset()
    done()
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'networkErrors are passed', async( config ) => {
    mock.onAny().networkError()
    expect( () => webRequest( config ) ).rejects
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'aborted requests are passed', async( config ) => {
    mock.onAny().abortRequest()
    expect( () => webRequest( config ) ).rejects
  } )

  test.each( methods.map( e => [getConfigForMethod( e )] ) )( 'timeouts are passed', async( config ) => {
    mock.onAny().timeout()
    expect( () => webRequest( config ) ).rejects
  } )
} )
