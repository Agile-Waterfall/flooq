import { Disclosure } from '@headlessui/react'
import { LoginIcon, UserIcon } from '@heroicons/react/outline'
import { useRouter } from 'next/router'

import { classNames } from '../../helper/class'

import { Logo } from './logo'
import Link from 'next/link'

const publicNavigation = [
  { name: 'Features', href: '/#features' },
  { name: 'Pricing', href: '/#pricing' },
  { name: 'FAQ', href: '/#faq' },
  { name: 'Contact', href: '/#contact' },
]

export const Header = () => {
  const router = useRouter()

  return (
    <div className="mt-16">
      <Disclosure as="nav" className="fixed w-full z-40 bg-gray-100 dark:bg-gray-900 top-0">
        <>
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex items-center justify-between h-16">
              <div className="flex items-center">
                <Link href="/">
                  <a>
                    <Logo />
                  </a>
                </Link>
                <div className="hidden md:block">
                  <div className="ml-10 flex items-baseline space-x-4">
                    {publicNavigation.map( ( item ) => (
                      <a
                        key={item.name}
                        href={item.href}
                        className={classNames(
                          router.pathname === item.href
                            ? 'bg-gray-900 text-white'
                            : 'text-gray-600 hover:text-gray-900 dark:text-gray-200 hover:bg-gray-300 hover:dark:bg-gray-700',
                          'px-3 py-2 rounded-md text-sm font-medium'
                        )}
                      >
                        {item.name}
                      </a>
                    ) )}
                  </div>
                </div>
              </div>
              <div className="hidden md:flex gap-4">
                <Link href={process.env.NEXT_PUBLIC_EDITOR_URL}>
                  <a
                    className="bg-blue-500 inline-flex items-center justify-center p-2 px-4 rounded-md text-gray-100 hover:text-gray-50 hover:bg-blue-400 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
                  >
                    <LoginIcon className="block h-6 w-6" aria-hidden="true" />
                    <span className="pl-1">Login</span>
                  </a>
                </Link>
                <Link href={process.env.NEXT_PUBLIC_EDITOR_URL}>
                  <a
                    className="bg-gray-300 hover:bg-gray-20 dark:bg-gray-800 hover:dark:bg-gray-600 text-gray-700 hover:text-gray-900 dark:text-gray-300 0 inline-flex items-center justify-center p-2 px-4 rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
                  >
                    <UserIcon className="block h-6 w-6" aria-hidden="true" />
                    <span className="pl-1">Register</span>
                  </a>
                </Link>
              </div>
            </div>
          </div>
          <Disclosure.Panel className="md:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
              {publicNavigation.map( ( item ) => (
                <Disclosure.Button
                  key={item.name}
                  as="a"
                  href={item.href}
                  className={classNames(
                    router.pathname === item.href
                      ? 'bg-gray-900 text-white'
                      : 'text-gray-300 hover:bg-gray-700 hover:text-white',
                    'block px-3 py-2 rounded-md text-base font-medium'
                  )}
                >
                  {item.name}
                </Disclosure.Button>
              ) )}
              <hr className="border-gray-500" />
              <Link href={process.env.NEXT_PUBLIC_EDITOR_URL}>
                <a
                  className="bg-gray-800 flex items-center justify-start p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
                >
                  <LoginIcon className="block h-6 w-6" aria-hidden="true" />
                  <span className="pl-1">Login</span>
                </a>
              </Link>
            </div>
          </Disclosure.Panel>
        </>
      </Disclosure>
    </div>
  )
}
