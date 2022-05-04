import { NextPage } from 'next'
import { signOut } from 'next-auth/react'
import Head from 'next/head'
import { useEffect } from 'react'

const LogoutDone: NextPage<void> = () => {
  useEffect( () => {
    setTimeout( () => {
      signOut( { callbackUrl: window.location.origin } )
    }, 500 )
  } )

  return (
    <>
      <Head>
        <title>Flooq | Logout</title>
      </Head>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div>Please wait while we sign you out!</div>
        </div>
      </main>
    </>
  )
}

export default LogoutDone
