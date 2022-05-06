import { ArrowRightIcon } from '@heroicons/react/outline'
import Link from 'next/link'
import { ListItem } from '../list/list-item'
import { ListItemStatus } from './list-item-status'

export interface DataFlowListItemProps {
  id: number,
  name: string,
  lastEdited: string,
  status: any
}

export const DataFlowListItem = ( { id, name, lastEdited, status }: DataFlowListItemProps ): JSX.Element => (
  <ListItem>
    <div>
      <Link href={`/flows/${id}`}>
        <a>
          <div className="font-semibold dark:text-gray-100">{name}</div>
          <div className="text-gray-500 dark:text-gray-400 text-xs">Last Edited: {lastEdited}</div>
        </a>
      </Link>
    </div>
    <ListItemStatus status={status}/>
    <Link href={`/flows/${id}`}>
      <a
        className="dark:bg-gray-800 p-1 rounded-full text-gray-400 hover:text-black dark:hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white">
        <ArrowRightIcon className="block h-5 w-5"/>
      </a>
    </Link>
  </ListItem>
)
