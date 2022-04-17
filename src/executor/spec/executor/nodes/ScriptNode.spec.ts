import { executeScriptNode, ScriptNode } from '../../../source/executor/nodes/ScriptNode'
import * as sn from '../../../source/executor/nodes/ScriptNode'
import { Node } from '../../../source/Dataflow'

function getScriptNode( functionString: string ): Node<ScriptNode> {
  return {
    id: 'TestingId',
    type: 'script',
    data: {
      incomingHandles: [],
      outgoingHandles: [],
      params: {
        function: functionString
      }
    }
  }
}

const mock = jest.spyOn( sn, 'executeScriptNode' )

afterEach( mock.mockReset )

describe ( 'ScriptNode', () => {
  it( 'returns constant value', () => {
    expect( executeScriptNode( getScriptNode( 'return 2' ), {} ) ).resolves.toBe( 2 )
  } )

  it( 'executes simple addition', () => {
    expect( executeScriptNode( getScriptNode( 'return a + b' ), { 'a': 2, 'b': 7 } ) ).resolves.toBe( 9 )
  } )

  it( 'executes multiline addition', () => {
    expect( executeScriptNode( getScriptNode( 'const res = 3\nreturn res' ), {} ) ).resolves.toBe( 3 )
  } )

  it( 'executes map function', () => {
    const a = [1, 2, 3, 4]
    const aRef = a.map( ( val ) => val + 1 )
    expect( executeScriptNode( getScriptNode( 'const res = a.map((val) => val+1)\nreturn res' ), { 'a': a } ) ).resolves.toStrictEqual( aRef )
  } )

  it( 'shouldnt execute syntactically incorrect code', () => {
    const a = [1, 2, 3, 4]
    const aRef = a.map( ( val ) => val + 1 )
    expect( executeScriptNode( getScriptNode( 'wenn(a und b)denn{\nreturn true\n}suscht{\nreturn false\n}' ), { 'a': true, 'b': false } ) ).rejects.toThrow()
  } )
} )
