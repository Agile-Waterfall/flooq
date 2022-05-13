import { ListItem } from '../list/list-item'
import { Button } from '../../components/form/button'
import { LockClosedIcon } from '@heroicons/react/outline'

export interface TokenListItemProps {
  name: string;
  id: string;
  deleteToken( name: string ): void
}

export const TokenListItem = ( { name, id, deleteToken }: TokenListItemProps ): JSX.Element => (
  <ListItem>
    <>
      <div>
        <div className="font-mono text-green-700 dark:text-green-300 flex">
          <LockClosedIcon className="s-6 w-6 mr-3 my-auto"/>
          <div className="break-all">{name}</div>
        </div>
      </div>
      <div>
        <Button dangerous onClick={(): void => deleteToken( id )}>Delete</Button>
      </div>
    </>
  </ListItem>
)
