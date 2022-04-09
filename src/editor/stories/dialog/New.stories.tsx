import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { AddNodeDialog } from '../../components/flow/new-node-dialog'

export default {
  title: 'Atoms/Dialog',
  component: AddNodeDialog
} as ComponentMeta<typeof AddNodeDialog>

const Template: ComponentStory<any> = ( args ) => (
  <div style={{
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    justifyContent: 'flex-start',
    alignItems: 'flex-start'
  }}>
    <AddNodeDialog
      setIsAddNodeOpen={(): void => { }}
      setNodes={(): void => { }}
      {...args}
    />
  </div>
)

export const AddNewNode = Template.bind( {} )
AddNewNode.args = {
  flow: {
    name: 'Data Flow Name'
  },
  isAddNodeOpen: true,
}
