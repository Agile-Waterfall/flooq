import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { List } from '../../components/list/list'
import { DataFlowListItem } from '../../components/list/data-flow-list-item'

export default {
  title: 'Atoms/List',
  component: DataFlowListItem
} as ComponentMeta<typeof DataFlowListItem>

const Template: ComponentStory<typeof DataFlowListItem> = ( args ) => (
  <div style={{ maxWidth: '1440px' }}>
    <List>
      <DataFlowListItem {...args} />
      <DataFlowListItem {...args} />
      <DataFlowListItem {...args} />
      <DataFlowListItem {...args} />
    </List>
  </div>
)

export const DataFlowList = Template.bind( {} )
DataFlowList.args = {
  id: 1,
  name: 'List Element #1',
  lastEdited: '2022-04-06T20:48:42Z',
  status: 'Active'
}
