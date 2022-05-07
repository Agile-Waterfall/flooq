import Head from 'next/head'
import Image from 'next/image'

import {Header} from "../components/header/header";
import Hero from "../components/hero";
import Feature from "../components/feature";
import CTA from "../components/cta";
import Pricing from "../components/pricing";
import Footer from "../components/footer";

export default function Landing() {
  return (
    <>
      <Hero/>
      <Feature/>
      <CTA/>
      <Pricing/>
      <Footer/>
    </> 
  )
}
