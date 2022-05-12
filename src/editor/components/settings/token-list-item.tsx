import { ListItem } from '../list/list-item'
import { Button } from '../../components/form/button'

export interface TokenListItemProps {
  name: string;
  id: string;
  deleteToken( name: string ): void
}

export const TokenListItem = ( { name, id, deleteToken }: TokenListItemProps ): JSX.Element => (
  <ListItem>
    <>
      <div>
        <div className="font-semibold dark:text-gray-100">{name}</div>
      </div>
      <div>
        <Button dangerous onClick={(): void => deleteToken( id )}>Delete</Button>
      </div>
    </>
  </ListItem>
)
