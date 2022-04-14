import * as WebRequest from '../../source/request/WebRequest'
import * as ApiWrapper from '../../source/api/ApiWrapper'
import { AxiosError } from 'axios'
import 'dotenv/config'

const defaultPath = 'dataflows'
const defaultResponse = 'This is a response'
const defaultError = 'TestingError'

const mock = jest.spyOn( WebRequest, 'webRequest' )
mock.mockResolvedValue( { data: defaultResponse } )

afterEach( ( done ) => {
  mock.mockReset()
  done()
} )

describe( 'ApiWrapper', () => {
  it( 'returns the correct value on success', async () => {
    let url
    mock.mockImplementation( ( req ) => {
      url = req.url
      return Promise.resolve( { data: defaultResponse } )
    } )

    await expect( ApiWrapper.get( defaultPath ) ).resolves.toEqual( defaultResponse )
    expect( url ).toEqual( `${process.env.API_BASE_URL}/api/${defaultPath}` )
  } )

  it( 'rejects on a network error', async () => {
    mock.mockRejectedValue( defaultError )
    expect( ApiWrapper.get( defaultPath ) ).rejects.toHaveProperty( 'message' )
  } )

  it( 'rejects on a axios error', async () => {
    const error: AxiosError = {
      config: {},
      isAxiosError: true,
      toJSON: () => ( {} ),
      name: '',
      message: ''
    }
    mock.mockRejectedValue( error )
    expect( ApiWrapper.get( defaultPath ) ).rejects.toHaveProperty( 'message' )
  } )
} )
