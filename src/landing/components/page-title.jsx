export default function PageTitle( { name } ) {
  return (
    <>
      <header className="bg-white dark:bg-gray-900 shadow dark:shadow-gray-800">
        <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8 flex flex-col md:flex-row gap-4 justify-between">
          <h1 className="text-3xl font-bold text-gray-900 dark:text-gray-100">{name}</h1>
        </div>
      </header>
    </>
  )
}
