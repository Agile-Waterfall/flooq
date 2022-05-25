import { NextPage } from 'next'
import Head from 'next/head'
import { PageTitle } from '../components/page-title'

interface Version {
  tag: string,
  name: string,
  notes: string
}

interface DeveloperPageProps {
  status: string,
  version: Version
}

export const Developer: NextPage<DeveloperPageProps> = ( { status, version }: DeveloperPageProps ) => {
  return (
    <>
      <Head>
        <title>Flooq | Developer</title>
      </Head>
      <PageTitle name="Developer"/>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 py-6 sm:px-0">
            <ul className="text-gray-900 dark:text-gray-100">
              <li><strong>Status</strong> {status}</li>
              <li><strong>DB</strong> {version?.tag === 'unknown' ? 'not connected' : 'connected'}</li>
              <li><strong>Version</strong></li>
              <ul className="pl-2">
                <li><strong>Tag</strong> {version?.tag}</li>
                <li><strong>Name</strong> {version?.name}</li>
                <li><strong>Notes</strong> {version?.notes}</li>
              </ul>
            </ul>
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( ): Promise<any> => {
  const res = await fetch( `${process.env.BASE_URL}/api/status/` )
  const data = await res.json()

  return { props: { ...data } }
}

export default Developer
