import { CheckIcon } from '@heroicons/react/solid'

const currency = '$'

const tiers = [
  {
    name: 'Free',
    href: '#',
    priceMonthly: currency + "0",
    description: 'If you feel like using our resources for no money',
    includedFeatures: [
      '5 Data Flows',
      '10 Executions per day',
      '1s Execution time',
      '500MB memory'
    ]
  },
  {
    name: 'Hobby',
    href: '#',
    priceMonthly: currency + "4",
    description: 'For developers developing small projects',
    includedFeatures: [
      '20 Data Flows',
      '50 Executions per day',
      '10s Execution time',
      '1GB memory'
    ]
  },
  {
    name: 'Startup',
    href: '#',
    priceMonthly: currency + "32",
    description: 'All the basics for starting a new business',
    includedFeatures: [
      '100 Data Flows',
      '200 Executions per day',
      '1min Execution time',
      '16GB memory'
    ]
  },
  {
    name: 'Enterprise',
    href: '#',
    priceMonthly: "contact us",
    description: 'The best for improving a company that already exists',
    includedFeatures: [
      'unlimited Data Flows',
      'unlimited Executions per day',
      'custom Execution time',
      'custom memory',
      'organisations',
      'local deployment'
    ]
  },
]

export default function Pricing() {
  return (
    <div className="bg-white">
      <div className="max-w-7xl mx-auto py-24 px-4 sm:px-6 lg:px-8">
        <div className="sm:flex sm:flex-col sm:align-center">
          <h1 className="text-5xl font-extrabold text-gray-900 sm:text-center">Pricing Plans</h1>
          <p className="mt-5 text-xl text-gray-500 sm:text-center">
            Start building for free, then add a site plan to go live. Account plans unlock additional features.
          </p>
        </div>
        <div className="mt-12 space-y-4 sm:mt-16 sm:space-y-0 sm:grid sm:grid-cols-2 sm:gap-6 lg:max-w-4xl lg:mx-auto xl:max-w-none xl:mx-0 xl:grid-cols-4">
          {tiers.map((tier) => (
            <div key={tier.name} className="border border-gray-200 rounded-lg shadow-sm divide-y divide-gray-200">
              <div className="p-6">
                <h2 className="text-lg leading-6 font-medium text-gray-900">{tier.name}</h2>
                <p className="mt-4 text-sm text-gray-500">{tier.description}</p>
                <p className="mt-8">
                  <span className="text-4xl font-extrabold text-gray-900">{tier.priceMonthly}</span>{' '}
                  {tier.priceMonthly.startsWith(currency) ? <span className="text-base font-medium text-gray-500">/mo</span> : <></>}
                </p>
                <a
                  href={tier.href}
                  className="mt-8 block w-full bg-gray-800 border border-transparent rounded-md py-2 text-sm font-semibold bg-blue-500 hover:bg-blue-400 text-gray-100 text-center"
                >
                  Buy {tier.name}
                </a>
              </div>
              <div className="pt-6 pb-8 px-6">
                <h3 className="text-xs font-medium text-gray-900 tracking-wide uppercase">What's included</h3>
                <ul role="list" className="mt-6 space-y-4">
                  {tier.includedFeatures.map((feature) => (
                    <li key={feature} className="flex space-x-3">
                      <CheckIcon className="flex-shrink-0 h-5 w-5 text-green-500" aria-hidden="true" />
                      <span className="text-sm text-gray-500">{feature}</span>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}