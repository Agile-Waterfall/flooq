import { ListItem } from '../list/list-item'
import { Button } from '../../components/form/button'

export interface TokenListItemProps {
  name: string;
  deleteToken( name: string ): void
}

export const TokenListItem = ( { name, deleteToken }: TokenListItemProps ): JSX.Element => (
  <ListItem>
    <>
      <div>
        <div className="font-semibold dark:text-gray-100">{name}</div>
      </div>
      <div>
        <Button dangerous onClick={(): void => deleteToken( name )}>Delete</Button>
      </div>
    </>
  </ListItem>
)
