import {LibraryIcon, MailIcon, PhoneIcon} from '@heroicons/react/outline'

export default function Contact() {
  return (
    <div id="contact" className="bg-white">
      <div className="max-w-7xl mx-auto py-16 px-4 sm:px-6 lg:px-8">
        <div className="max-w-lg mx-auto md:max-w-none md:grid md:grid-cols-2 md:gap-8">
          <div>
            <h2 className="text-2xl font-extrabold text-gray-900 sm:text-3xl">Where we are</h2>
            <div className="mt-3">
              <p className="text-lg text-gray-500">
                We are located in the beautiful swiss city of Winterthur. Although or team consists of people from many different places. 
              </p>
            </div>
            <div className="mt-9">
              <div className="flex">
                <div className="flex-shrink-0">
                  <LibraryIcon className="h-6 w-6 text-gray-400" aria-hidden="true" />
                </div>
                <div className="ml-3 text-base text-gray-500">
                  <p>Agile Waterfall inc.</p>
                  <p className="mt-1">Gertrudstrasse 15</p>
                  <p className="mt-1">8400 Winterthur</p>
                </div>
              </div>
            </div>
          </div>
          <div className="mt-12 sm:mt-16 md:mt-0">
            <h2 className="text-2xl font-extrabold text-gray-900 sm:text-3xl">Contact us</h2>
            <div className="mt-3">
              <p className="text-lg text-gray-500">
                Do you have a question or any issue with our services? just send us a quick e-mail.
              </p>
            </div>
            <div className="mt-9">
              <div className="mt-6 flex">
                <div className="flex-shrink-0">
                  <MailIcon className="h-6 w-6 text-gray-400" aria-hidden="true" />
                </div>
                <div className="ml-3 text-base text-gray-500">
                  <a href="mailto:info@flooq.ioe">info@flooq.io</a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}