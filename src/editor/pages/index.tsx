import { NextPage } from 'next'
import Head from 'next/head'
import { DataFlowListItem } from '../components/list/data-flow-list-item'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { useState } from 'react'
import { Button } from '../components/form/button'
import { User } from '../services/user-service'

interface DashboardProps {
  user: User,
  dataFlows: any
}

export const Dashboard: NextPage<DashboardProps> = ( { dataFlows, user } ) => {

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
      <PageTitle name="Dashboard"/>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 sm:px-0">
            <Button primary onClick={createNewDataFlow}>
              Add new Data Flow
            </Button>
          </div>
          <div className="px-4 py-6 sm:px-0">
            <List>
              {dataFlowsList?.map( ( flow: any, i: number ) => <DataFlowListItem {...flow} key={i}/> )}
            </List>
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const res = await fetch( `${process.env.BASE_URL}/api/flows/list` )
  const dataFlows = await res.json()

  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )

  const user: User = {
    email: 'felix@saaro.ch',
    name: 'Felix'
  }

  return { props: { dataFlows, user } }
}


export default Dashboard
