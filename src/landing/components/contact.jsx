import { MailIcon } from '@heroicons/react/outline'
import Signup from "./signup";

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
          <Signup/>
        </div>
      </div>
    </div>
  )
}