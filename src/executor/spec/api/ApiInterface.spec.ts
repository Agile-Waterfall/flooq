import * as ApiWrapper from '../../source/api/ApiWrapper'
import * as ApiInterface from '../../source/api/ApiInterface'
import { APILinearizedDataflow } from '../../source/Dataflow'

const defaultDataflow = {
  'id': 'de780797-b556-4122-b133-7a446f79b024',
  'name': 'Demo Flow #1',
  'status': 'Active',
  'lastEdited': '2022-03-05T13:45:12Z',
  'definition': '{"nodes":[{"id":"1","dragHandle":".custom-drag-handle","type":"httpIn","data":{"title":"Http Input","input":{"url":"https://executor.dataflow.ch/IJF9K2","method":"post","contentType":"application/json","sampleBody":"{}"},"incomingHandles":[],"outgoingHandles":[{"id":"11","name":"a"}]},"position":{"x":0,"y":0},"width":258,"height":352},{"id":"2","dragHandle":".custom-drag-handle","type":"filter","data":{"title":"Filter","filter":{"field":"tags","value":"secret","condition":"ne"},"incomingHandles":[{"id":"21","name":"a"}],"outgoingHandles":[{"id":"21","name":"a"}]},"position":{"x":400,"y":100},"width":258,"height":258},{"id":"3","dragHandle":".custom-drag-handle","type":"httpOut","data":{"title":"Http Output","output":{"url":"","method":"post","contentType":"application/json","sampleBody":"{}"},"incomingHandles":[{"id":"3a","name":"a"}],"outgoingHandles":[]},"position":{"x":800,"y":0},"width":258,"height":445}],"edges":[{"id":"e1-2","source":"1","sourceHandle":"11","target":"2","targetHandle":"2a","animated":"true","type":"default"},{"id":"e2-3","source":"2","sourceHandle":"21","target":"3","targetHandle":"3a","animated":"true","type":"default"}]}'
}

const defaultDataflowID = 'DATAFLOW_ID'
const defaultError = '404: Error'
const defaultApiVersion = 'DBVersion -1.99'
const defaultLinearizedDataflow: APILinearizedDataflow = {
  id: defaultDataflowID,
  dataflow: 'STRINGIFIED_LINEARIZED_DATAFLOW'
}

const spy = jest.spyOn( ApiWrapper, 'get' )

afterEach( () => {
  spy.mockReset()
} )

test( 'getting a dataflow', async () => {
  spy.mockResolvedValue( defaultDataflow )

  ApiInterface.getDataflow( defaultDataflowID )
  expect( spy ).toBeCalledTimes( 1 )
  expect( spy ).toBeCalledWith( `DataFlow/${defaultDataflowID}` )

  spy.mockResolvedValue( defaultDataflow )
  expect( ApiInterface.getDataflow( defaultDataflowID ) ).resolves.toEqual( defaultDataflow )

  spy.mockRejectedValue( defaultError )
  expect( ApiInterface.getDataflow( defaultDataflowID ) ).rejects.toEqual( defaultError )
} )

test( 'getting version', async () => {
  spy.mockResolvedValue( defaultApiVersion )

  ApiInterface.getApiVersion( )
  expect( spy ).toBeCalledTimes( 1 )
  expect( spy ).toBeCalledWith( `version` )

  spy.mockResolvedValue( defaultApiVersion )
  expect( ApiInterface.getApiVersion() ).resolves.toEqual( defaultApiVersion )

  spy.mockRejectedValue( defaultError )
  expect( ApiInterface.getApiVersion() ).rejects.toEqual( defaultError )
} )

test( 'getting linearized dataflow', async () => {
  spy.mockResolvedValue( defaultLinearizedDataflow )

  ApiInterface.getLinearizedDataflow( defaultDataflowID )
  expect( spy ).toBeCalledTimes( 1 )
  expect( spy ).toBeCalledWith( `LinearizedGraph/${defaultDataflowID}` )

  spy.mockResolvedValue( defaultLinearizedDataflow )
  expect( ApiInterface.getLinearizedDataflow( defaultDataflowID ) ).resolves.toEqual( defaultLinearizedDataflow )

  spy.mockRejectedValue( defaultError )
  expect( ApiInterface.getLinearizedDataflow( defaultDataflowID ) ).rejects.toEqual( defaultError )
} )
