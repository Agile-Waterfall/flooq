import { Node } from '../../Dataflow'

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
  // {a: {}, b: {}, c: {}}

  return input
}
