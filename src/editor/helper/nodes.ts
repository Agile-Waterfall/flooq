import { FlooqNode } from '../components/graph/node'

export const updateNodeParameter = ( node: FlooqNode, id: string, params: any ): FlooqNode => {
  if ( node.id !== id ) return node
  return {
    ...node,
    data: {
      ...node.data,
      params
    }
  }
}
