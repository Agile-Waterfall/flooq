import { NextPage } from 'next'
import Head from 'next/head'
import { DataFlowListItem } from '../components/list/data-flow-list-item'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { useState } from 'react'
import { Button } from '../components/form/button'
import { getToken } from 'next-auth/jwt'

interface DashboardProps {
  dataFlows: any
}

export const Dashboard: NextPage<DashboardProps> = ( { dataFlows } ) => {

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
            <Button primary onClick={createNewDataFlow}>
              Add new Data Flow
            </Button>
          </div>
          <div className="px-4 py-6 sm:px-0">
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
  const rawToken = await getToken( { req: context.req, raw: true } )
  const res = await fetch( `${process.env.BASE_URL}/api/flows/list`, {
    headers: {
      Authorization: `Bearer ${rawToken}`
    }
  } )

  if ( !res.ok ) {
    const error = await res.text()
    console.log( error )
    return { props: { dataFlows: [] } }
  }

  const dataFlows = await res.json()

  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )

  return { props: { dataFlows } }
}


export default Dashboard
