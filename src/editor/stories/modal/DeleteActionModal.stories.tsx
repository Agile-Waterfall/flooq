import React, { useEffect, useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { DeleteAccountAction } from '../../components/actions/delete-account-action'

export default {
  title: 'Atoms/Modal',
  component: DeleteAccountAction
} as ComponentMeta<typeof DeleteAccountAction>

const Template: ComponentStory<typeof DeleteAccountAction> = ( args ) => {
  const [open, setOpen] = useState( args.open )

  useEffect( () => {
    setOpen( args.open )
  }, [args.open] )

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      gap: '10px',
      justifyContent: 'flex-start',
      alignItems: 'flex-start'
    }}>
      <DeleteAccountAction {...args} open={open} setOpen={setOpen} />
    </div>
  )
}

export const DeleteActionModal = Template.bind( {} )
DeleteActionModal.args = {
  open: true,
  setOpen: console.log,
  onDelete: console.log
}
