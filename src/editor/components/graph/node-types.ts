import { Node as ReactFlowNode } from 'react-flow-renderer/dist/nocss/types/nodes'
import { XYPosition } from 'react-flow-renderer/dist/nocss/types/utils'
import { v4 as uuidv4 } from 'uuid'

export type NodeType = 'httpIn' | 'httpOut' | 'filter' | 'script' | 'condition' | 'remap' | 'timeTrigger' | 'emailOutput'

interface NodeData {
  title: string,
  description: string,
  disabled?: boolean,
  type: string | 'Input' | 'Output' | 'Process',
  rest?: any[]
}

abstract class Node implements ReactFlowNode {
  type: NodeType
  id: string
  dragHandle: string
  data: NodeData
  position: XYPosition

  protected constructor( type: NodeType, data: NodeData ) {
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
      type: 'Input',
      description: 'Used as an input to get data from an http request. This node can be used as the start of your flow.',
      params: {
        url: '',
        method: '',
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
      type: 'Output',
      description: 'Used as an output to send data via an http request. This node can be used as the end of your flow.',
      params: {
        url: '',
        method: '',
        headers: '{}',
        body: '{}',
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
      type: 'Process',
      disabled: true,
      description: 'Useful for filtering the data flowing through your data flow.',
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
      type: 'Process',
      description: 'Unlock the full potential of data flows. Create a custom function to transform, extend, or reduce your data.',
      params: {
        function: 'const handler = (a) => {\n\treturn a\n}'
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

class ConditionNode extends Node {
  constructor() {
    const type = 'condition'
    const data = {
      title: 'Condition',
      type: 'Process',
      disabled: true,
      description: 'Allows to separate a data flow into multiple sub flows based on a condition.',
      params: {
        field: '',
        value: '',
        condition: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': 'in' }],
      outgoingHandles: [{ 'id': 'true', 'name': 'true' }, { 'id': 'false', 'name': 'false' }]
    }
    super(
      type,
      data
    )
  }

  create = (): ConditionNode => {
    return new ConditionNode()
  }
}

class RemapNode extends Node {
  constructor() {
    const type = 'remap'
    const data = {
      title: 'Remap',
      type: 'Process',
      disabled: true,
      description: 'Change the structure of an incoming object based on a json definition.',
      params: {
        input: '',
        output: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': 'in' }],
      outgoingHandles: [{ 'id': 'out', 'name': 'out' }]
    }
    super(
      type,
      data
    )
  }

  create = (): RemapNode => {
    return new RemapNode()
  }
}

class TimeTrigger extends Node {
  constructor() {
    const type = 'timeTrigger'
    const data = {
      title: 'Time Trigger',
      type: 'Input',
      disabled: true,
      description: 'Start a data flow based on a cron tab.',
      params: {
        cron: ''
      },
      incomingHandles: [],
      outgoingHandles: [{ 'id': 'out', 'name': 'out' }]
    }
    super(
      type,
      data
    )
  }

  create = (): TimeTrigger => {
    return new TimeTrigger()
  }
}

class EmailOutput extends Node {
  constructor() {
    const type = 'emailOutput'
    const data = {
      title: 'E-Mail Output',
      type: 'Output',
      disabled: true,
      description: 'Send an E-Mail at the end of a data flow. Set the receiver, subject and body.',
      params: {
        receiver: '',
        subject: '',
        body: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': 'in' }],
      outgoingHandles: []
    }
    super(
      type,
      data
    )
  }

  create = (): EmailOutput => {
    return new EmailOutput()
  }
}

export const nodeTypes = [
  new HttpInNode(),
  new HttpOutNode(),
  new ScriptNode(),
  new FilterNode(),
  new ConditionNode(),
  new RemapNode(),
  new TimeTrigger(),
  new EmailOutput()
]
