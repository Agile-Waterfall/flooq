import { Dialog } from '../dialog'
import { Node as ReactFlowNode } from 'react-flow-renderer/nocss'
import { NodeTypeOption } from './node-type-option'
import { FilterNode, HttpInNode, HttpOutNode } from '../graph/node-types'

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
      <hr />
      <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 py-4 dark:text-gray-100">
        <NodeTypeOption title="HTTP Input" description="A HTTP Input Node, used to get data from a http request. This node can be used as the start Node of your flow." onClick={(): void => addNode( new HttpInNode() )}/>
        <NodeTypeOption title="HTTP Output" description="A HTTP Output Node, used to send data in the form of a http request. This node can be used as the end Node of your flow." onClick={(): void => addNode( new HttpOutNode() )}/>
        <NodeTypeOption title="Filter" description="A Filter Node, useful for filtering the data flowing through your data flow. Don't need parts of your incoming data? Just add a Filter Node!" onClick={(): void => addNode( new FilterNode() )}/>
      </div>
    </Dialog>
  )
}
