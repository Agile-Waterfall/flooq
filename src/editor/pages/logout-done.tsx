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
          <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
            Please wait while we sign you out!
          </p>
        </div>
      </main>
    </>
  )
}

export default LogoutDone
