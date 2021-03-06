import { BellIcon } from '@heroicons/react/outline'
import { useState } from 'react'

export default function Signup() {

  const [email, setEmail] = useState( '' )
  const [isDisabled, setIsDisabled] = useState( false )
  const [hasSubmitEmail, setHasSubmitEmail] = useState( false )

  const handleSubmit = async ( event ) => {
    event.preventDefault()
    setIsDisabled( true )

    const response = await fetch( `/api/email`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify( { email } )
    } )
    setIsDisabled( false )
    setHasSubmitEmail( true )

    if ( response.ok ) setEmail( '' )
  }

  return (
    <div className="mt-12 sm:mt-16 md:mt-0">
      <h2 className="text-2xl font-extrabold text-gray-900 dark:text-white sm:text-3xl">E-Mail Signup</h2>
      <div className="mt-3">
        <p className="text-lg text-gray-500 dark:text-gray-400">
          Do you want to receive information about new developments or features? Sign up for our newsletter today.
        </p>
      </div>
      <div className="mt-9">
        {!hasSubmitEmail &&
          <form onSubmit={handleSubmit} className="mt-5 sm:flex sm:items-center">
            <div className="w-full sm:max-w-xs">
              <label htmlFor="email" className="sr-only">
                E-Mail
              </label>
              <input
                type="email"
                name="email"
                id="email"
                disabled={isDisabled}
                value={email}
                onChange={e => setEmail( e.target.value )}
                placeholder="you@example.com"
                className="
                    p-2 border-2 rounded-md w-full
                    bg-white dark:bg-gray-900
                    text-gray-900 dark:text-gray-100 text-sm
                    disabled:text-gray-400 disabled:bg-gray-200 disabled:border-gray-200
                    disabled:dark:text-gray-500 disabled:dark:bg-gray-700 disabled:dark:border-gray-700"
              />
            </div>
            <button
              type="submit"
              disabled={isDisabled}
              className="ml-2 bg-blue-500 disabled:bg-gray-400 inline-flex items-center justify-center p-2 px-4 rounded-md text-gray-100 hover:text-gray-50 hover:bg-blue-400 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
            >
              <BellIcon className="block h-5 w-5" aria-hidden="true" />
              <span className="pl-1">Notify Me</span>
            </button>
          </form>
        }
        {hasSubmitEmail &&
          <p className="text-lg font-bold text-blue-500 dark:text-blue-400">
            Thank you for signing up!
          </p>
        }
      </div>
    </div>
  )
}
