import PageTitle from "../components/page-title";
import Head from 'next/head'

export default function Privacy() {
  return (
    <>
      <Head>
        <title>Flooq | Privacy Policy</title>
      </Head>
      <PageTitle name="Privacy Policy" />
      <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
          Our privacy policy.
        </p>
      </div>
    </>
  )
}