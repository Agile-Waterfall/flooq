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
  status: boolean;
  lastEdited: string;
  nodes: Node[];
  edges: Edge[];
}

interface Node {
  id: string; // unique id to find the node
  type: string; // type of the node (e.g. httpIn, httpOut, script, etc.)
  data: any; // any data that is required by the node
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
  id: string;
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

### Sample Data

This is how a sample flow could look like. Some fields are only used in the frontend but should be saved in the db as well. The executor can ignore those.

```json
{
    "id": 3,
    "name": "Demo Flow",
    "status": "Active",
    "lastEdited": "05.03.2022 14:45.12",
    "nodes": [
      {
        "id": "0", 
        "type": "httpIn",
        "data": {
          "title": "Http Input",
          "input": {
            "url": "https://executor.dataflow.ch/IJF9K2",
            "method": "post",
            "contentType": "application/json",
            "sampleBody": "{}"
          }
        },
        "incomingHandles": [],
        "outgoingHandles": [
            {
              "id": "11",
              "name": "a"
            },
            {
              "id": "12",
              "name": "b"
            }
        ],
        "position": {
          "x": 0,
          "y": 0
        }
      },
      {
        "id": "2",
        "type": "script",
        "data": {
          "title": "JavaScript",
          "input": {
            "function": "async (a, b) => {\n  return a + b;\n}"
          }
        },
        "incomingHandles": [
            {
              "id": "2a",
              "name": "a"
            },
            {
              "id": "2b",
              "name": "b"
            }
        ],
        "outgoingHandles": [
            {
              "id": "21",
              "name": "a"
            }
        ],
        "position": {
          "x": 400,
          "y": 100
        }
      },
      {
        "id": "0", 
        "type": "httpOut",
        "data": {
          "title": "Http Output",
          "input": {
            "url": "",
            "method": "post",
            "contentType": "application/json",
            "sampleBody": "{}"
          }
        },
        "incomingHandles": [
            {
              "id": "3a",
              "name": "a"
            }
        ],
        "outgoingHandles": [],
        "position": {
          "x": 800,
          "y": 0
        }
      },
    ],
    "edges": [
      {
        "id": "e1-2",
        "fromNode": "1",
        "fromHandle": "11",
        "toNode": "2",
        "toHandle": "2a"
      },
      {
        "id": "e1-2",
        "fromNode": "2",
        "fromHandle": "21",
        "toNode": "3",
        "toHandle": "3a"
      }
    ]
  }
```