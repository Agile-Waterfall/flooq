interface NodeTypeOptionProps {
  title: string,
  type: string,
  description: string,
  disabled?: boolean,
  onClick(): void
}

export const NodeTypeOption = ( { title, type, description, disabled, onClick }: NodeTypeOptionProps ): JSX.Element => (
  <div onClick={!disabled ? onClick : (): void => {}} className={`${disabled ? 'cursor-not-allowed' : 'cursor-pointer'} col-span-1 bg-white dark:bg-gray-800 rounded-lg shadow divide-y divide-gray-200`}>
    <div className="w-full flex items-center justify-between p-6 space-x-6">
      <div className="flex-1">
        <div className="flex items-center space-x-3">
          <h3 className="text-gray-900 dark:text-gray-100 text-sm font-medium">{title}</h3>
          {!disabled &&
          <span className="flex-shrink-0 inline-block px-2 py-0.5 text-green-800 dark:text-green-300 text-xs font-medium bg-green-100 dark:bg-green-900 rounded-full">
            {type}
          </span>
          }
          {disabled &&
            <span className="flex-shrink-0 inline-block px-2 py-0.5 text-orange-800 dark:text-orange-300 text-xs font-medium bg-orange-100 dark:bg-orange-900 rounded-full">
              Coming Soon
            </span>
          }
        </div>
        <p className="mt-1 text-gray-500 dark:text-gray-400 text-sm">{description}</p>
      </div>
    </div>
  </div>
)
