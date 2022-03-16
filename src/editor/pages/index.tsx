import Head from 'next/head'
import { DataFlowListItem } from '../components/list/data-flow-list-item'
import { List } from '../components/list/list'
import { PageTitle } from '../components/page-title'
import { TsConfigJson } from 'type-fest'
import JSX = TsConfigJson.CompilerOptions.JSX;

export const Dashboard = ( { data }: any ): JSX.Element => {
  console.log( data )
  return (
    <>
      <Head>
        <title>Flooq | Dashboard</title>
      </Head>
      <PageTitle name="Dashboard" />
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 py-6 sm:px-0">
            <List>
              {/* data.map( ( flow: any, i: number ) => <DataFlowListItem {...flow} key={i} /> )*/}
            </List>
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const res = await fetch( `http://localhost:3000/api/flows/` )
  const data = await res.json()

  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )

  return { props: { data } }
}


export default Dashboard
