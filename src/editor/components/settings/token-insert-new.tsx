import { ListItem } from '../list/list-item'
import { Button } from '../form/button'
import { Input } from '../form/input'
import { useState } from 'react'

interface TokenInsertNewProps {
  saveNewToken( name: string, value: string ): Promise<void>
}

export const TokenInsertNew = ( { saveNewToken }: TokenInsertNewProps ): JSX.Element => {
  const [name, setName] = useState<string>( '' )
  const [value, setValue] = useState<string>( '' )

  const save = async (): Promise<void> => {
    await saveNewToken( name, value )
    setName( '' )
    setValue( '' )
  }

  return (
    <ListItem>
      <div className="flex flex-wrap justify-between items-end w-full gap-2">
        <Input
          label="Token name"
          value={name}
          onChange={( e: React.ChangeEvent<HTMLInputElement> ): void => setName( e.target.value )}
        />
        <Input
          label="Token value"
          value={value}
          onChange={( e: React.ChangeEvent<HTMLInputElement> ): void => setValue( e.target.value )}
        />
        <Button
          primary
          disabled={!name || name.length === 0 || !value || value.length === 0}
          onClick={save}
        >
            Save
        </Button>
      </div>
    </ListItem>
  )
}
