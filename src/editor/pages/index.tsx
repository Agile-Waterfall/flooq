import { NextPage } from 'next'
import Head from 'next/head'
import { ListItem } from '../components/list/list-item'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { useState } from 'react'
import { getSession, useSession } from 'next-auth/react'
import dayjs from 'dayjs'

interface DashboardProps {
  dataFlows: any
  error: any
}

export const Dashboard: NextPage<DashboardProps> = ( { dataFlows } ) => {
  const { data: session } = useSession()
  const [dataFlowsList, setListData] = useState( dataFlows )

  const createNewDataFlow = async (): Promise<void> => {
    const response = await fetch( '/api/flows/create' )
    const newFlow = await response.json()
    setListData( [...dataFlowsList, newFlow] )
  }

  const getDescription = (): string => {
    if ( !session && dataFlowsList.length === 0 ) {
      return 'Login to see your DataFlows.'
    }
    if ( session && dataFlowsList.length === 0 ) {
      return 'You do not have any DataFlows yet. Create one to get started.'
    }
    return 'These are the DataFlows you have access to.'
  }

  const byLastEdited = ( a: any, b: any ): number => dayjs( b.lastEdited ).diff( dayjs( a.lastEdited ), 'seconds' )

  return (
    <>
      <Head>
        <title>Flooq | Dashboard</title>
      </Head>
      <PageTitle name="Dashboard" />
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 py-6 sm:px-0 flex flex-col">
            <List
              title="DataFlows"
              description={getDescription()}
              action={{
                label: 'Create',
                disabled: !session,
                onClick: createNewDataFlow
              }}
            >
              {dataFlowsList.sort( byLastEdited )?.map( ( flow: any, i: number ) => <ListItem {...flow} key={i} /> )}
            </List>
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

  const res = await fetch( `${process.env.BASE_URL}/api/flows/list`, {
    headers: context.req.headers
  } )

  if ( !res.ok ) {
    context.res.statusCode = res.status
    return { props: { dataFlows: [], error: `${res.status} ${res.statusText}` } }
  }

  const dataFlows = await res.json()
  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )
  return {
    props: { dataFlows }
  }
}


export default Dashboard
