import { NextPage } from 'next'
import { signIn, useSession } from 'next-auth/react'
import Head from 'next/head'
import { useRouter } from 'next/router'
import { useEffect } from 'react'

const Login: NextPage<void> = () => {
  const session = useSession()
  const router = useRouter()
  useEffect( () => {
    if( !router.isReady ) {
      return
    }

    if ( router.query.error ) {
      return
    }
    if ( !session || session.status === 'unauthenticated' ) {
      signIn( 'flooq', { callbackUrl: '/' } )
    } else {
      router.push( '/' )
    }
  }, [router, session] )

  return (
    <>
      <Head>
        <title>Flooq | Login</title>
      </Head>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          {router.query.error &&
            <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
              Something went wrong while signing you in. Please try again later or contact <a className="text-blue-500" href="mailto:info@flooq.io">info@flooq.io</a> for help.
            </p>
          }
          {!router.query.error &&
            <>
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
            </>
          }
        </div>
      </main>
    </>
  )
}

export default Login
