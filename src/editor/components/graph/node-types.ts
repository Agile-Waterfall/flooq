import { Node as ReactFlowNode } from 'react-flow-renderer/dist/nocss/types/nodes'
import { XYPosition } from 'react-flow-renderer/dist/nocss/types/utils'
import { v4 as uuidv4 } from 'uuid'

abstract class Node implements ReactFlowNode {
  type: string
  title: string
  description: string
  id: string
  dragHandle: string
  data: any
  position: XYPosition

  protected constructor( type: string, title: string, description: string, data: any ) {
    this.type = type
    this.title = title
    this.description = description
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
    const title = 'HTTP Input'
    const description = 'A HTTP Input Node, used to get data from a http request. This node can be used as the start Node of your flow.'
    const data = {
      title: 'http in',
      input: {
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
      title,
      description,
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
    const title = 'HTTP Output'
    const description = 'A HTTP Output Node, used to send data in the form of a http request. This node can be used as the end Node of your flow.'
    const data = {
      title: 'http out',
      output: {
        url: '',
        method: '',
        contentType: '',
        sampleBody: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': 'in' }],
      outgoingHandles: []
    }
    super(
      type,
      title,
      description,
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
    const title = 'Filter'
    const description = 'A Filter Node, useful for filtering the data flowing through your data flow. Don\'t need parts of your incoming data? Just add a Filter Node!'
    const data = {
      title: 'Filter',
      filter: {
        field: '',
        value: '',
        condition: ''
      },
      incomingHandles: [{ 'id': 'in', 'name': 'in' }],
      outgoingHandles: [{ 'id': 'out', 'name': 'out' }]
    }
    super(
      type,
      title,
      description,
      data
    )
  }

  create = (): FilterNode => {
    return new FilterNode()
  }
}

export const nodeTypes = [
  new HttpInNode(),
  new HttpOutNode(),
  new FilterNode()
]
