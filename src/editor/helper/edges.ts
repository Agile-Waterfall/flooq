export interface Handle {
  name: string;
  id: string;
}

export interface Edge {
  id: string;
  fromNode: string;
  fromHandle: string;
  toNode: string;
  toHandle: string;
}

export const toReactFlowEdge = ( edge: Edge ): any => ( {
  'id': edge.id,
  'source': edge.fromNode,
  'sourceHandle': edge.fromHandle,
  'target': edge.toNode,
  'targetHandle': edge.toHandle,
  'animated': 'true',
  'type': 'default'
} )

export const toFlooqEdge = ( edge: any ): Edge => ( {
  'id': edge.id,
  'fromNode': edge.source,
  'fromHandle': edge.sourceHandle,
  'toNode': edge.target,
  'toHandle': edge.targetHandle
} )
