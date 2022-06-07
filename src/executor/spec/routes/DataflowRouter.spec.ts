import * as apiInterface from '../../source/api/ApiInterface'
import * as Executor from '../../source/executor/Executor'
import request from 'supertest'
import app from '../../source/Index'
import { APILinearizedDataflow, LinearizedDataflow } from '../../source/Dataflow'
import * as Linearization from '../../source/executor/Linearization'

const defaultDataflow = {
  id: 'de780797-b556-4122-b133-7a446f79b024',
  name: 'Demo Flow #1',
  status: 'Active',
  lastEdited: '2022-03-05T13:45:12Z',
  definition: '{"nodes":[{"id":"1","dragHandle":".custom-drag-handle","type":"httpIn","data":{"title":"Http Input","input":{"url":"https://executor.dataflow.ch/IJF9K2","method":"post","contentType":"application/json","sampleBody":"{}"},"incomingHandles":[],"outgoingHandles":[{"id":"11","name":"a"}]},"position":{"x":0,"y":0},"width":258,"height":352},{"id":"2","dragHandle":".custom-drag-handle","type":"filter","data":{"title":"Filter","filter":{"field":"tags","value":"secret","condition":"ne"},"incomingHandles":[{"id":"21","name":"a"}],"outgoingHandles":[{"id":"21","name":"a"}]},"position":{"x":400,"y":100},"width":258,"height":258},{"id":"3","dragHandle":".custom-drag-handle","type":"httpOut","data":{"title":"Http Output","output":{"url":"","method":"post","contentType":"application/json","sampleBody":"{}"},"incomingHandles":[{"id":"3a","name":"a"}],"outgoingHandles":[]},"position":{"x":800,"y":0},"width":258,"height":445}],"edges":[{"id":"e1-2","source":"1","sourceHandle":"11","target":"2","targetHandle":"2a","animated":"true","type":"default"},{"id":"e2-3","source":"2","sourceHandle":"21","target":"3","targetHandle":"3a","animated":"true","type":"default"}]}'
}
const defaultLinearizedDataflowAPI: APILinearizedDataflow = {
  id: 'de780797-b556-4122-b133-7a446f79b024',
  graph: ''
}
const defaultLinearizedDataflow: LinearizedDataflow = {
  linearized: [],
  nodes: [],
  edges: []
}

const defaultInvalidDataflowID = 'asdf'

const mockAPI = jest.spyOn( apiInterface, 'getDataflow' )
  .mockImplementation( ( id ) => {
    if ( id !== defaultDataflow.id ) return Promise.reject()
    else return Promise.resolve( defaultDataflow )
  } )

const mockAPIGetLinearized = jest.spyOn( apiInterface, 'getLinearizedDataflow' )
  .mockImplementation( ( id ) => {
    if ( id !== defaultDataflow.id ) return Promise.reject()
    else return Promise.resolve( defaultLinearizedDataflowAPI )
  } )

const mockAPIPostLinearized = jest.spyOn( apiInterface, 'postLinearizedDataflow' )

const mockExecutor = jest.spyOn( Executor, 'execute' )
  .mockImplementation( ( i, ldf ) => Promise.resolve( { i, ldf } ) )

const mockLinearization = jest.spyOn( Linearization, 'linearize' )
  .mockImplementation ( () => {
    return defaultLinearizedDataflow
  } )

describe( 'DataFlow Router', () => {
  it( 'rejects dataflow with no ID', async () => {
    await request( app ).get( '/flow' ).expect( 404 )
    await request( app ).get( '/flow/' ).expect( 404 )
  } )

  it( 'requests dataflow from API', async () => {
    await request( app ).get( `/flow/${defaultDataflow.id}` )
    expect( mockAPI ).toBeCalledWith( defaultDataflow.id )
    expect( mockAPIGetLinearized ).toBeCalledWith( defaultDataflow.id )
  } )

  it( 'returns 400 on API connection issue', async () => {
    const result = await request( app ).get( `/flow/${defaultInvalidDataflowID}` ).expect( 404 )
    expect( result.body ).toHaveProperty( 'message' )
  } )

  it( 'returns 500 on Executor issue', async () => {
    mockExecutor.mockRejectedValueOnce( 'Error during execution' )
    const result = await request( app ).get( `/flow/${defaultDataflow.id}` ).expect( 500 )
    expect( result.body ).toHaveProperty( 'error' )
  } )

  it( 'linearizes and posts dataflow if no linearized version is returned from API', async () => {
    mockAPIGetLinearized.mockRejectedValueOnce( 'No linearized dataflow' )
    await request( app ).get( `/flow/${defaultDataflow.id}` )
    expect( mockAPI ).toBeCalledWith( defaultDataflow.id )
    expect( mockAPIGetLinearized ).toBeCalledWith( defaultDataflow.id )
    expect( mockLinearization ).toHaveBeenCalledWith( JSON.parse( defaultDataflow.definition ) )
    expect( mockLinearization ).not.toThrow()
    expect( mockAPIPostLinearized ).toBeCalledTimes( 1 )
  } )

  it( 'linearizes and posts dataflow if no linearized version is returned from API', async () => {
    mockAPIGetLinearized.mockRejectedValueOnce( 'No linearized dataflow' )
    mockLinearization.mockImplementation( () => {
      throw new Error( ' could not linearize ' )
    } )
    await request( app ).get( `/flow/${defaultDataflow.id}` ).expect( 500 )
    expect( mockLinearization ).toHaveBeenCalledWith( JSON.parse( defaultDataflow.definition ) )
    expect( mockAPIPostLinearized ).toBeCalledTimes( 0 )
  } )
} )
