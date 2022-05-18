
interface FormListProps {
  title: string,
  description: string,
  children: JSX.Element
}

export const FormList = ( { title, description, children }: FormListProps ): JSX.Element => (
  <div className="md:grid md:grid-cols-3 md:gap-6">
    <div className="md:col-span-1">
      <div className="px-4 sm:px-0">
        <h3 className="text-lg font-medium leading-6 text-gray-900 dark:text-gray-100">{title}</h3>
        <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
          {description}
        </p>
      </div>
    </div>
    <div className="mt-5 md:mt-0 md:col-span-2">
      {children}
    </div>
  </div>
)
