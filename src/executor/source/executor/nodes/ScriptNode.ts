import { Node } from '../../Dataflow'
import { VM } from 'vm2'

export interface ScriptNode {
  function: string
}

/**
 * Executes JavaScript function from a node.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values
 * @returns the output of the JavaScript function
 */
export async function executeScriptNode( node: Node<ScriptNode>, input: Record<string, any> ): Promise<any> {
  const vm = new VM( {
    timeout: 1000,
    allowAsync: false,
    sandbox: { inputs: Object.values( input ) }
  } )
  const result = vm.run( `${node.data.params.function}\nhandler(...inputs)` )
  if ( node.data.outgoingHandles.length > 0 ){
    const results = {}
    for ( const outgoingHandle of node.data.outgoingHandles ){
      Object.assign( results, { [outgoingHandle.id]: result } )
    }
    return Promise.resolve( results )
  }
  return Promise.resolve( result )
}
