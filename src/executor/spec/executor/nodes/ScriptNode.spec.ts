import { executeScriptNode, ScriptNode } from '../../../source/executor/nodes/ScriptNode'
import { Node } from '../../../source/Dataflow'

function getScriptNode( functionString: string, args: Record<string, any> ): Node<ScriptNode> {
  return {
    id: 'TestingId',
    type: 'script',
    data: {
      incomingHandles: [],
      outgoingHandles: [{ name: 'a', id: 'a' }],
      params: {
        function: getFunctionFromBody( functionString, args )
      }
    }
  }
}

const defaultUserTokens: Record<string, any> = {
  'testToken1': 'testToken1Value',
  'testToken2': 'testToken2Value'
}

function getFunctionFromBody( body: string, input: Record<string, any> ): string {
  return `const handler = (${Object.keys( input ).join( ', ' )}) => {\n${body}\n}`
}

describe ( 'ScriptNode', () => {
  it( 'returns constant value', () => {
    const args = {}
    const node = getScriptNode( 'return 2', args )
    expect( executeScriptNode( node, args, defaultUserTokens ) ).resolves.toStrictEqual( { 'a': 2 } )
  } )

  it( 'executes simple addition', () => {
    const args = { 'a': 2, 'b': 7 }
    const node = getScriptNode( 'return a + b', args )
    expect( executeScriptNode( node, args, defaultUserTokens ) ).resolves.toStrictEqual( { 'a': 9 } )
  } )

  it( 'executes multiline addition', () => {
    const args = {}
    const node = getScriptNode( 'const res = 3\nreturn res', args )
    expect( executeScriptNode( node, args, defaultUserTokens ) ).resolves.toStrictEqual( { 'a': 3 } )
  } )

  it( 'executes map function', () => {
    const a = [1, 2, 3, 4]
    const aRef = a.map( ( val ) => val + 1 )

    const args = { 'a': a }
    const node = getScriptNode( 'const res = a.map((val) => val+1)\nreturn res', args )

    expect( executeScriptNode( node, args, defaultUserTokens ) ).resolves.toStrictEqual( { 'a': aRef } )
  } )

  it( 'shouldnt execute syntactically incorrect code', () => {

    const args = { 'a': true, 'b': false }
    const node = getScriptNode( 'wenn(a und b)denn{\nreturn true\n}suscht{\nreturn false\n}', args )

    expect( executeScriptNode( node, args, defaultUserTokens ) ).rejects.toThrow()
  } )

  it( 'should enforce runtime limit', () => {
    const args = {}
    const node = getScriptNode( 'while(true){}', args )
    expect( executeScriptNode( node, args, defaultUserTokens ) ).rejects.toThrow()
  } )

  it( 'shouldnt be able to exit process', () => {
    const args = {}
    const node = getScriptNode( 'process.exit()', args )
    expect( executeScriptNode( node, args, defaultUserTokens ) ).rejects.toThrow()
  } )

  it( 'should return result in object without Handle ID', () => {
    const args = {}
    const node = getScriptNode( 'return 2', args )
    node.data.outgoingHandles = []
    expect( executeScriptNode( node, args, defaultUserTokens ) ).resolves.toStrictEqual( { } )
  } )

  it( 'should return token', () => {
    const args = {}
    const node = getScriptNode( 'return "{{token.testToken1}}"', args )
    expect( executeScriptNode( node, args, defaultUserTokens ) ).resolves.toStrictEqual( { 'a': 'testToken1Value' } )
  } )
} )
