import { Dialog } from '../dialog'
import { Node as ReactFlowNode } from 'react-flow-renderer/nocss'
import { NodeTypeOption } from './node-type-option'
import { nodeTypes } from '../graph/node-types'

interface AddNodeDialogProps {
  isAddNodeOpen: boolean,
  setIsAddNodeOpen( value: boolean ): void
  nodes: any
  setNodes( value: any ): void
}

export const AddNodeDialog = ( {
  isAddNodeOpen,
  setIsAddNodeOpen,
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
      title="Add Node"
    >
      <hr/>
      <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 py-4 dark:text-gray-100 max-w-lg max-h-[85vh] overflow-y-auto">
        {nodeTypes.map( node => (
          <NodeTypeOption
            key={node.id}
            title={node.data.title}
            type={node.data.type}
            description={node.data.description}
            disabled={node.data.disabled}
            onClick={(): void => addNode( node.create() )}
          />
        ) )}
      </div>
    </Dialog>
  )
}
