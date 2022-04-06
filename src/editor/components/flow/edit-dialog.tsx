import { useState } from 'react'
import { Dialog } from '../dialog'
import { Button } from '../form/button'
import { Input } from '../form/input'
import { Select } from '../form/select'

interface EditDataFlowDialogProps {
  isEditOpen: boolean,
  setIsEditOpen( value: boolean ): void
  save(): void
  flow: any
  setFlow( value: any ): void
}

const states = [
  { value: 'active', name: 'Active' },
  { value: 'disabled', name: 'Disabled' }
]

export const EditDataFlowDialog = ( { isEditOpen, setIsEditOpen, save, flow, setFlow }: EditDataFlowDialogProps ): JSX.Element => {
  return (
    <Dialog
      isOpen={isEditOpen}
      onClose={(): void => setIsEditOpen( false )}
      title={`Edit ${flow.name}`}
    >
      <div className="py-4 flex flex-col gap-3">
        <Input
          label="Name"
          value={flow.name}
          onChange={( e ): void => setFlow( { ...flow, name: e.target.value } )}
        />
        <Select
          label="Status"
          options={states}
          selected={flow.status}
          onChange={( e ): void => setFlow( { ...flow, status: e.target.value } )}
        />
      </div>
      <div className="flex gap-2">
        <Button secondary small onClick={(): void => setIsEditOpen( false )}>
          Cancel
        </Button>
        <Button primary small onClick={save}>
          Save
        </Button>
      </div>
    </Dialog>
  )
}
