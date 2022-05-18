import { ListItem } from '../list/list-item'
import { Button } from '../form/button'
import { Input } from '../form/input'
import { useState, FormEvent } from 'react'

interface TokenInsertNewProps {
  saveNewToken( name: string, value: string ): Promise<void>
}

export const TokenInsertNew = ( { saveNewToken }: TokenInsertNewProps ): JSX.Element => {
  const [name, setName] = useState<string>( '' )
  const [value, setValue] = useState<string>( '' )

  const save = async ( e: FormEvent<HTMLFormElement> ): Promise<void> => {
    e.preventDefault()
    await saveNewToken( name, value )
    setName( '' )
    setValue( '' )
  }

  return (
    <ListItem>
      <form onSubmit={save} className="w-full">
        <div className="flex flex-wrap justify-between items-end w-full gap-2">
          <Input
            className="md:flex-2"
            label="Token name"
            value={name}
            onChange={( e: React.ChangeEvent<HTMLInputElement> ): void => setName( e.target.value )}
          />
          <Input
            className="md:flex-1"
            label="Token value"
            value={value}
            onChange={( e: React.ChangeEvent<HTMLInputElement> ): void => setValue( e.target.value )}
          />
          <Button
            primary
            type="submit"
            disabled={!name || name.length === 0 || !value || value.length === 0}
          >
            Save
          </Button>
        </div>
      </form>
    </ListItem>
  )
}
