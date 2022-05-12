import { CheckIcon } from '@heroicons/react/solid'

const currency = '$'

const tiers = [
  {
    name: 'Free',
    href: process.env.NEXT_PUBLIC_EDITOR_URL,
    action: 'Sign Up',
    priceMonthly: currency + '0',
    description: 'Get a feeling for our tool by creating simple Data Flows',
    includedFeatures: [
      '1 user',
      '5 Data Flows',
      '50 Executions per day',
      '1s Execution time',
    ]
  },
  {
    name: 'Personal',
    href: '#',
    action: 'Buy Personal',
    priceMonthly: currency + '3',
    disabled: true,
    description: 'For developers building small solutions',
    includedFeatures: [
      '1 user',
      '20 Data Flows',
      '1000 Executions per day',
      '2s Execution time',
    ]
  },
  {
    name: 'Startup',
    href: '#',
    action: 'Buy Startup',
    priceMonthly: currency + '20',
    disabled: true,
    description: 'Use Flooq for bigger and more active data transformations',
    includedFeatures: [
      '10 users',
      'unlimited Data Flows',
      '10\'000 Executions per day',
      '10s Execution time',
      'organisations',
      'partial local deployment'
    ]
  },
  {
    name: 'Enterprise',
    href: 'mailto:info@flooq.ioe',
    action: 'Get Enterprise',
    priceInformation: 'Contact Us',
    description: 'The all in one solution for your business needs.',
    includedFeatures: [
      'unlimited user',
      'unlimited Data Flows',
      'unlimited Executions',
      'custom Execution time',
      'organisations',
      'full local deployment',
      'custom integrations',
      'priority support',
    ]
  },
]

export default function Pricing() {
  return (
    <div id="pricing" className="bg-white dark:bg-gray-800">
      <div className="max-w-7xl mx-auto py-24 px-4 sm:px-6 lg:px-8">
        <div className="sm:flex sm:flex-col sm:align-center">
          <h1 className="text-4xl font-extrabold text-gray-900 dark:text-white sm:text-center">Pricing Plans</h1>
          <p className="mt-5 text-xl text-gray-500 dark:text-gray-100 sm:text-center max-w-2xl mx-auto">
            We offer a variety of different Pricing Plans available for you. From a free tier to get to know the tool, up to an enterprise grade solution to fit your custom needs.
          </p>
        </div>
        <div className="mt-12 space-y-4 sm:mt-16 sm:space-y-0 sm:grid sm:grid-cols-2 sm:gap-6 lg:max-w-4xl lg:mx-auto xl:max-w-none xl:mx-0 xl:grid-cols-4">
          {tiers.map( ( tier ) => (
            <div key={tier.name} className="border border-gray-200 rounded-lg shadow-sm divide-y divide-gray-200 dark:bg-gray-900">
              <div className="p-6">
                <h2 className="text-lg leading-6 font-medium text-gray-900 dark:text-white flex justify-between">
                  {tier.name}
                  {tier.disabled &&
                    <span className="flex-shrink-0 flex items-center px-2 py-0.5 text-orange-800 dark:text-orange-300 text-xs font-medium bg-orange-100 dark:bg-orange-900 rounded-full">
                      Coming Soon
                    </span>
                  }
                  {tier.specialOffer &&
                    <span className="flex-shrink-0 flex items-center px-2 py-0.5 text-green-800 dark:text-green-300 text-xs font-medium bg-green-100 dark:bg-green-900 rounded-full">
                      {tier.specialOffer}
                    </span>
                  }
                </h2>
                <p className="mt-4 text-sm text-gray-500 dark:text-gray-400">{tier.description}</p>
                {tier.priceMonthly &&
                  <p className="mt-8">
                    <span className="text-4xl font-extrabold text-gray-900 dark:text-white">{tier.priceMonthly}</span>{' '}
                    <span className="text-base font-medium text-gray-500 dark:text-gray-200">/mo</span>
                  </p>
                }
                {tier.priceInformation &&
                  <p className="mt-8">
                    <span className="text-4xl font-extrabold text-gray-900 dark:text-white">{tier.priceInformation}</span>
                  </p>
                }
                {!tier.disabled &&
                  <a
                    href={tier.href}
                    className="mt-8 block w-full bg-gray-800 border border-transparent rounded-md py-2 text-sm font-semibold bg-blue-500 hover:bg-blue-400 text-gray-100 text-center"
                  >
                    {tier.action}
                  </a>
                }
                {tier.disabled &&
                  <a
                    className="mt-8 block w-full bg-gray-300 text-gray-100 dark:bg-gray-700 dark:text-gray-900 border border-transparent rounded-md py-2 text-sm font-semibold text-center"
                  >
                    {tier.action}
                  </a>
                }
              </div>
              <div className="pt-6 pb-8 px-6">
                <h3 className="text-xs font-medium text-gray-900 dark:text-gray-200 tracking-wide uppercase">What&apos;s included</h3>
                <ul role="list" className="mt-6 space-y-4">
                  {tier.includedFeatures.map( ( feature ) => (
                    <li key={feature} className="flex space-x-3">
                      <CheckIcon className="flex-shrink-0 h-5 w-5 text-green-500" aria-hidden="true" />
                      <span className="text-sm text-gray-500 dark:text-gray-400">{feature}</span>
                    </li>
                  ) )}
                </ul>
              </div>
            </div>
          ) )}
        </div>
      </div>
    </div>
  )
}
