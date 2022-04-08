import { Dialog } from '../dialog'
import { Button } from '../form/button'
import { Node as ReactFlowNode } from 'react-flow-renderer/nocss'
import { NodeTypeOption } from './node-type-option'

interface AddNodeDialogProps {
  isAddNodeOpen: boolean,

  setIsAddNodeOpen( value: boolean ): void

  save(): void

  flow: any

  nodes: any

  setNodes( value: any ): void
}

export const AddNodeDialog = ( {
  isAddNodeOpen,
  setIsAddNodeOpen,
  save,
  flow,
  nodes,
  setNodes
}: AddNodeDialogProps ): JSX.Element => {

  const randomIntFromInterval = ( min: number, max: number ): number => {
    return Math.floor( Math.random() * ( max - min + 1 ) + min )
  }

  const newHttpInNode = (): ReactFlowNode => {
    return ( {
      id: 'id-' + Date.now(),
      dragHandle: '.custom-drag-handle',
      type: 'httpIn',
      data: {
        title: 'New HttpIn Node',
        input: {
          url: '',
          method: '',
          contentType: '',
          sampleBody: ''
        },
        incomingHandles: [],
        outgoingHandles: []
      },
      position: { 'x': randomIntFromInterval( -100, 100 ), 'y': randomIntFromInterval( -100, 100 ) }
    } )
  }

  const newHttpOutNode = (): ReactFlowNode => {
    return ( {
      id: 'id-' + Date.now(),
      dragHandle: '.custom-drag-handle',
      type: 'httpOut',
      data: {
        title: 'New Filter Node',
        output: {
          url: '',
          method: '',
          contentType: '',
          sampleBody: ''
        },
        incomingHandles: [],
        outgoingHandles: []
      },
      position: { 'x': randomIntFromInterval( -100, 100 ), 'y': randomIntFromInterval( -100, 100 ) }
    } )
  }

  const newFilterNode = (): ReactFlowNode => {
    return ( {
      id: 'id-' + Date.now(),
      dragHandle: '.custom-drag-handle',
      type: 'filter',
      data: {
        title: 'New HttpOut Node',
        filter: {
          field: '',
          value: '',
          condition: ''
        },
        incomingHandles: [],
        outgoingHandles: []
      },
      position: { 'x': randomIntFromInterval( -100, 100 ), 'y': randomIntFromInterval( -100, 100 ) }
    } )
  }

  const addNode = ( newNode: ReactFlowNode ): void => {
    setNodes( [
      ...nodes,
      newNode
    ] )
    save()
  }

  return (
    <Dialog
      isOpen={isAddNodeOpen}
      onClose={(): void => setIsAddNodeOpen( false )}
      title={`Add Node to: ${flow.name}`}
    >
      <hr />
      <div className="grid gap-4 grid-cols-2 py-4 dark:text-gray-100">
        <NodeTypeOption title="http Input" description="A http Input Node, used to get data from a http request. This node can be used as the start Node of your flow." onClick={(): void => addNode( newHttpInNode() )}/>
        <NodeTypeOption title="http Output" description="A http Output Node, used to send data in the form of a http request. This node can be used as the end Node of your flow." onClick={(): void => addNode( newHttpOutNode() )}/>
        <NodeTypeOption title="Filter" description="A Filter Node, useful for filtering the data flowing through your data flow. Don't need parts of your incoming data? Just add a Filter Node!" onClick={(): void => addNode( newFilterNode() )}/>
      </div>
      <hr/>
      <div className="pt-4 flex gap-2">
        <Button secondary small onClick={(): void => setIsAddNodeOpen( false )}>
          Cancel
        </Button>
      </div>
    </Dialog>
  )
}
