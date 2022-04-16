import { Node as ReactFlowNode } from 'react-flow-renderer/dist/nocss/types/nodes'
import { XYPosition } from 'react-flow-renderer/dist/nocss/types/utils'
import { v4 as uuidv4 } from 'uuid'

interface NodeData {
  title: string,
  description: string,
  rest?: any[]
}

abstract class Node implements ReactFlowNode {
  type: string
  id: string
  dragHandle: string
  data: NodeData
  position: XYPosition

  protected constructor( type: string, data: NodeData ) {
    this.type = type
    this.id = uuidv4()
    this.dragHandle = '.custom-drag-handle'
    this.data = data
    this.position = this.randomPosition( -100, 100 )
  }

  abstract create(): Node

  private randomPosition = ( min: number, max: number ): XYPosition => {
    return { 'x': this.randomIntFromInterval( min, max ), 'y': this.randomIntFromInterval( min, max ) }
  }

  private randomIntFromInterval = ( min: number, max: number ): number => {
    return Math.floor( Math.random() * ( max - min + 1 ) + min )
  }
}

class HttpInNode extends Node {
  constructor() {
    const type = 'httpIn'
    const data = {
      title: 'HTTP Input',
      description: 'A HTTP Input Node, used to get data from a http request. This node can be used as the start Node of your flow.',
      params: {
        url: '',
        method: '',
        contentType: '',
        sampleBody: ''
      },
      incomingHandles: [],
      outgoingHandles: [{ 'id': 'out', 'name': 'out' }]
    }
    super(
      type,
      data
    )
  }

  create = (): HttpInNode => {
    return new HttpInNode()
  }
}

class HttpOutNode extends Node {
  constructor() {
    const type = 'httpOut'
    const data = {
      title: 'HTTP Output',
      description: 'A HTTP Output Node, used to send data in the form of a http request. This node can be used as the end Node of your flow.',
      params: {
        url: '',
        method: '',
        contentType: '',
        sampleBody: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': '' }],
      outgoingHandles: []
    }
    super(
      type,
      data
    )
  }

  create = (): HttpOutNode => {
    return new HttpOutNode()
  }
}

class FilterNode extends Node {
  constructor() {
    const type = 'filter'
    const data = {
      title: 'Filter',
      description: 'A Filter Node, useful for filtering the data flowing through your data flow. Don\'t need parts of your incoming data? Just add a Filter Node!',
      params: {
        field: '',
        value: '',
        condition: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': '' }],
      outgoingHandles: [{ 'id': 'out', 'name': 'out' }]
    }
    super(
      type,
      data
    )
  }

  create = (): FilterNode => {
    return new FilterNode()
  }
}

class ScriptNode extends Node {
  constructor() {
    const type = 'script'
    const data = {
      title: 'Script',
      description: 'If you want to unlock the full potential of data flows, the Script Node is the way to go. Create a custom function to transform extend or reduce your data.',
      params: {
        function: '(a) => {\n\treturn a\n}'
      },
      incomingHandles: [{ 'id': 'a', 'name': 'a' }],
      outgoingHandles: [{ 'id': 'out', 'name': 'out' }]
    }
    super(
      type,
      data
    )
  }

  create = (): ScriptNode => {
    return new ScriptNode()
  }
}

export const nodeTypes = [
  new HttpInNode(),
  new HttpOutNode(),
  new FilterNode(),
  new ScriptNode(),
]
