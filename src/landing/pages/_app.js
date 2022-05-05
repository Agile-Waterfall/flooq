import Script from 'next/script'
import {Header} from '../components/header'
import Head from 'next/head'
import '../styles/globals.scss'

const App = ({Component, pageProps}) => (
  <>
    <Head>
      <link rel="apple-touch-icon" sizes="180x180" href="/apple-touch-icon.png"/>
      <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png"/>
      <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png"/>
      <link rel="manifest" href="/site.webmanifest"/>
      <link rel="mask-icon" href="/safari-pinned-tab.svg" color="#fbbf24"/>
      <meta name="msapplication-TileColor" content="#2b5797"/>
      <meta name="theme-color" content="#ffffff"/>
      <title>Flooq</title>
    </Head>
    <Script
      src="/theme.js"
      strategy="beforeInteractive"
    />
    <div className="flex flex-col full-height bg-white dark:bg-gray-900">
      <Header/>
      <div className="flex-1">
        <Component {...pageProps} />
      </div>
    </div>
  </>
)

export default App
