import { Node as ReactFlowNode } from 'react-flow-renderer/dist/nocss/types/nodes'
import { XYPosition } from 'react-flow-renderer/dist/nocss/types/utils'

class Node implements ReactFlowNode{
  id: string
  dragHandle: string
  type: string
  data: any
  position: XYPosition

  constructor( type: string, data: any ) {
    this.id = 'id-' + Date.now()
    this.dragHandle = '.custom-drag-handle'
    this.type = type
    this.data = data
    this.position = this.randomPosition( -100, 100 )
  }

  randomPosition = ( min: number, max: number ): XYPosition => {
    return { 'x': this.randomIntFromInterval( min, max ), 'y': this.randomIntFromInterval( min, max ) }
  }

  randomIntFromInterval = ( min: number, max: number ): number => {
    return Math.floor( Math.random() * ( max - min + 1 ) + min )
  }
}

export class HttpInNode extends Node{
  constructor() {
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
    super( 'httpIn', data )
  }
}

export class HttpOutNode extends Node{
  constructor() {
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
    super( 'httpOut', data )
  }
}

export class FilterNode extends Node{
  constructor() {
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
    super( 'filter', data )
  }
}
