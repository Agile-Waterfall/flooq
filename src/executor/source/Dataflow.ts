export interface APIDataflowResponse {
    id: string;
    name: string;
    status: string;
    lastEdited: string;
    definition: string;
}

export interface APIGraphResponse {
    id: string;
    graph: string;
}

// Data passed to the initial node of a dataflow, collected from the request triggering the execution
export interface DataflowInput {
    method: string;
    body: any;
    query: any
}

export interface Dataflow {
    nodes: Node<any>[];
    edges: Edge[];
}

export interface LinearizedDataflow extends Dataflow {
    linearized: Node<any>[];
}

export type NodeType = 'httpIn' | 'httpOut' | 'script';

export interface Node<T> {
    id: string;
    type: NodeType
    data: {
      params: T,
      incomingHandles: Handle[];
      outgoingHandles: Handle[];
    }
}

export interface Edge {
    id: string;
    fromNode: string;
    fromHandle: string;
    toNode: string;
    toHandle: string;
}

export interface Handle {
    name: string;
    id: string;
}

