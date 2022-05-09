import { Button } from '../form/button'

export interface Action {
  label: string,
  disabled?: boolean,
  onClick: ( e: any ) => void
}

interface ListHeaderComponent {
  title: string,
  description: string,
  action: Action
}

export const ListHeader = ( { title, description, action }: ListHeaderComponent ): JSX.Element => {
  return (
    <div className="bg-white dark:bg-gray-800 px-4 py-5 border-b border-gray-200 dark:border-gray-600 sm:px-6 rounded-t-md">
      <div className="-ml-4 -mt-4 flex justify-between items-center flex-wrap sm:flex-nowrap">
        <div className="ml-4 mt-4">
          <h3 className="text-lg leading-6 font-medium text-gray-900 dark:text-gray-100">{title}</h3>
          <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
            {description}
          </p>
        </div>
        <div className="ml-4 mt-4 flex-shrink-0">
          <Button
            onClick={action.onClick}
            disabled={action.disabled}
            primary
          >
            {action.label}
          </Button>
        </div>
      </div>
    </div>
  )
}
