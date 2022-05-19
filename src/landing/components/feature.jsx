import {
  CodeIcon,
  DesktopComputerIcon,
  CurrencyDollarIcon,
  CloudIcon,
  DatabaseIcon,
  OfficeBuildingIcon,
} from '@heroicons/react/outline'

const features = [
  {
    name: 'Visual Editor',
    description: 'Edit your Data Flows in an easy to use visual editor. Create new nodes to add new functionality to your Data Flow.',
    icon: DesktopComputerIcon,
  },
  {
    name: 'API Integration',
    description: 'Connect your data flows to services without manually configuring request contents to satisfy the API format.',
    icon: CloudIcon,
  },
  {
    name: 'Custom Scripts',
    description: 'Create a Script Node to unlock unlimited possibilities using JavaScript inside your Data Flows.',
    icon: CodeIcon,
  },
  {
    name: 'Token Storage',
    description: 'Securely store your API access tokens and use them in any of your data flows. Delete your tokens any time.',
    icon: DatabaseIcon,
  },
  {
    name: 'Organisations',
    description: 'Collaborate with your colleagues when creating and maintaining data flows. Only available with certain plans.',
    icon: OfficeBuildingIcon,
  },
  {
    name: 'Pay what you need',
    description: 'Start using Flooq for free. Upgrade to one of our custom plans once your requirements increase.',
    icon: CurrencyDollarIcon,
  },
]

export default function Features() {
  return (
    <div id="features" className="relative bg-white dark:bg-gray-800 py-16 sm:py-24 lg:py-32">
      <div className="mx-auto max-w-md px-4 text-center sm:max-w-3xl sm:px-6 lg:max-w-7xl lg:px-8">
        <p className="mt-2 text-3xl font-extrabold tracking-tight text-gray-900 dark:text-white sm:text-4xl">
          What our service offers
        </p>
        <p className="mx-auto mt-5 max-w-prose text-xl text-gray-500 dark:text-gray-200">
          Many web services provide automation APIs using webhooks. Use Flooq to connect incompatible endpoints and create complex automations!
        </p>
        <div className="mt-12">
          <div className="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3">
            {features.map( ( feature ) => (
              <div key={feature.name} className="pt-6">
                <div className="flow-root rounded-lg bg-gray-50 dark:bg-gray-900 px-6 pb-8">
                  <div className="-mt-6">
                    <div>
                      <span className="inline-flex items-center justify-center rounded-md bg-amber-400 p-3 shadow-lg">
                        <feature.icon className="h-6 w-6 text-white dark:text-black" aria-hidden="true" />
                      </span>
                    </div>
                    <h3 className="mt-8 text-lg font-medium tracking-tight text-gray-900 dark:text-white">{feature.name}</h3>
                    <p className="mt-5 text-base text-gray-500 dark:text-gray-200">{feature.description}</p>
                  </div>
                </div>
              </div>
            ) )}
          </div>
        </div>
      </div>
    </div>
  )
}
