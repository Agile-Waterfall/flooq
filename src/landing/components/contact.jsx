import { BellIcon, LibraryIcon, MailIcon, MailOpenIcon, PhoneIcon } from '@heroicons/react/outline'

export default function Contact() {
  return (
    <div id="contact" className="bg-white dark:bg-gray-800">
      <div className="max-w-7xl mx-auto py-16 px-4 sm:px-6 lg:px-8">
        <div className="max-w-lg mx-auto md:max-w-none md:grid md:grid-cols-2 md:gap-8">

          <div className="mt-12 sm:mt-16 md:mt-0">
            <h2 className="text-2xl font-extrabold text-gray-900 dark:text-white sm:text-3xl">Contact us</h2>
            <div className="mt-3">
              <p className="text-lg text-gray-500 dark:text-gray-400">
                Do you have a question or any issue with our services? just send us a quick e-mail.
              </p>
            </div>
            <div className="mt-9">
              <div className="mt-6 flex">
                <div className="flex-shrink-0">
                  <MailIcon className="h-6 w-6 text-gray-400" aria-hidden="true" />
                </div>
                <div className="ml-3 text-base text-gray-500 dark:text-gray-400">
                  <a href="mailto:info@flooq.ioe">info@flooq.io</a>
                </div>
              </div>
            </div>
          </div>

          <div className="mt-12 sm:mt-16 md:mt-0">
            <h2 className="text-2xl font-extrabold text-gray-900 dark:text-white sm:text-3xl">E-mail signup</h2>
            <div className="mt-3">
              <p className="text-lg text-gray-500 dark:text-gray-400">
                Do you want to receive information about new developments or features? Sign up for our newsletter today.
              </p>
            </div>
            <div className="mt-9">
              <form className="mt-5 sm:flex sm:items-center">
                <div className="w-full sm:max-w-xs">
                  <label htmlFor="email" className="sr-only">
                    Email
                  </label>
                  <input
                    type="email"
                    name="email"
                    id="email"
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
                    className="ml-2 bg-blue-500 inline-flex items-center justify-center p-2 px-4 rounded-md text-gray-100 hover:text-gray-50 hover:bg-blue-400 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
                  >
                    <BellIcon className="block h-5 w-5" aria-hidden="true" />
                    <span className="pl-1">Notify Me</span>
                  </button>
              </form>
            </div>
          </div>

        </div>
      </div>
    </div>
  )
}