import axios from 'axios'
import * as apiInterface from '../../source/ApiInterface'

test( 'Should get version from API', async () => {
  const testVersionConst = { data: 'TestVersion' }
  const target = jest.spyOn( apiInterface, 'getApiVersion' )
  const mock = jest.spyOn( axios, 'get' ).mockResolvedValue( testVersionConst )

  expect( apiInterface.getApiVersion() ).resolves.toBe( testVersionConst.data )
  expect( target ).toHaveBeenCalledTimes( 1 )

  mock.mockRestore()
} )

export {}
