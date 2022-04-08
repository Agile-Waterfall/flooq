import { Dialog } from '../dialog'
import { Button } from '../form/button'
import { Node as ReactFlowNode } from 'react-flow-renderer/nocss'

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
  }

  return (
    <Dialog
      isOpen={isAddNodeOpen}
      onClose={(): void => setIsAddNodeOpen( false )}
      title={`Add Node to: ${flow.name}`}
    >
      <div className="py-4 flex flex-col gap-3 w-px-{400} dark:text-gray-100">
        <h4>To be changed</h4>
        <Button secondary small onClick={(): void => addNode( newHttpInNode() )}>
          Add httpIn  Node
        </Button>
        <Button secondary small onClick={(): void => addNode( newHttpOutNode() )}>
          Add httpOut  Node
        </Button>
        <Button secondary small onClick={(): void => addNode( newFilterNode() )}>
          Add filter Node
        </Button>
      </div>
      <hr/>
      <div className="pt-4 flex gap-2">
        <Button secondary small onClick={(): void => setIsAddNodeOpen( false )}>
          Cancel
        </Button>
        <Button primary small onClick={save}>
          Save
        </Button>
      </div>
    </Dialog>
  )
}
