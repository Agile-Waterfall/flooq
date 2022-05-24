import remarkGfm from 'remark-gfm'
import generateWithMDX from '@next/mdx'

/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  experimental: {
    outputStandalone: true,
    esmExternals: true
  },
}

const withMDX = generateWithMDX( {
  options: {
    remarkPlugins: [remarkGfm],
  },
  extension: /\.(md|mdx)$/,
} )

export default withMDX( {
  pageExtensions: ['ts', 'tsx', 'js', 'jsx', 'md', 'mdx'],
  ...nextConfig
} )
