import { getDataFieldNameForMethod } from '../../source/request/HttpHelpers'

describe( 'HttpHelpers', () => {
  it ( 'should return "query" on "GET" and "DELETE"', () => {
    expect( getDataFieldNameForMethod( 'GET' ) ).toEqual( 'query' )
    expect( getDataFieldNameForMethod( 'DELETE' ) ).toEqual( 'query' )
  } )

  it ( 'should return "body" on "POST", "PATCH" and "PUT"', () => {
    expect( getDataFieldNameForMethod( 'POST' ) ).toEqual( 'body' )
    expect( getDataFieldNameForMethod( 'PATCH' ) ).toEqual( 'body' )
    expect( getDataFieldNameForMethod( 'PUT' ) ).toEqual( 'body' )
  } )
} )
