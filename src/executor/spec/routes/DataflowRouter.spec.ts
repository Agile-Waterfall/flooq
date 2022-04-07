import * as apiInterface from '../../source/api/ApiInterface'
import * as Executor from '../../source/executor/Executor'
import request from 'supertest'
import app, { server } from '../../source/Index'

const defaultDataflow = {
  id: 'de780797-b556-4122-b133-7a446f79b024',
  name: 'Demo Flow #1',
  status: 'Active',
  lastEdited: '2022-03-05T13:45:12Z',
  definition: '{"nodes":[{"id":"1","dragHandle":".custom-drag-handle","type":"httpIn","data":{"title":"Http Input","input":{"url":"https://executor.dataflow.ch/IJF9K2","method":"post","contentType":"application/json","sampleBody":"{}"},"incomingHandles":[],"outgoingHandles":[{"id":"11","name":"a"}]},"position":{"x":0,"y":0},"width":258,"height":352},{"id":"2","dragHandle":".custom-drag-handle","type":"filter","data":{"title":"Filter","filter":{"field":"tags","value":"secret","condition":"ne"},"incomingHandles":[{"id":"21","name":"a"}],"outgoingHandles":[{"id":"21","name":"a"}]},"position":{"x":400,"y":100},"width":258,"height":258},{"id":"3","dragHandle":".custom-drag-handle","type":"httpOut","data":{"title":"Http Output","output":{"url":"","method":"post","contentType":"application/json","sampleBody":"{}"},"incomingHandles":[{"id":"3a","name":"a"}],"outgoingHandles":[]},"position":{"x":800,"y":0},"width":258,"height":445}],"edges":[{"id":"e1-2","source":"1","sourceHandle":"11","target":"2","targetHandle":"2a","animated":"true","type":"default"},{"id":"e2-3","source":"2","sourceHandle":"21","target":"3","targetHandle":"3a","animated":"true","type":"default"}]}'
}

const defaultInvalidDataflowID = 'asdf'
const defaultParams = { a: 'b', c: 'd' }


const mockAPI = jest.spyOn( apiInterface, 'getDataflow' )
  .mockImplementation( ( id ) => {
    if ( id !== defaultDataflow.id ) return Promise.reject()
    else return Promise.resolve( defaultDataflow )
  } )

const mockExecutor = jest.spyOn( Executor, 'execute' )
  .mockImplementation( ( df, i ) => Promise.resolve( { df, i } ) )


test( 'rejects dataflow with no ID', async () => {
  await request( app ).get( '/flow' ).expect( 404 )
  await request( app ).get( '/flow/' ).expect( 404 )
} )

test( 'requests dataflow from API', async () => {
  const result = await request( app ).get( `/flow/${defaultDataflow.id}` )
  expect( mockAPI ).toBeCalledWith( defaultDataflow.id )
} )

test( 'returns 500 on API connection issue', async () => {
  const result = await request( app ).get( `/flow/${defaultInvalidDataflowID}` ).expect( 500 )
  expect( result.body ).toHaveProperty( 'message' )
} )

test ( 'parses dataflow definition', async ( ) => {
  const result = await request( app ).get( `/flow/${defaultDataflow.id}` )
  expect( result.body.df ).toEqual( JSON.parse( defaultDataflow.definition ) )
} )

test( 'forwards the method', async () => {
  expect( ( await request( app ).get( `/flow/${defaultDataflow.id}` ) ).body.i.method ).toEqual( 'GET' )
  expect( ( await request( app ).post( `/flow/${defaultDataflow.id}` ) ).body.i.method ).toEqual( 'POST' )
  expect( ( await request( app ).put( `/flow/${defaultDataflow.id}` ) ).body.i.method ).toEqual( 'PUT' )
  expect( ( await request( app ).delete( `/flow/${defaultDataflow.id}` ) ).body.i.method ).toEqual( 'DELETE' )
} )

test( 'forwards query params', async () => {
  const result = await request( app ).get( `/flow/${defaultDataflow.id}` ).query( defaultParams )
  expect ( result.body.i.query ).toEqual( defaultParams )
} )

test( 'forwards body params', async () => {
  const result = await request( app ).post( `/flow/${defaultDataflow.id}` ).send( defaultParams )
  expect( result.body.i.body ).toEqual( defaultParams )
} )

test( 'returns 500 on Executor issue', async () => {
  mockExecutor.mockRejectedValueOnce( 'Error during execution' )
  const result = await request( app ).get( `/flow/${defaultDataflow.id}` ).expect( 500 )
  expect( result.body ).toHaveProperty( 'error' )
} )
