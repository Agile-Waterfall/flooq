import { FormEventHandler } from 'react'
import { Button } from './button'

interface FormGroupProps {
  onSubmit?: FormEventHandler<HTMLFormElement>
  children: JSX.Element[] | JSX.Element
}

export const FormGroup = ( { children, onSubmit }: FormGroupProps ): JSX.Element => (
  <form onSubmit={onSubmit}>
    <div className="shadow dark:shadow-black sm:rounded-md sm:overflow-hidden">
      <div className="px-4 py-5 bg-white dark:bg-gray-800 space-y-6 sm:p-6">
        {children}
      </div>

      {onSubmit &&
        <div className="px-4 py-3 bg-gray-50 dark:bg-gray-900 text-right sm:px-6">
          <Button
            type="submit"
            primary
          >
            Save
          </Button>
        </div>
      }
    </div>
  </form>
)
