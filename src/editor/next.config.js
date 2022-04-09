const removeImports = require( 'next-remove-imports' )()

/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  experimental: {
    outputStandalone: true,
    esmExternals: true
  },
}

module.exports = removeImports( nextConfig )
