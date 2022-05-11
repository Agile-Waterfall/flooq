export default function CTA() {
  return (
    <div className="bg-gray-50">
      <div className="max-w-7xl mx-auto py-16 px-4 sm:px-6 lg:px-8">
        <div className="bg-white rounded-lg shadow-xl overflow-hidden lg:grid lg:grid-cols-2 lg:gap-4">
          <div className="pt-10 pb-12 px-6 sm:pt-16 sm:px-16 lg:py-16 lg:pr-0 xl:py-20 xl:px-20">
            <div className="lg:self-center">
              <h2 className="text-3xl font-extrabold text-gray-800 sm:text-4xl">
                <span className="block">Ready to create your first Data Flow?</span>
              </h2>
              <p className="mt-4 mb-4 text-lg leading-6 text-gray-500">
                Get started in minutes by signing up for free and create a first Data Flow.
              </p>
              <a
                href={process.env.NEXT_PUBLIC_EDITOR_URL}
                className="inline-flex items-center justify-center px-5 py-3 border border-transparent text-base font-medium rounded-md bg-blue-500 hover:bg-blue-400 text-gray-100"
              >
                Sign up for free
              </a>
            </div>
          </div>
          <div className="-mt-6 aspect-w-5 aspect-h-3 md:aspect-w-2 md:aspect-h-1">
            <img
              className="transform translate-x-6 translate-y-6 rounded-md object-cover object-left-top sm:translate-x-16 lg:translate-y-20"
              src="/assets/img/editor-flooq.png"
              alt="App screenshot"
            />
          </div>
        </div>
      </div>
    </div>
  )
}