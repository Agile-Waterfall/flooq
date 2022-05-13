import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { object } from '@storybook/addon-knobs'
import { ListItem } from '../../components/list/list-item'

export default {
  title: 'Atoms/List',
  component: ListItem
} as ComponentMeta<typeof ListItem>

const Template: ComponentStory<any> = ( args ) => (
  <div style={{ maxWidth: '1440px' }}>
    <div className="flex flex-col">
      {Object.values<ListItem>( args ).map( ( item: ListItem, i: number ): any => (
        <ListItem key={i} {...item} />
      ) )}
    </div>
  </div>
)

export const Items = Template.bind( {} )

Items.args = object( 'List of Items', [
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
