import { Dialog } from '@headlessui/react'
import { Button } from '../form/button'

interface DeleteAccountActionProps {
  open: boolean,
  setOpen: ( open: boolean ) => void
  onDelete: () => void
}

export const DeleteAccountAction = ( { open, setOpen, onDelete }: DeleteAccountActionProps ): JSX.Element => (
  <Dialog open={open} onClose={(): void => setOpen( false )}>
    <div className="fixed w-full h-full full-height z-20 top-0 left-0 flex items-center justify-center bg-gray-100 dark:bg-gray-800 bg-opacity-50 dark:bg-opacity-70">
      <div className="bg-white dark:bg-gray-900 shadow sm:rounded-lg z-30 relative">
        <div className="px-4 py-5 sm:p-6">

          <Dialog.Title className="text-lg leading-6 font-medium text-gray-900 dark:text-gray-50">
            Delete your account
          </Dialog.Title>
          <Dialog.Description className="mt-2 max-w-xl text-sm text-gray-500">
            Once you delete your account, you will lose all data associated with it.
          </Dialog.Description>
          <div className="mt-5 flex gap-4">
            <Button
              dangerous
              onClick={onDelete}
            >
              Delete Account
            </Button>
            <Button
              secondary
              onClick={(): void => setOpen( false )}
            >
              Cancel
            </Button>
          </div>
        </div>
      </div>
    </div>
  </Dialog>
)
