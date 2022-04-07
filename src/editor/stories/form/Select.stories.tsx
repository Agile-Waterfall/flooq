import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { Select } from '../../components/form/select'

export default {
  title: 'Atoms/Form',
  component: Select
} as ComponentMeta<typeof Select>

const Template: ComponentStory<typeof Select> = ( args ) => (
  <div style={{
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    justifyContent: 'flex-start',
    alignItems: 'flex-start'
  }}>
    <Select {...args} />
    <Select {...args} disabled />
  </div>
)

export const SelectInput = Template.bind( {} )
SelectInput.args = {
  label: 'Label',
  selected: 'Value',
  options: [
    { name: 'Option 1', value: 'o1' },
    { name: 'Option 2', value: 'o2' },
    { name: 'Option 3', value: 'o3' },
    { name: 'Option 4', value: 'o4' }
  ],
  disabled: false
}
