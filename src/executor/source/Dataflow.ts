import { Method } from 'axios'

export interface APIDataflowResponse {
    id: string;
    name: string;
    status: string;
    lastEdited: string;
    definition: string;
}

// Data passed to the initial node of a dataflow, collected from the request triggering the execution
export interface DataflowInput {
    method: string;
    body: any;
    query: any
}
export interface Dataflow {
    initialNode: Node; // where the request to the executer first inputs the structure.
    nodes: Node[];
    edges: Edge[];
}

export interface LinearizedDataflow extends Dataflow {
    linearized: Node[];
}

export interface Node {
    id: string; // unique id to find the node
    type: 'httpIn' | 'httpOut' | 'filter' | 'request'; // type of the node (e.g. httpIn, httpOut, script, etc.)
    data: any; // any data that is required by the node
    incomingHandles: Handle[];
    outgoingHandles: Handle[];
}
export interface Edge {
    id: string;
    fromNode: Node;
    fromHandle: Handle;
    toNode: Node;
    toHandle: Handle;
}

export interface Handle {
    name: string;
    id: string;
}

