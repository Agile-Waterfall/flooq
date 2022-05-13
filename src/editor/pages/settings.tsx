import { NextPage } from 'next'
import Head from 'next/head'
import { useState } from 'react'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { TokenListItem } from '../components/settings/token-list-item'
import { TokenInsertNew } from '../components/settings/token-insert-new'
import { Message, MessageType } from '../components/message'

interface Token {
  Id: string;
  Name: string;
}

interface SettingsProps {
  tokens: Token[];
}

export const Settings: NextPage<SettingsProps> = ( { tokens: t }: SettingsProps ) => {
  const [tokens, setTokens] = useState( t )
  const [globalMessage, setMessage] = useState<Message>()

  let messageTimeout: NodeJS.Timeout

  const updateMessage = ( message: Message ): void => {
    setMessage( message )
    clearTimeout( messageTimeout )
    messageTimeout = setTimeout( () => setMessage( undefined ), 1500 )
  }

  const deleteToken = async ( id: string ): Promise<void> => {
    const res = await fetch( `/api/token/delete?${new URLSearchParams( { id } )}`, { method: 'DELETE' } )
    if ( res.ok ) {
      setTokens( tokens.filter( e => e.Id !== id ) )
      updateMessage( { text: 'Successfully deleted Token.', type: MessageType.Info } )
    } else {
      updateMessage( { text: 'Error while deleting Token.', type: MessageType.Error } )
    }
  }

  const saveNewToken = async ( name: string, value: string ): Promise<void> => {
    if ( tokens.map( token => token.Name ).includes( name ) ) {
      updateMessage( { text: 'Token name already exists.', type: MessageType.Error } )
      return Promise.reject()
    } else {
      const res = await fetch( `/api/token/create`, { method: 'POST', body: JSON.stringify( { name, value } ) } )
      if ( res.ok ) {
        updateMessage( { text: 'Successfully saved Token.', type: MessageType.Info } )
        return fetch( '/api/token/list' )
          .then( r => r.json() )
          .then( setTokens )
          .catch( () => {
            updateMessage( { text: 'Could not retrieved saved tokens', type: MessageType.Error } )
            return Promise.reject()
          } )
      } else {
        updateMessage( { text: 'Error while saving Token.', type: MessageType.Error } )
        return Promise.reject()
      }
    }
  }

  return (
    <>
      <Head>
        <title>Flooq | Settings</title>
      </Head>
      <PageTitle name="Settings" message={globalMessage} />

      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <List title="Tokens" description="Tokens are used to authenticate Flooq to servers during dataflow execution">
            <TokenInsertNew saveNewToken={saveNewToken} />
            {tokens.map( token => <TokenListItem name={token.Name} key={token.Id} id={token.Id} deleteToken={deleteToken} /> )}
          </List>
        </div>
      </main>
    </>
  )
}


export const getServerSideProps = async ( context: any ): Promise<{ 'props': SettingsProps }> => {
  const res = await fetch( `${process.env.BASE_URL}/api/token/list`, { headers: context.req.headers } )
  if ( !res.ok ) {
    return { props: { tokens: [] } }
  }
  const tokens = await res.json()
  return { props: { tokens } }
}


export default Settings
