import { ArrowRightIcon, ClockIcon } from '@heroicons/react/outline'
import dayjs from 'dayjs'
import Link from 'next/link'
import { ListItem } from '../list/list-item'
import { ListItemStatus } from './list-item-status'

export interface DataFlowListItemProps {
  id: number,
  name: string,
  lastEdited: string,
  status: any
}

const getLastEditedString = ( lastEdited: string ): string => {
  const now = dayjs()
  const edited = dayjs( lastEdited )
  const diffSeconds = now.diff( edited, 'seconds' )

  if ( diffSeconds < 60 ) {
    return `${now.diff( edited, 'seconds' )}s ago`
  } else if ( diffSeconds < 60 * 60 ) {
    return `${now.diff( edited, 'minutes' )}min ago`
  } else if ( diffSeconds < 60 * 60 * 24 ) {
    return `${now.diff( edited, 'hours' )}h ago`
  } else {
    return `${now.diff( edited, 'days' )} days ago`
  }
}


export const DataFlowListItem = ( { id, name, lastEdited, status }: DataFlowListItemProps ): JSX.Element => (
  <ListItem>
    <>
      <div className="flex flex-col sm:flex-row">
        <Link href={`/flows/${id}`}>
          <a className="sm:w-96 w-52 mr-2">
            <div className="font-semibold text-blue-500 dark:text-blue-400 truncate">{name}</div>
            <div className="text-gray-500 dark:text-gray-400 text-xs flex my-1">
              <ClockIcon className="h-4 w-4 mr-1" />
              <strong className="mr-1">Last Edited:</strong>{getLastEditedString( lastEdited )}
            </div>
          </a>
        </Link>
        <ListItemStatus status={status} />
      </div>
      <Link href={`/flows/${id}`}>
        <a
          className="dark:bg-gray-800 p-1 rounded-full text-gray-400 hover:text-black dark:hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white">
          <ArrowRightIcon className="block h-5 w-5" />
        </a>
      </Link>
    </>
  </ListItem>
)
