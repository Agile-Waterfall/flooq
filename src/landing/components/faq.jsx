const faqs = [
  {
    id: 1,
    question: 'What are Data Flows?',
    answer: 'Data Flows are an essential part of Flooq and define how two applications can communicate with one another. Each Data Flow consists at least of an input, which receives data, and an output, which sends data.',
  },
  {
    id: 10,
    question: 'Can I try out Flooq for free?',
    answer: 'Yes, with the free plan you can use Flooq for free. Check the pricing section to see what you can do with it.',
  },
  {
    id: 20,
    question: 'I want to access external APIs with a secret access token, can I store them somewhere safely?',
    answer: 'Yes, Flooq provides a secure token storage for all your access tokens. These tokens are stored encrypted and can only be read by the executor. If you want to know more about our security precautions feel free to contact us.',
  },
  {
    id: 30,
    question: 'Can I share Data Flows with my friends?',
    answer: 'No, at this moment it is not possible to share Data Flows with other users. Sign up for the newsletter if you want to get notified whenever new features are released.',
  }
]

export default function FAQ() {
  return (
    <div id="faq" className="bg-gray-50 dark:bg-gray-900">
      <div className="max-w-7xl mx-auto py-12 px-4 divide-y divide-gray-200 sm:px-6 lg:py-16 lg:px-8">
        <h2 className="text-4xl font-extrabold text-gray-900 dark:text-white">Frequently asked questions</h2>
        <div className="mt-8">
          <dl className="divide-y divide-gray-200">
            {faqs.map( ( faq ) => (
              <div key={faq.id} className="pt-6 pb-8 md:grid md:grid-cols-12 md:gap-8">
                <dt className="text-base dark:text-gray-200 font-medium text-gray-900 md:col-span-5">{faq.question}</dt>
                <dd className="mt-2 md:mt-0 md:col-span-7">
                  <p className="text-base text-gray-500 dark:text-gray-400">{faq.answer}</p>
                </dd>
              </div>
            ) )}
          </dl>
        </div>
      </div>
    </div>
  )
}
