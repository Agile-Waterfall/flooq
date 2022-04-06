import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { HttpOutputNode } from '../../components/graph/http-output-node'

export default {
  title: 'Atoms/Graph',
  component: HttpOutputNode
} as ComponentMeta<typeof HttpOutputNode>

const Template: ComponentStory<typeof HttpOutputNode> = ( args ) => (
  <HttpOutputNode {...args} />
)
export const HttpOutput = Template.bind( {} )
HttpOutput.args = {
  id: '1',
  type: 'filter',
  position: { x: 0, y: 0 },
  data: {
    title: 'Title',
    output: {
      contentType: 'application/json',
      method: 'post'
    },
    outgoingHandles: [],
    incomingHandles: []
  }
}
