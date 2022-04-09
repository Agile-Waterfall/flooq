import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { EditDataFlowDialog } from '../../components/flow/edit-dialog'

export default {
  title: 'Molecule/Dialog',
  component: EditDataFlowDialog
} as ComponentMeta<typeof EditDataFlowDialog>

const Template: ComponentStory<any> = ( args ) => (
  <div style={{
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    justifyContent: 'flex-start',
    alignItems: 'flex-start'
  }}>
    <EditDataFlowDialog
      setIsEditOpen={(): void => { }}
      save={(): void => { }}
      setFlow={(): void => { }}
      deleteFlow={(): void => { }}
      {...args}
    />
  </div>
)

export const EditFlow = Template.bind( {} )
EditFlow.args = {
  flow: {
    name: 'Data Flow Name'
  },
  isEditOpen: true
}
