import { Dialog } from '../dialog'
import { Node as ReactFlowNode } from 'react-flow-renderer/nocss'
import { NodeTypeOption } from './node-type-option'
import { nodeTypes } from '../graph/node-types'

interface AddNodeDialogProps {
  isAddNodeOpen: boolean,
  setIsAddNodeOpen( value: boolean ): void
  flow: any
  nodes: any
  setNodes( value: any ): void
}

export const AddNodeDialog = ( {
  isAddNodeOpen,
  setIsAddNodeOpen,
  flow,
  nodes,
  setNodes
}: AddNodeDialogProps ): JSX.Element => {

  const addNode = ( newNode: ReactFlowNode ): void => {
    setNodes( [
      ...nodes,
      newNode
    ] )
    setIsAddNodeOpen( false )
  }

  return (
    <Dialog
      isOpen={isAddNodeOpen}
      onClose={(): void => setIsAddNodeOpen( false )}
      title={`Add Node to: ${flow.name}`}
    >
      <hr/>
      <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 py-4 dark:text-gray-100">
        {nodeTypes.map( node => (
          <NodeTypeOption
            key={node.id}
            title={node.title}
            description={node.description}
            onClick={(): void => addNode( node.create() )}
          />
        ) )}
      </div>
    </Dialog>
  )
}
