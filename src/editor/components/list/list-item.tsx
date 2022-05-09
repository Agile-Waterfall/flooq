export interface ListItem {
  children: JSX.Element
}

export const ListItem = ( { children }: ListItem ): JSX.Element => (
  <div className="bg-white first:rounded-t-md last:rounded-b-md dark:bg-gray-800 border-b border-gray-200 dark:border-gray-600 last:border-none flex flex-row justify-between items-center p-6">
    {children}
  </div>
)
