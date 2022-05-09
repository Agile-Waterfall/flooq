import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { object } from '@storybook/addon-knobs'
import { DataFlowListItem, DataFlowListItemProps } from '../../components/dashboard/data-flow-list-item'
import { List } from '../../components/list/list'

export default {
  title: 'Molecules/Dashboard',
  component: DataFlowListItem
} as ComponentMeta<typeof DataFlowListItem>

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

export const DataFlowList = Template.bind( {} )

DataFlowList.args = object( 'List of DataFlow Items', [
  {
    id: 1,
    name: 'List Element #1',
    lastEdited: '2022-04-06T20:48:42Z',
    status: 'Active'
  }
] )
