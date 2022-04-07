import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { TextArea } from '../../components/form/textarea'

export default {
  title: 'Atoms/Form',
  component: TextArea
} as ComponentMeta<typeof TextArea>

const Template: ComponentStory<typeof TextArea> = ( args ) => (
  <div style={{
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    justifyContent: 'flex-start',
    alignItems: 'flex-start'
  }}>
    <TextArea {...args} />
    <TextArea {...args} disabled />
  </div>
)

export const TextAreaInput = Template.bind( {} )
TextAreaInput.args = {
  label: 'Label',
  value: 'Value',
  placeholder: 'Placeholder',
  disabled: false
}
