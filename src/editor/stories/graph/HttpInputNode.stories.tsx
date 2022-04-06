import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { HttpInputNode } from '../../components/graph/http-input-node'

export default {
  title: 'Atoms/Graph',
  component: HttpInputNode
} as ComponentMeta<typeof HttpInputNode>

const Template: ComponentStory<typeof HttpInputNode> = ( args ) => (
  <HttpInputNode {...args} />
)
export const HttpInput = Template.bind( {} )
HttpInput.args = {
  id: '1',
  type: 'filter',
  position: { x: 0, y: 0 },
  data: {
    title: 'Title',
    input: {
      contentType: 'application/json',
      method: 'post',
      sampleBody: '{}',
      url: 'https://executor.dataflow.ch/IJF9K2',
    },
    outgoingHandles: [],
    incomingHandles: []
  }
}
