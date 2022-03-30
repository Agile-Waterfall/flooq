import axios from 'axios'
import * as apiInterface from '../source/ApiInterface'

const OLD_ENV = process.env

beforeEach( () => {
  jest.resetModules()
  process.env = { ...OLD_ENV }
} )

afterAll( () => {
  process.env = OLD_ENV
} )

test( 'Testing version-getter function', async () => {
  const testVersionConst = { data: 'TestVersion' }
  const target = jest.spyOn( apiInterface, 'getApiVersion' )
  const mock = jest.spyOn( axios, 'get' ).mockResolvedValue( testVersionConst )

  expect( apiInterface.getApiVersion() ).resolves.toBe( testVersionConst.data )
  expect( target ).toHaveBeenCalledTimes( 1 )

  mock.mockRestore()
} )

test( 'Testing version-getter function with development environment', async () => {
  const testVersionConst = { data: 'TestVersion' }
  const target = jest.spyOn( apiInterface, 'getApiVersion' )
  const mock = jest.spyOn( axios, 'get' ).mockResolvedValue( testVersionConst )
  process.env.NODE_ENV = 'development'

  expect( apiInterface.getApiVersion() ).resolves.toBe( testVersionConst.data )
  expect( target ).toHaveBeenCalledTimes( 1 )

  mock.mockRestore()
} )

export {}
