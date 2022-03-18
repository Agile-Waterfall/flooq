import React from 'react'

interface ListItemProps {
  children: any
}

export const ListItem = ( { children }: ListItemProps ): JSX.Element => (
  <div
    className="bg-gray-100 first:rounded-t-md last:rounded-b-md dark:bg-gray-800 border-b border-gray-200 dark:border-gray-600 last:border-none flex flex-row justify-between items-center p-6">
    {children}
  </div>
)
