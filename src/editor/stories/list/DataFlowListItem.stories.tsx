import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { object } from '@storybook/addon-knobs'
import { List } from '../../components/list/list'
import { DataFlowListItem, DataFlowListItemProps } from '../../components/list/data-flow-list-item'

export default {
  title: 'Atoms/List',
  component: DataFlowListItem
} as ComponentMeta<typeof DataFlowListItem>

const Template: ComponentStory<any> = ( args ) => (
  <div style={{ maxWidth: '1440px' }}>
    <List>
      {Object.values<DataFlowListItemProps>( args ).map( ( item: DataFlowListItemProps, i: number ): any => (
        <DataFlowListItem key={i} {...item} />
      ) )}
    </List>
  </div>
)

export const DataFlowList = Template.bind( {} )

DataFlowList.args = object( 'List of Items', [
  {
    id: 1,
    name: 'List Element #1',
    lastEdited: '2022-04-06T20:48:42Z',
    status: 'Active'
  },
  {
    id: 2,
    name: 'List Element #2',
    lastEdited: '2022-04-06T20:48:42Z',
    status: 'Active'
  },
  {
    id: 3,
    name: 'List Element #3',
    lastEdited: '2022-04-06T20:48:42Z',
    status: 'Active'
  },
  {
    id: 4,
    name: 'List Element #4',
    lastEdited: '2022-04-06T20:48:42Z',
    status: 'Active'
  }
] )
