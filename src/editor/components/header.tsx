import { Disclosure } from '@headlessui/react'
import { LoginIcon, LogoutIcon, MenuIcon, XIcon } from '@heroicons/react/outline'
import { useRouter } from 'next/router'
import { classNames } from '../helper/class'
import { Logo } from './logo'
import { signIn, useSession } from 'next-auth/react'
import Link from 'next/link'

const navigation = [
  { name: 'Dashboard', href: '/' },
  { name: 'Settings', href: '/settings' },
  { name: 'Developer', href: '/developer' },
]

const profileNavigation = [
  { name: 'Your Profile', href: '/profile' },
  { name: 'Sign out', href: '/sign-out' },
]

export const Header = (): JSX.Element => {
  const router = useRouter()
  const { data: session } = useSession()

  return (
    <Disclosure as="nav" className="bg-gray-800">
      {( { open } ): any => (
        <>
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex items-center justify-between h-16">
              <div className="flex items-center">
                <Logo />
                <div className="hidden md:block">
                  <div className="ml-10 flex items-baseline space-x-4">
                    {navigation.map( ( item ) => (
                      <a
                        key={item.name}
                        href={item.href}
                        className={classNames(
                          router.pathname === item.href
                            ? 'bg-gray-900 text-white'
                            : 'text-gray-300 hover:bg-gray-700 hover:text-white',
                          'px-3 py-2 rounded-md text-sm font-medium'
                        )}
                      >
                        {item.name}
                      </a>
                    ) )}
                  </div>
                </div>
              </div>

              <div className="hidden md:block">
                {session &&
                  <div
                    className="flex gap-2 items-center text-gray-100"
                  >
                    <div>
                      {session.user?.name}
                    </div>
                    <a
                      className="bg-gray-800 inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white cursor-pointer"
                      onClick={(): any => window.location.href = '/api/auth/federated-logout'}
                    >
                      <LogoutIcon className="block h-6 w-6" aria-hidden="true" />
                      <span className="pl-1">Logout</span>
                    </a>
                  </div>
                }
                {!session &&
                  <Link href={`/api/auth/signin`}>
                    <a
                      className="bg-gray-800 inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
                      onClick={( e ): void => {
                        e.preventDefault()
                        signIn( 'flooq', { callbackUrl: 'http://localhost:3000' } )
                      }}
                    >
                      <LoginIcon className="block h-6 w-6" aria-hidden="true" />
                      <span className="pl-1">Login</span>
                    </a>
                  </Link>
                }
              </div>

              <div className="-mr-2 flex md:hidden">
                <Disclosure.Button
                  className="bg-gray-800 inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white">
                  <span className="sr-only">Open main menu</span>
                  {open ? (
                    <XIcon className="block h-6 w-6" aria-hidden="true" />
                  ) : (
                    <MenuIcon className="block h-6 w-6" aria-hidden="true" />
                  )}
                </Disclosure.Button>
              </div>
            </div>
          </div>
          <Disclosure.Panel className="md:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
              {navigation.map( ( item ) => (
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
              {profileNavigation.map( ( item ) => (
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
            </div>
          </Disclosure.Panel>
        </>
      )}
    </Disclosure>
  )
}
