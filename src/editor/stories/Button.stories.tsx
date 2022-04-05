import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { Button } from '../components/form/button'

export default {
  title: 'Atoms/Button',
  layout: 'fullscreen',
  component: Button,
  argTypes: {
    backgroundColor: { control: 'color' },
  },
} as ComponentMeta<typeof Button>

const Template: ComponentStory<typeof Button> = ( args ) => (
  <div className="dark:bg-gray-800">
    <Button {...args} />
  </div>
)

export const Primary = Template.bind( {} )
Primary.args = {
  disabled: false,
  children: 'Button',
  onClick: console.log
}

export const Secondary = Template.bind( {} )
Secondary.args = {
  disabled: false,
  children: 'Button',
  onClick: console.log
}
