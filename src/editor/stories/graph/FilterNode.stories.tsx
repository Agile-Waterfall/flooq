import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { FilterNode } from '../../components/graph/filter-node'

export default {
  title: 'Atoms/Graph',
  component: FilterNode
} as ComponentMeta<typeof FilterNode>

const Template: ComponentStory<typeof FilterNode> = ( args ) => (
  <FilterNode {...args} />
)

export const Filter = Template.bind( {} )
Filter.args = {
  id: '1',
  type: 'filter',
  position: { x: 0, y: 0 },
  data: {
    title: 'Title',
    filter: {
      field: 'test'
    },
    outgoingHandles: [],
    incomingHandles: []
  }
}
