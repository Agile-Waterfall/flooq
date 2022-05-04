import { NextPage } from 'next'
import Head from 'next/head'
import { useSession } from 'next-auth/react'
import { Hero } from '../components/landingPage/hero'
import { Features } from '../components/landingPage/features'
import { CTA } from '../components/landingPage/cta'
import { Pricing } from '../components/landingPage/pricing'
import { Footer } from '../components/landingPage/footer'

interface LandingPageProps {
  props?: any
}

export const LandingPage: NextPage<LandingPageProps> = ( { props } ) => {
  const { data: session } = useSession()

  return (
    <>
      <Head>
        <title>Flooq</title>
      </Head>
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <h1><b>Landing Page</b></h1>
          <Hero/>
          <Features/>
          <CTA/>
          <Pricing/>
          <Footer/>
        </div>
      </main>
    </>
  )
}

export default LandingPage
