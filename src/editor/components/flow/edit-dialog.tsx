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
  deleteFlow(): void
}

const states = [
  { value: 'Active', name: 'Active' },
  { value: 'Disabled', name: 'Disabled' }
]

export const EditDataFlowDialog = ( { isEditOpen, setIsEditOpen, save, flow, setFlow, deleteFlow }: EditDataFlowDialogProps ): JSX.Element => {
  const [deleteFlowName, setDeleteFlowName] = useState<string>( )

  return (
    <Dialog
      isOpen={isEditOpen}
      onClose={(): void => setIsEditOpen( false )}
      title={`Edit: ${flow.name}`}
    >
      <div className="py-4 flex flex-col gap-3 w-px-{400} dark:text-gray-100">
        <h4>Settings</h4>
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
      <hr />
      <div className="py-4 flex flex-col gap-3 w-px-{400} dark:text-gray-100">
        <h4>Danger Zone</h4>
        <Input
          label="Repeat the Data Flow name to delete it."
          value={deleteFlowName}
          placeholder={flow.name}
          onChange={( e ): void => setDeleteFlowName( e.target.value )}
        />
        <Button dangerous disabled={deleteFlowName !== flow.name} small onClick={deleteFlow}>
          Delete Data Flow
        </Button>
      </div>
      <hr />
      <div className="pt-4 flex gap-2">
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
