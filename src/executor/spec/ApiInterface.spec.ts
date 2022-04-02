import axios, { AxiosRequestConfig } from 'axios'
import MockAdapter from 'axios-mock-adapter'
import * as apiInterface from '../source/ApiInterface'

const mock = new MockAdapter( axios )

test( 'Testing get version from api success', async () => {
  const testVersionConst = 'TestVersion'

  mock.onGet().replyOnce( 200, testVersionConst )

  expect( await apiInterface.getApiVersion() ).toBe( testVersionConst )

  mock.reset()
} )

test( 'Testing get version from api 500 error', async () => {
  mock.onGet().replyOnce( 500 )
  const target = jest.spyOn( apiInterface, 'getApiVersion' )

  expect( apiInterface.getApiVersion() ).rejects.toThrow()
  expect( target ).toBeCalled()

  mock.reset()
} )

test( 'Testing get version from api unknown error', async () => {
  const target = jest.spyOn( apiInterface, 'getApiVersion' )
  const mock = jest.spyOn( axios, 'get' ).mockImplementation( ( url: string, config?: AxiosRequestConfig | undefined ) => {
    throw new Error( 'TestingError' )
  } )

  expect( apiInterface.getApiVersion() ).rejects.toThrow()
  expect( target ).toBeCalled()

  mock.mockRestore()
} )


export {}
