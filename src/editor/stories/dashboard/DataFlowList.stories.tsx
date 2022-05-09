import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { object } from '@storybook/addon-knobs'
import { ListItem } from '../../components/list/list-item'
import { DataFlowListItem, DataFlowListItemProps } from '../../components/dashboard/data-flow-list-item'
import { List } from '../../components/list/list'

export default {
  title: 'Molecules/Dashboard',
  component: ListItem
} as ComponentMeta<typeof ListItem>

const Template: ComponentStory<any> = ( args ) => (
  <div style={{ maxWidth: '1440px' }}>
    <List
      title="DataFlows"
      description="These are the DataFlows you have access to."
      action={{
        label: 'Create',
        onClick: console.log
      }}
    >{Object.values<DataFlowListItemProps>( args ).map( ( item: DataFlowListItemProps, i: number ): any => (
      <DataFlowListItem key={i} {...item} />
    ) )}
    </List>
  </div>
)

export const Items = Template.bind( {} )

Items.args = object( 'List of Items', [
  {
    id: 1,
    name: 'List Element #1',
    lastEdited: '2022-04-06T20:48:42Z',
    status: 'Active'
  }
] )
