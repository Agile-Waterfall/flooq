import React, { useCallback } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { Input } from '../../components/form/input'
import { ScriptNode } from '../../components/graph/script-node'
import ReactFlow, { addEdge, Background, useEdgesState, useNodesState, Node as ReactFlowNode, Edge as ReactFlowEdge, updateEdge } from 'react-flow-renderer/nocss'
import { FilterNode } from '../../components/graph/filter-node'
import { HttpInputNode } from '../../components/graph/http-input-node'
import { HttpOutputNode } from '../../components/graph/http-output-node'

export default {
  title: 'Molecules/Graph',
  component: Input
} as ComponentMeta<any>

const nodeTypes = {
  script: ScriptNode,
  filter: FilterNode,
  httpIn: HttpInputNode,
  httpOut: HttpOutputNode
}

const Template: ComponentStory<any> = ( args ) => {

  const [nodes, _, onNodesChange] = useNodesState<ReactFlowNode[]>( args.nodes )
  const [edges, setEdges, onEdgesChange] = useEdgesState<ReactFlowEdge[]>( args.edges )

  const onConnect = useCallback(
    ( connection ) => setEdges( ( eds: any ) => addEdge( { ...connection, animated: true }, eds ) ),
    [setEdges]
  )

  const onEdgeUpdate = ( oldEdge: any, newConnection: any ): any => setEdges( ( els ) => updateEdge( oldEdge, newConnection, els ) )

  return (
    <main>
      <ReactFlow
        nodes={nodes}
        edges={edges}
        onNodesChange={onNodesChange}
        onEdgesChange={onEdgesChange}
        onConnect={onConnect}
        onEdgeUpdate={onEdgeUpdate}
        nodeTypes={nodeTypes}
        minZoom={1}
        defaultZoom={1}
        maxZoom={1}
      >
        <Background />
      </ReactFlow>

    </main>
  )
}

export const NodeScript = Template.bind( {} )
NodeScript.args = {
  nodes: [
    {
      id: '1',
      type: 'script',
      dragHandle: '.custom-drag-handle',
      data: {
        title: 'JavaScript',
        params: {
          function: 'const handler = (a) => {\n\treturn a\n}'
        },
        incomingHandles: [{ id: 1, name: 'a' }],
        outgoingHandles: [{ id: 'out', name: 'out' }]
      },
      position: {
        x: 50,
        y: 50,
      }
    }
  ],
  edges: []
}

export const NodeFilter = Template.bind( {} )
NodeFilter.args = {
  nodes: [
    {
      id: '1',
      type: 'filter',
      dragHandle: '.custom-drag-handle',
      data: {
        title: 'Filter',
        params: {
          field: 'id',
          condition: 'eq',
          value: 'secret'
        },
        incomingHandles: [{ id: 1, name: 'a' }],
        outgoingHandles: [{ id: 'out', name: 'out' }]
      },
      position: {
        x: 50,
        y: 50,
      }
    }
  ],
  edges: []
}

export const NodeHttpInput = Template.bind( {} )
NodeHttpInput.args = {
  nodes: [
    {
      id: '1',
      type: 'httpIn',
      dragHandle: '.custom-drag-handle',
      data: {
        title: 'Http Input',
        params: {
          url: '',
          method: 'get',
          contentType: '',
          sampleBody: ''
        },
        incomingHandles: [{ id: 1, name: 'a' }],
        outgoingHandles: [{ id: 'out', name: 'out' }]
      },
      position: {
        x: 50,
        y: 50,
      }
    }
  ],
  edges: []
}

export const NodeHttpOutput = Template.bind( {} )
NodeHttpOutput.args = {
  nodes: [
    {
      id: '1',
      type: 'httpOut',
      dragHandle: '.custom-drag-handle',
      data: {
        title: 'Http Output',
        params: {
          url: '',
          method: 'get',
          contentType: '',
          header: '',
          body: ''
        },
        incomingHandles: [{ id: 1, name: 'a' }],
        outgoingHandles: [{ id: 'out', name: 'out' }]
      },
      position: {
        x: 50,
        y: 50,
      }
    }
  ],
  edges: []
}

