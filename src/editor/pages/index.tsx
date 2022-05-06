import { NextPage } from 'next'
import Head from 'next/head'
import { DataFlowListItem } from '../components/dashboard/data-flow-list-item'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { useState } from 'react'
import { Button } from '../components/form/button'
import { useSession } from 'next-auth/react'

interface DashboardProps {
  dataFlows: any
}

export const Dashboard: NextPage<DashboardProps> = ( { dataFlows } ) => {
  const { data: session } = useSession()
  const [dataFlowsList, setListData] = useState( dataFlows )

  const createNewDataFlow = async (): Promise<void> => {
    const response = await fetch( '/api/flows/create' )
    const newFlow = await response.json()
    setListData( [...dataFlowsList, newFlow] )
  }

  return (
    <>
      <Head>
        <title>Flooq | Dashboard</title>
      </Head>
      <PageTitle name="Dashboard" />
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 sm:px-0">
            <Button primary onClick={createNewDataFlow} disabled={!session}>
              Add new Data Flow
            </Button>
          </div>
          <div className="px-4 py-6 sm:px-0">
            {!session && dataFlowsList.length === 0 &&
              <span className="text-gray-900 dark:text-gray-100">Login to see your DataFlows.</span>
            }
            {session && dataFlowsList.length === 0 &&
              <span className="text-gray-900 dark:text-gray-100">You do not have any DataFlows yet. Add a new one to get started.</span>
            }
            <List>
              {dataFlowsList?.map( ( flow: any, i: number ) => <DataFlowListItem {...flow} key={i} /> )}
            </List>
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const res = await fetch( `${process.env.BASE_URL}/api/flows/list`, {
    headers: context.req.headers
  } )

  const dataFlows = await res.json()
  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )
  return { props: { dataFlows } }
}


export default Dashboard
