import { NextPage } from 'next'
import { signIn, useSession } from 'next-auth/react'
import Head from 'next/head'
import { useRouter } from 'next/router'
import { useEffect } from 'react'

const Login: NextPage<void> = () => {
  const session = useSession()
  const router = useRouter()
  useEffect( () => {
    if ( !session || session.status === 'unauthenticated' ) {
      signIn( 'flooq', { callbackUrl: '/' } )
    } else {
      router.push( '/' )
    }
  } )

  return (
    <>
      <Head>
        <title>Flooq | Login</title>
      </Head>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          {!session || session.status === 'unauthenticated' &&
            <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
              You will be redirected to the sign in page in a moment.
            </p>
          }
          {session && session.status === 'authenticated' &&
            <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
              You will be redirected to the dashboard in a moment.
            </p>
          }
        </div>
      </main>
    </>
  )
}

export default Login
