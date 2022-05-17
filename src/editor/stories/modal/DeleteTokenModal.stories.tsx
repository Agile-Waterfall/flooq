import React, { useEffect, useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { DeleteTokenAction } from '../../components/actions/delete-token-action'

export default {
  title: 'Atoms/Modal',
  component: DeleteTokenAction
} as ComponentMeta<typeof DeleteTokenAction>

const Template: ComponentStory<typeof DeleteTokenAction> = ( args ) => {
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
      <DeleteTokenAction {...args} open={open} setOpen={setOpen} />
    </div>
  )
}

export const DeleteTokenModal = Template.bind( {} )
DeleteTokenModal.args = {
  open: true,
  setOpen: console.log,
  onDelete: console.log
}
