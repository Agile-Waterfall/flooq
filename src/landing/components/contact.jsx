import {LibraryIcon, MailIcon, PhoneIcon} from '@heroicons/react/outline'

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
                    className="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
                    placeholder="you@example.com"
                  />
                </div>
                <button 
                  type="submit"
                  className="mt-3 w-full inline-flex items-center justify-center px-4 py-2 border border-transparent shadow-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
                >
                  Save
                </button>
              </form>
            </div>
          </div>
          
        </div>
      </div>
    </div>
  )
}