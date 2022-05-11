import * as WebRequest from '../../source/request/WebRequest'
import * as ApiWrapper from '../../source/api/ApiWrapper'
import { AxiosError } from 'axios'
import * as Authentication from '../../source/request/Authentication'
import 'dotenv/config'

const defaultPath = 'dataflows'
const defaultData = JSON.stringify( { a: 1 } )
const defaultJWT = 'LINEARIZED_JWT'
const defaultResponse = {
  data: 'This is a response',
  status: 200,
  statusText: 'success',
  headers: {},
  config: {}
}
const defaultError = 'TestingError'

const webRequestMock = jest.spyOn( WebRequest, 'webRequest' ).mockResolvedValue( defaultResponse )
const authMock = jest.spyOn( Authentication, 'getJWT' ).mockResolvedValue( defaultJWT )

afterEach( ( ) => {
  webRequestMock.mockClear()
  authMock.mockClear()
} )

describe( 'ApiWrapper', () => {
  it( 'returns the correct value on success', async () => {
    let url
    webRequestMock.mockImplementationOnce( ( req ) => {
      url = req.url
      return Promise.resolve( defaultResponse )
    } )

    await expect( ApiWrapper.get( defaultPath ) ).resolves.toEqual( defaultResponse.data )
    expect( url ).toEqual( `${process.env.API_BASE_URL}/api/${defaultPath}` )
  } )

  it ( 'Applies JWT GET', async () => {
    await ApiWrapper.get( defaultPath )
    expect( webRequestMock ).toHaveBeenCalledWith( {
      method: 'GET',
      url: `${process.env.API_BASE_URL}/api/${defaultPath}`,
      headers: { Authorization: `Bearer ${defaultJWT}` }
    } )
    expect( authMock ).toHaveBeenCalled()
  } )

  it ( 'Applies JWT POST', async () => {
    await ApiWrapper.post( defaultPath, defaultData )
    expect( webRequestMock ).toHaveBeenCalledWith( {
      method: 'POST',
      url: `${process.env.API_BASE_URL}/api/${defaultPath}`,
      data: defaultData,
      headers: { Authorization: `Bearer ${defaultJWT}` }
    } )
    expect( authMock ).toHaveBeenCalled()
  } )

  it( 'rejects on a network error', async () => {
    webRequestMock.mockRejectedValue( defaultError )
    expect( ApiWrapper.get( defaultPath ) ).rejects.toBe( defaultError )
  } )

  it( 'rejects on a axios error', async () => {
    const error: AxiosError = {
      config: {},
      isAxiosError: true,
      toJSON: () => ( {} ),
      name: '',
      message: ''
    }
    webRequestMock.mockRejectedValue( error )
    expect( ApiWrapper.get( defaultPath ) ).rejects.toBeTruthy()
  } )
} )
