import MockAdapter from 'axios-mock-adapter/types'
import { Dataflow } from '../../source/Dataflow'
import * as ApiWrapper from '../../source/api/ApiWrapper'
import * as ApiInterface from '../../source/api/ApiInterface'

const defaultDataflow: Dataflow | undefined = undefined
const defaultDataflowID = 'DATAFLOW_ID'
const defaultUsername = 'some_username'
const defaultError = '404: Error'
const defaultApiVersion = 'DBVersion -1.99'

const spy = jest.spyOn( ApiWrapper, 'get' )


afterEach( () => {
  spy.mockReset()
} )


test( 'getting a dataflow', async () => {
  spy.mockResolvedValue( defaultDataflow )

  ApiInterface.getDataFlow( defaultUsername, defaultDataflowID )
  expect( spy ).toBeCalledTimes( 1 )
  expect( spy ).toBeCalledWith( `/${defaultUsername}/${defaultDataflowID}` )

  spy.mockResolvedValue( defaultDataflow )
  expect( ApiInterface.getDataFlow( defaultUsername, defaultDataflowID ) ).resolves.toEqual( defaultDataflow )

  spy.mockRejectedValue( defaultError )
  expect( ApiInterface.getDataFlow( defaultUsername, defaultDataflowID ) ).rejects.toEqual( defaultError )
} )

test( 'getting version', async () => {
  spy.mockResolvedValue( defaultApiVersion )

  ApiInterface.getApiVersion( )
  expect( spy ).toBeCalledTimes( 1 )
  expect( spy ).toBeCalledWith( `/version` )

  spy.mockResolvedValue( defaultApiVersion )
  expect( ApiInterface.getApiVersion() ).resolves.toEqual( defaultApiVersion )

  spy.mockRejectedValue( defaultError )
  expect( ApiInterface.getApiVersion() ).rejects.toEqual( defaultError )
} )
