import { CheckCircleIcon, XCircleIcon } from '@heroicons/react/solid'

interface ListItemStatusProps {
  status: string
}

export const ListItemStatus = ( { status }: ListItemStatusProps ): JSX.Element => {
  const getStatusIcon = ( status: string ): JSX.Element => {
    switch ( status ) {
      case 'Active':
        return <CheckCircleIcon className="text-emerald-400 h-4 w-4" />
      case 'Disabled':
        return <XCircleIcon className="text-red-400 h-4 w-4" />
    }

    return <></>
  }

  const icon = getStatusIcon( status )

  return (
    <div className="flex items-center">
      {icon}
      <div className="m-1 dark:text-gray-100">{status}</div>
    </div>
  )
}
