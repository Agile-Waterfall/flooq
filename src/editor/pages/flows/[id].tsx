import dynamic from 'next/dynamic'
import Head from 'next/head'
import { useCallback, useState } from 'react'
import ReactFlow, { useNodesState, MiniMap, Controls, Node as ReactFlowNode, Edge as ReactFlowEdge, useNodes, useEdgesState, addEdge, updateEdge } from 'react-flow-renderer/nocss'
import { FilterNode } from '../../components/graph/filter-node'
import { HttpInputNode } from '../../components/graph/http-input-node'
import { HttpOutputNode } from '../../components/graph/http-output-node'
import { PageTitle } from '../../components/page-title'
import { toReactFlowEdge } from '../../helper/edges'

const Background = dynamic( () => import( 'react-flow-renderer/nocss' ).then( ( mod ): any => mod.Background ), { ssr: false } )

const nodeTypes = {
  httpIn: HttpInputNode,
  httpOut: HttpOutputNode,
  filter: FilterNode,
}

const DataFlowOverview = ( { flow }: any ): JSX.Element => {

  const [nodes, setNodes, onNodesChange] = useNodesState<ReactFlowNode[]>( flow.nodes )
  const [edges, setEdges, onEdgesChange] = useEdgesState<ReactFlowEdge[]>( flow.edges )

  const onConnect = useCallback(
    ( connection ) => setEdges( ( eds: any ) => addEdge( { ...connection, animated: true }, eds ) ),
    [setEdges]
  )

  const onEdgeUpdate = ( oldEdge: any, newConnection: any ): any => setEdges( ( els ) => updateEdge( oldEdge, newConnection, els ) )

  return (
    <>
      <Head>
        <title>Flooq | {flow.name}</title>
      </Head>
      <PageTitle name={flow.name} />
      <main>
        <ReactFlow
          nodes={nodes}
          edges={edges}
          onConnect={onConnect}
          onEdgeUpdate={onEdgeUpdate}
          snapToGrid={true}
          snapGrid={[15, 15]}
          nodeTypes={nodeTypes}
          fitView
        >
          <MiniMap />
          <Controls />
          <Background color="#6b7280" size={.7} gap={16} />
        </ReactFlow>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const res = await fetch( `${process.env.BASE_URL}/api/flows/${context.query.id}` )
  const flow = await res.json()

  return {
    props: {
      flow: {
        ...flow,
        edges: flow.edges.map( toReactFlowEdge )
      }
    }
  }
}


export default DataFlowOverview
