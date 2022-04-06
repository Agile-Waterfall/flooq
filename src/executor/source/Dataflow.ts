import { AxiosRequestHeaders, Method } from 'axios'

export interface APIDataflowResponse {
    id: string;
    name: string;
    status: string;
    lastEdited: string;
    definition: string;
}

export interface Dataflow {
    name: string; // user-defined, possibly part of the domain (e.g. executor.flooq.io/u/<username>/<dataflowName>)
    initialNode: Node; // where the request to the executer first inputs the structure.
    status: string; // active/…
    lastEdited: string;
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

export interface RequestNode extends Node {
    data: {
        url: string;
        method: Method;
        header: any;
        body: any;
    }
}
export interface FilterNode extends Node {
    data: {
        fieldName: string;
        condition: 'ne' | 'eq' | 'gt' | 'lt' | 'ge' | 'le' | 'nn' | 're'
        filterValue: string;
    }
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

