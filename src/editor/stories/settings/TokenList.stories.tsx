import { useState } from 'react'
import { List } from '../../components/list/list'
import { TokenListItem, TokenListItemProps } from '../../components/settings/token-list-item'
import { TokenInsertNew } from '../../components/settings/token-insert-new'
import { ComponentMeta, ComponentStory } from '@storybook/react'
import { object } from '@storybook/addon-knobs'
import { Token } from '../../pages/settings'


export default {
  title: 'Molecules/Tokens',
  component: TokenListItem
} as ComponentMeta<typeof TokenListItem>


const Template: ComponentStory<any> = ( args ) => {
  const [tokens, _setTokens] = useState( Object.values<Token>( args ) )
  return (
    <div style={{ maxWidth: '1440px' }}>
      <List
        title="Tokens"
        description="Tokens are used to authenticate Flooq to servers during dataflow execution"
      >
        <TokenInsertNew
          saveNewToken={async ( name, value ): Promise<void> => console.log( `Adding token with name "${name}" and value "${value}"` )}
        />
        {tokens.map( token =>
          <TokenListItem
            name={token.Name}
            key={token.Id}
            id={token.Id}
            deleteToken={async ( id ): Promise<void> => console.log( `Deleting token with ID "${id}"` )}
          /> )}
      </List>
    </div>
  )
}

export const TokenList = Template.bind( {} )

TokenList.args = object( 'List of Token', [
  {
    Id: 'ID_1',
    Name: 'TOKEN_1'
  }
] )
