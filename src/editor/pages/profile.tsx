import { NextPage } from 'next'
import { getSession } from 'next-auth/react'
import Head from 'next/head'
import { PageTitle } from '../components/page-title'

export const Profile: NextPage = () => {
  return (
    <>
      <Head>
        <title>Flooq | Profile</title>
      </Head>
      <PageTitle name="Profile" />
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 py-6 sm:px-0">
            <div className="border-4 border-dashed border-gray-200 rounded-lg h-96" />
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const session = await getSession( context )
  if ( !session ) {
    return {
      redirect: {
        destination: '/login',
        permanent: false,
      },
    }
  }

  return {
    props: {}
  }
}


export default Profile
