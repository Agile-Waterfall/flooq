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
        <title>Flooq | Status</title>
      </Head>
      <PageTitle name="Status" />
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 py-6 sm:px-0">
            <ul>
              <li><strong>Status:</strong> {status}</li>
              <li><strong>Version:</strong> {version.name}</li>
            </ul>
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const res = await fetch( `${process.env.BASE_URL}/api/status/` )
  const data = await res.json()

  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )

  return { props: { ...data } }
}



export default Developer
