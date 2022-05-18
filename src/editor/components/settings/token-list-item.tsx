import { ListItem } from '../list/list-item'
import { Button } from '../../components/form/button'
import { LockClosedIcon } from '@heroicons/react/outline'
import { useState } from 'react'
import { DeleteTokenAction } from '../actions/delete-token-action'

export interface TokenListItemProps {
  name: string;
  id: string;
  deleteToken( id: string ): void
}

export const TokenListItem = ( { name, id, deleteToken }: TokenListItemProps ): JSX.Element => {
  const [openDeleteAction, setOpenDeleteAction] = useState( false )

  return (
    <ListItem>
      <>
        <DeleteTokenAction open={openDeleteAction} setOpen={setOpenDeleteAction} onDelete={(): void => deleteToken( id )} />
        <div className=" text-green-700 dark:text-green-300 flex items-center justify-between gap-4 w-full">
          <LockClosedIcon className="h-6 w-6 flex-grow-0 flex-shrink-0" />
          <div className="font-mono break-all flex-1 flex-grow">{name}</div>
          <Button dangerous onClick={(): void => setOpenDeleteAction( true )}>Delete</Button>
        </div>
      </>
    </ListItem>
  )
}
