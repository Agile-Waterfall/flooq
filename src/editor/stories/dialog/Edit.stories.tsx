import React, { useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { EditDataFlowDialog } from '../../components/flow/edit-dialog'

export default {
  title: 'Molecules/Dialog',
  component: EditDataFlowDialog
} as ComponentMeta<typeof EditDataFlowDialog>

const Template: ComponentStory<any> = ( args ) => {
  const [flow, setFlow] = useState( args.flow )

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      gap: '10px',
      justifyContent: 'flex-start',
      alignItems: 'flex-start'
    }}>
      <EditDataFlowDialog
        isEditOpen={args.isEditOpen}
        setIsEditOpen={(): void => { }}
        save={(): void => { }}
        flow={flow}
        setFlow={setFlow}
        deleteFlow={(): void => { }}
      />
    </div>
  )
}

export const EditFlow = Template.bind( {} )
EditFlow.args = {
  flow: {
    name: 'Data Flow Name'
  },
  isEditOpen: true
}
