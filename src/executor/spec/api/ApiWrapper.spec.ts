import axios, { AxiosRequestConfig } from 'axios'
import MockAdapter from 'axios-mock-adapter'
import * as ApiWrapper from '../../source/api/ApiWrapper'
import Logger from '../../source/utils/logging/Logger'

const mock = new MockAdapter( axios )

const defaultPath = '/'
const defaultResponse = 'This is a response'
const defaultErrorCode = 500
const defaultUnknownError = 'TestingError'

afterEach( ( done ) => {
  mock.reset()
  done()
} )

it( 'returns the correct value on success',async () =>{
  mock.onGet().reply( 200, defaultResponse )
  expect( ApiWrapper.get( defaultPath ) ).resolves.toEqual( defaultResponse )
} )

it( 'rejects on a axios error', async () => {
  mock.onGet().reply( defaultErrorCode )
  expect( ApiWrapper.get( defaultPath ) ).rejects.toContain( defaultErrorCode.toString() )
} )

it( 'rejects on a unknown error', () => {
  const mock = jest.spyOn( axios, 'get' ).mockImplementation( ( url: string, config?: AxiosRequestConfig | undefined ) => {
    throw new Error( defaultUnknownError  )
  } )
  expect( ApiWrapper.get( defaultPath ) ).rejects.toThrow( defaultUnknownError )
} )
