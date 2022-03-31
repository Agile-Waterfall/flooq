import Head from 'next/head'
import { useState } from 'react'
import ReactFlow, { removeElements, addEdge, MiniMap, Controls, Background, } from 'react-flow-renderer/nocss'
import { FilterNode } from '../../components/graph/filter-node'
import { HttpInputNode } from '../../components/graph/http-input-node'
import { HttpOutputNode } from '../../components/graph/http-output-node'
import { PageTitle } from '../../components/page-title'
import { toReactFlowEdge } from '../../helper/edges'

const nodeTypes = {
  httpIn: HttpInputNode,
  httpOut: HttpOutputNode,
  filter: FilterNode,
}

const onLoad = ( reactFlowInstance: any ): void => {
  console.log( 'flow loaded:', reactFlowInstance )
  reactFlowInstance.fitView( { padding: 1 } )
}

const DataFlowOverview = ( { flow }: any ): JSX.Element => {
  const [elements, setElements] = useState( flow.elements )

  const onElementsRemove = ( elementsToRemove: any ): void => setElements( ( els: any ) => removeElements( elementsToRemove, els ) )
  const onConnect = ( params: any ): void => setElements( ( els: any ) => addEdge( { ...params, animated: true }, els ) )
  console.log( flow )
  return (
    <>
      <Head>
        <title>Flooq | {flow.name}</title>
      </Head>
      <PageTitle name={flow.name} />
      <main>
        <ReactFlow
          elements={elements}
          onElementsRemove={onElementsRemove}
          onConnect={onConnect}
          onLoad={onLoad}
          snapToGrid={true}
          snapGrid={[15, 15]}
          nodeTypes={nodeTypes}
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

  // context.res.setHeader(
  //   'Cache-Control',
  //   'public, s-maxage=10, stale-while-revalidate=59'
  // )

  return {
    props: {
      flow: {
        ...flow,
        elements: [
          ...flow.nodes,
          ...flow.edges.map( toReactFlowEdge )
        ]
      }
    }
  }
}


export default DataFlowOverview
