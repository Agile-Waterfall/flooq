import { ListItem } from '../list/list-item'
import { Button } from '../form/button'
import { Input } from '../form/input'

export interface TokenInsertNewProps {
  name: string;
  value: string;
  updateName( newName: string ): void
  updateValue( newValue: string ): void
  saveNewToken( name: string, value: string ): Promise<any>;
}

export const TokenInsertNew = ( { name, value, updateName, updateValue, saveNewToken }: TokenInsertNewProps ): JSX.Element => (
  <ListItem>
    <div>
      <Input label={'Token name'} value={name} onChange={( e ): void => updateName( e.target.value )}></Input>
    </div>
    <div>
      <Input label={'Token value'} value={value} onChange={( e ): void => updateValue( e.target.value )}></Input>
    </div>
    <div>
      <Button onClick={async (): Promise<any> => await saveNewToken( name, value )}>Save</Button>
    </div>
  </ListItem>
)
