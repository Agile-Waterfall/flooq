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
      {Object.values<JSX.Element>( args ).map( ( item: JSX.Element, i: number ): any => (
        <ListItem key={i}>{item}</ListItem>
      ) )}
    </div>
  </div>
)

export const Items = Template.bind( {} )

Items.args = object( 'List of Items', [
  <>Item 1</>,
  // <>Item 2</>,
  // <>Item 3</>,
  // <>Item 4</>,
] )
