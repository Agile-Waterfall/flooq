import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { ListHeader } from '../../components/list/list-header'

export default {
  title: 'Atoms/List',
  component: ListHeader
} as ComponentMeta<typeof ListHeader>

const Template: ComponentStory<any> = () => (
  <div style={{ maxWidth: '1440px' }}>
    <ListHeader
      title="DataFlows"
      description="Desc"
      action={{
        label: 'Add a new DataFlow',
        onClick: console.log
      }}
    />
  </div>
)

export const Header = Template.bind( {} )
