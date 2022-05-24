import { DotsVerticalIcon, PlusIcon, XIcon, InformationCircleIcon } from '@heroicons/react/outline'
import { FC, useState } from 'react'
import { Handle, Position, Node as ReactFlowNode, useReactFlow } from 'react-flow-renderer/nocss'
import { NodeType } from './node-types'
import { DocsDialog } from '../docs-dialog'

const docsElements: Record<NodeType, JSX.Element > = {
  httpIn: require( './docs/http-input-node.md' ).default( {} ),
  httpOut: require( './docs/http-output-node.md' ).default( {} ),
  script: require( './docs/script-node.md' ).default( {} ),
  filter: require( './docs/no-documentation-present.md' ).default( {} ),
  condition: require( './docs/no-documentation-present.md' ).default( {} ),
  remap: require( './docs/no-documentation-present.md' ).default( {} ),
  timeTrigger: require( './docs/no-documentation-present.md' ).default( {} ),
  emailOutput: require( './docs/no-documentation-present.md' ).default( {} )
}

type NodeData = {
  title: string,
  outgoingHandles: any[],
  incomingHandles: any[],
  params?: any,
  canAddTargetHandle?: boolean,
  dataFlowId?: string,
  docsMdPath: string,
  onAddedTargetHandle?: ( e: any ) => void,
};

export type FlooqNode = ReactFlowNode<NodeData>;

export const Node: FC<FlooqNode> = ( { id, data, type: nodeType, children } ) => {

  const reactFlowHook = useReactFlow()

  const [areDocsOpen, setAreDocsOpen] = useState( false )

  const deleteNode = (): any => {
    reactFlowHook.setNodes( reactFlowHook.getNodes().filter( n => n.id !== id ) )
    reactFlowHook.setEdges( reactFlowHook.getEdges().filter( e => e.target !== id && e.source !== id ) )
  }

  return (
    <>
      <DocsDialog areDocsOpen={areDocsOpen} setAreDocsOpen={setAreDocsOpen}>
        { docsElements[nodeType as NodeType] }
      </DocsDialog>
      <div className="flex bg-gray-100 dark:bg-gray-900">
        {data.incomingHandles &&
        <div className="w-0 flex flex-col justify-evenly gap-1 relative">
          {data.incomingHandles.map( ( input: any ) => (
            <Handle
              key={input.id}
              id={input.id}
              type="target"
              position={Position.Left}
              className="bg-yellow-400 react-flow__handle--input"
            >
              {input.name}
            </Handle>
          ) )}
          {data.canAddTargetHandle &&
            <div
              onClick={data.onAddedTargetHandle}
              className=" bg-yellow-400 cursor-pointer pointer-events-auto flex justify-center items-center react-flow__add__handle"
            >
              <PlusIcon className="w-3 h-3" />
            </div>
          }
        </div>
        }
        <div className="flex flex-col flex-1 text-gray-900 dark:text-gray-100">
          <div className="p-2 flex justify-between items-center border-b border-gray-200 dark:border-gray-100">
            <span className="custom-drag-handle">
              <DotsVerticalIcon className="w-4 h-4" />
            </span>
            <span className="custom-drag-handle">
              {data.title}
            </span>
            <div className="flex align-center">
              <button onClick={(): void=> setAreDocsOpen( true )} className="pr-1">
                <InformationCircleIcon className='w-4 h-4'/>
              </button>
              <button onClick={deleteNode} className="custom-delete-handle w-4 cursor-pointer">
                <XIcon className="w-4 h-4"/>
              </button>
            </div>
          </div>
          <div>
            {children}
          </div>
        </div>
        {data.outgoingHandles &&
        <div className="w-2 flex flex-col justify-between gap-1">
          {data.outgoingHandles.map( ( output: any ) => (
            <Handle
              key={output.id}
              id={output.id}
              type="source"
              position={Position.Right}
              className="flex-1 grow bg-yellow-400 react-flow__handle--out"
            />
          ) )}
        </div>
        }
      </div>
    </>
  )
}
