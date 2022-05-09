import { ListItem } from '../list/list-item'
import { Button } from '../form/button'
import { Input } from '../form/input'
import { useState } from 'react'

interface TokenInsertNewProps {
  saveNewToken( name: string, value: string ): void
}

export const TokenInsertNew = ( { saveNewToken }: TokenInsertNewProps ): JSX.Element => {
  const [name, setName] = useState<string>( '' )
  const [value, setValue] = useState<string>( '' )

  return (
    <ListItem>
      <>
        <div>
          <Input
            label="Token name"
            value={name}
            onChange={( e: React.ChangeEvent<HTMLInputElement> ): void => setName( e.target.value )}
          />
        </div>
        <div>
          <Input
            label="Token value"
            value={value}
            onChange={( e: React.ChangeEvent<HTMLInputElement> ): void => setValue( e.target.value )}
          />
        </div>
        <div>
          <Button
            primary
            disabled={!name || name.length === 0 || !value || value.length === 0}
            onClick={(): void => saveNewToken( name, value )}
          >Save</Button>
        </div>
      </>
    </ListItem>
  )
}
