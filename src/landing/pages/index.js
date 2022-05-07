import Head from 'next/head'
import Image from 'next/image'

import {Header} from "../components/header/header";
import Hero from "../components/hero";
import Feature from "../components/feature";

export default function Landing() {
  return (
    <>
      <Hero/>
      <Feature/>
    </> 
  )
}
