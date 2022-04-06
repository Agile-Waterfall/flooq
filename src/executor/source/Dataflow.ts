export interface Dataflow {
    name: string; // user-defined, possibly part of the domain (e.g. executor.flooq.io/u/<username>/<dataflowName>)
    initialNode: Node; // where the request to the executer first inputs the structure.
    status: string; // active/…
    lastEdited: string;
    nodes: Node[];
    edges: Edge[];
}

export interface Node {
    id: string; // unique id to find the node
    type: string; // type of the node (e.g. httpIn, httpOut, script, etc.)
    data: any; // any data that is required by the node
    run( input: any[] ): any[]; // to be executed. The input elements match the incoming handles, the output elements the outgoing handles. Storage/transmission of the data is to be implemented by the actual block implementations.
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

