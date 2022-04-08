import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { Input } from '../../components/form/input'

export default {
  title: 'Atoms/Form',
  component: Input
} as ComponentMeta<typeof Input>

const Template: ComponentStory<typeof Input> = ( args ) => (
  <div style={{
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    justifyContent: 'flex-start',
    alignItems: 'flex-start'
  }}>
    <Input {...args} />
    <Input {...args} disabled />
  </div>
)

export const TextInput = Template.bind( {} )
TextInput.args = {
  label: 'Label',
  value: 'Value',
  placeholder: 'Placeholder',
  disabled: false
}
