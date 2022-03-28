# Dataflow

This file documents all matters concerning the actual dataflows.

## Structure

A dataflow is a [directed (potentially circular) graph](https://en.wikipedia.org/wiki/Directed_graph) of nodes (that perform some kind of action on the input data and possibly return some output) and edges (which connect the nodes).

Each node has input and output handles, to which the edges connect. Each incoming handle can only be connected to one incoming edge (use some merging block before otherwise). Outgoing handles can spawn multiple outgoing edges, each of which gets a copy of the output.

### Typescript

```typescript
interface Dataflow {
  name: string; // user-defined, possibly part of the domain (e.g. dataflowname.username.executor.flooq.io)
  initialNode: Node; // where the request to the executer first inputs the structure.
  nodes: Node[];
  edges: Edge[];
}

interface Node {
  run(input: any[]): any[]; // to be executed. The input elements match the incoming handles, the output elements the outgoing handles. Storage/transmission of the data is to be implemented by the actual block implementations.
  incomingHandles: Handle[];
  outgoingHandles: Handle[];
}

// contains parameters for js Function constructor.
interface JavaScriptFunctionNode extends Node {
  functionArgs: string[];
  functionBody: string;
}

// example custom block that requests data at the set url.
interface RequestNode extends Node {
  url: string;
}

interface Edge {
  fromNode: Node;
  fromHandle: Handle;
  toNode: Node;
  toHandle: Handle;
}

interface Handle {
  name: string;
  id: string;
}
```