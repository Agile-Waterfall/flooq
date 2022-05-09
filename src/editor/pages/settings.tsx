import { NextPage } from 'next'
import Head from 'next/head'
import { useState } from 'react'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { TokenListItem } from '../components/settings/token-list-item'
import { TokenInsertNew } from '../components/settings/token-insert-new'

interface SettingsProps {
  tokens: string[];
  newTokenName: string;
  newTokenValue: string;
}

export const Settings: NextPage<SettingsProps> = ( { tokens: t, newTokenName:nTN, newTokenValue:nTV }: SettingsProps ) => {
  const [tokens, updateTokens] = useState( t )
  const [newTokenName, updateNewTokenName] = useState( nTN )
  const [newTokenValue, updateNewTokenValue] = useState( nTV )

  const deleteToken =  ( name: string ): () => Promise<any> => async (): Promise<any> => {
    const res = { ok: true }; console.log( 'deleted', name ) // await fetch( `/api/token/${name}`, { method: 'DELETE' } )
    if ( res.ok ) {
      updateTokens( tokens.filter( e => e !== name ) )
      return Promise.resolve( `Deleted Token ${name}` )
    } else {
      return Promise.reject( `Could not delete Token ${name}` ) // TODO: where do we catch these?
    }
  }

  const saveNewToken = ( name: string, value: string ): ( ) => Promise<any> => async (): Promise<any> => {
    const res = { ok: true }; console.log( 'added', name, value ) // await fetch( `/api/token`, { method: 'POST', body: JSON.stringify( { name, value } ) } )
    if ( res.ok ) {
      tokens.push( newTokenName )
      updateTokens( tokens )
      updateNewTokenName( '' )
      updateNewTokenValue( '' )
      return Promise.resolve( `Added Token ${name}` )
    } else {
      return Promise.reject( `Could not add Token ${name}` ) // TODO: where do we catch these?
    }

  }

  return (
    <>
      <Head>
        <title>Flooq | Settings</title>
      </Head>
      <PageTitle name="Settings"/>

      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <List title="Tokens" description="" action={{ label: 'Create', onClick: console.log }}>
            <>
              { tokens.map( e => <TokenListItem name={e} key={e} deleteFunction={deleteToken( e )}></TokenListItem> ) }
              <TokenInsertNew
                name={newTokenName}
                value={newTokenValue}
                updateName={updateNewTokenName}
                updateValue={updateNewTokenValue}
                saveNewToken={saveNewToken( newTokenName, newTokenValue )}
              ></TokenInsertNew>
            </>
          </List>
        </div>
      </main>
    </>
  )
}


export const getServerSideProps = async ( ): Promise<{'props': SettingsProps}> => {
  const tokens = ['a', 'b', 'c'] // await fetch( `${process.env.BASE_URL}/api/tokens/` ).then(res => res.json())
  return { props: { tokens, newTokenName: '', newTokenValue: '' } }
}


export default Settings
