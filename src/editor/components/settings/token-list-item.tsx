import { ListItem } from '../list/list-item'
import { Button } from '../../components/form/button'

export interface TokenListItemProps {
  name: string;
  deleteFunction( ): void
}

export const TokenListItem = ( { name, deleteFunction }: TokenListItemProps ): JSX.Element => (
  <ListItem>
    <div>
      <div className="font-semibold dark:text-gray-100">{name}</div>
    </div>
    <div>
      <Button onClick={deleteFunction}>Delete</Button>
    </div>
  </ListItem>
)
