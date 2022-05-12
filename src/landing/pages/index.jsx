import Hero from '../components/hero'
import Features from '../components/feature'
import Pricing from '../components/pricing'
import FAQ from '../components/faq'
import Contact from '../components/contact'
import CTA from '../components/cta'

export default function Landing() {
  return (
    <>
      <Hero />
      <Features />
      <CTA />
      <Pricing />
      <FAQ />
      <Contact />
    </>
  )
}
