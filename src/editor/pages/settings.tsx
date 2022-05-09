import { NextPage } from 'next'
import Head from 'next/head'
import { useState } from 'react'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { TokenListItem } from '../components/settings/token-list-item'
import { TokenInsertNew } from '../components/settings/token-insert-new'
import { Message, MessageType } from '../components/message'

interface SettingsProps {
  tokens: string[];
}

export const Settings: NextPage<SettingsProps> = ( { tokens: t }: SettingsProps ) => {
  const [tokens, setTokens] = useState( t )
  const [message, setMessage] = useState<Message>( )

  const updateMessage =( message: Message ): void => {
    setMessage( message )
    setTimeout( () => setMessage( undefined ), 1500 )
  }

  const deleteToken = ( name: string ): void => {
    const res = { ok: true }; console.log( 'deleted', name ) // await fetch( `/api/token/${name}`, { method: 'DELETE' } )
    if ( res.ok ) {
      setTokens( tokens.filter( e => e !== name ) )
      updateMessage( { text: 'Successfully deleted Token.', type: MessageType.Info } )
    } else {
      updateMessage( { text: 'Error while deleting Token.', type: MessageType.Error } )
    }
  }

  const saveNewToken = ( name: string, value: string ): void => {
    if ( tokens.includes( name ) ) updateMessage( { text: 'Token name already exists.', type: MessageType.Error } )
    else {
      const res = { ok: true }; console.log( 'added', name, value ) // await fetch( `/api/token`, { method: 'POST', body: JSON.stringify( { name, value } ) } )
      if ( res.ok ) {
        tokens.push( name )
        setTokens( tokens )
        updateMessage( { text: 'Successfully saved Token.', type: MessageType.Info } )
      } else {
        updateMessage( { text: 'Error while saving Token.', type: MessageType.Error } )
      }
    }
  }

  return (
    <>
      <Head>
        <title>Flooq | Settings</title>
      </Head>
      <PageTitle name="Settings" message={message}/>

      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <List title="Tokens" description="Tokens are used to authenticate Flooq to servers during dataflow execution">
            <TokenInsertNew saveNewToken={saveNewToken}/>
            { tokens.map( tokenName => <TokenListItem name={tokenName} key={tokenName} deleteToken={deleteToken}/> ) }
          </List>
        </div>
      </main>
    </>
  )
}


export const getServerSideProps = async ( ): Promise<{'props': SettingsProps}> => {
  const tokens = ['a', 'b', 'c'] // await fetch( `${process.env.BASE_URL}/api/tokens/` ).then(res => res.json())
  return { props: { tokens } }
}


export default Settings
