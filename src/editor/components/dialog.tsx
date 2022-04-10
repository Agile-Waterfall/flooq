import { Dialog as HeadlessDialog } from '@headlessui/react'
import { XIcon } from '@heroicons/react/outline'

interface DialogProps {
  isOpen: boolean,
  title: string
  description?: string,
  children?: any,
  onClose(): void
}

export const Dialog = ( { isOpen, onClose, title, description, children }: DialogProps ): JSX.Element => {
  return (
    <HeadlessDialog
      open={isOpen}
      onClose={onClose}
      className="fixed z-10 inset-0 flex items-center justify-center p-4"
    >
      <div className="flex items-center justify-center min-h-screen max-w-fit">
        <HeadlessDialog.Overlay className="fixed inset-0 bg-black opacity-30" />
        <div className="relative bg-gray-50 dark:bg-gray-900 rounded mx-auto p-4">
          <HeadlessDialog.Title
            as="h3"
            className="text-lg pb-2 font-medium leading-6 text-gray-900 dark:text-gray-100 flex justify-between"
          >
            {title}
            <div className="cursor-pointer">
              <XIcon className="w-5 h-5" onClick={onClose} />
            </div>
          </HeadlessDialog.Title>
          <HeadlessDialog.Description>
            {description}
          </HeadlessDialog.Description>

          <div>
            {children}
          </div>
        </div>
      </div>
    </HeadlessDialog>
  )
}
