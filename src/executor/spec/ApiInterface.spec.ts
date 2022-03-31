import axios from 'axios'
import MockAdapter from 'axios-mock-adapter'
import { getApiVersion } from '../source/ApiInterface'

const mock = new MockAdapter( axios )

test( 'Testing get version from api success', async () => {
  const testVersionConst = 'TestVersion'

  mock.onGet().replyOnce( 200, testVersionConst )

  const res = await getApiVersion()
  expect( res ).toBe( testVersionConst )

  mock.reset()
} )

test( 'Testing get version from api error', async () => {
  mock.onGet().replyOnce( 500 )

  expect( getApiVersion ).rejects.toThrow()

  mock.reset()
} )


export {}
