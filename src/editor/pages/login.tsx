import { NextPage } from 'next'
import { signIn, signOut, useSession } from 'next-auth/react'
import Head from 'next/head'
import { useEffect } from 'react'

const LogoutDone: NextPage<void> = () => {
  const session = useSession()
  useEffect( () => {
    console.log( session )
    if ( !session || session.status === 'unauthenticated' ) {
      signIn( 'flooq', { callbackUrl: '/' } )
    }
  } )

  return (
    <>
      <Head>
        <title>Flooq | Login</title>
      </Head>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
            You will be redirected to the sign in page in a moment.
          </p>
        </div>
      </main>
    </>
  )
}

export default LogoutDone
