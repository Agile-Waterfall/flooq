import * as apiInterface from '../../source/api/ApiInterface'
import request from 'supertest'
import app, { server } from '../../source/Index'

afterEach( ( ) => {
  server.close()
} )


test( 'Testing version-endpoint', async () => {
  const testVersionConst = 'TestVersion'
  const mock = jest.spyOn( apiInterface, 'getApiVersion' ).mockResolvedValue( testVersionConst )

  const result = await request( app ).get( '/version' )

  expect( result.status ).toBe( 200 )
  expect( result.text ).toBe( testVersionConst )
  mock.mockRestore()
} )

test( 'Testing version-endpoint error', async () => {
  const mock = jest.spyOn( apiInterface, 'getApiVersion' ).mockImplementation( () => {
    throw new Error( 'TestError' )
  } )

  const result = await request( app ).get( '/version' )

  expect( result.status ).toBe( 500 )
  mock.mockRestore()
} )
