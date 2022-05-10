import Footer from "../components/footer"
import PageTitle from "../components/page-title";
import Head from 'next/head'

export default function Privacy() {
  return (
    <>
      <Head>
        <title>Flooq | Developer</title>
      </Head>
      <PageTitle name="Privacy"/>
      <p>
        Privacy Text here!
      </p>
      <Footer/>
    </>
  )
}