import {
  CodeIcon,
  DesktopComputerIcon,
  LinkIcon,
  SearchIcon,
  ServerIcon,
  TrendingUpIcon,
} from '@heroicons/react/outline'

const features = [
  {
    name: 'Visual Editor',
    description: 'Edit your Data Flows in a easy to use visual editor. Create new nodes to add new functionality to your Data Flow.',
    icon: DesktopComputerIcon,
  },
  {
    name: 'Webhooks',
    description: 'Use the Data Flow url as your webhook. This allows you to access and use data provided in the webhook call.',
    icon: LinkIcon,
  },
  {
    name: 'Filter Data',
    description: 'Use the Filter Node to easily filter data. No more unnecessary large data structures for you!',
    icon: SearchIcon,
  },
  {
    name: 'Custom Scripts',
    description: 'Create a new Script Node to unlock the unlimited possibilities of java script in your Data Flow.',
    icon: CodeIcon,
  },
  {
    name: 'Scaling',
    description: 'With our robust backend infrastructure, we can ensure, that yor Data Flows will be handled as advertised.',
    icon: TrendingUpIcon,
  },
  {
    name: 'Token Storage',
    description: 'We securely store your API Tokens. Saved Tokens can be used for different Data Flows and deleted at any time.',
    icon: ServerIcon,
  },
]

export default function Feature() {
  return (
    <div className="relative bg-white py-16 sm:py-24 lg:py-32">
      <div className="mx-auto max-w-md px-4 text-center sm:max-w-3xl sm:px-6 lg:max-w-7xl lg:px-8">
        <h2 className="text-base font-semibold uppercase tracking-wider text-amber-400">Reach new limits</h2>
        <p className="mt-2 text-3xl font-extrabold tracking-tight text-gray-900 sm:text-4xl">
          Everything our service offers
        </p>
        <p className="mx-auto mt-5 max-w-prose text-xl text-gray-500">
          Phasellus lorem quam molestie id quisque diam aenean nulla in. Accumsan in quis quis nunc, ullamcorper
          malesuada. Eleifend condimentum id viverra nulla.
        </p>
        <div className="mt-12">
          <div className="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3">
            {features.map((feature) => (
              <div key={feature.name} className="pt-6">
                <div className="flow-root rounded-lg bg-gray-50 px-6 pb-8">
                  <div className="-mt-6">
                    <div>
                      <span className="inline-flex items-center justify-center rounded-md bg-amber-400 p-3 shadow-lg">
                        <feature.icon className="h-6 w-6 text-white" aria-hidden="true" />
                      </span>
                    </div>
                    <h3 className="mt-8 text-lg font-medium tracking-tight text-gray-900">{feature.name}</h3>
                    <p className="mt-5 text-base text-gray-500">{feature.description}</p>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}