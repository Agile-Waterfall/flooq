import { MessageContainer, Message } from './message'

interface PageTitleProps {
  name: string,
  message?: Message,
  children?: any
}

export const PageTitle = ( { name, children, message }: PageTitleProps ): JSX.Element => (
  <header className="bg-white dark:bg-gray-900 shadow dark:shadow-gray-800">
    <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8 flex justify-between">
      <h1 className="text-3xl font-bold text-gray-900 dark:text-gray-100">{name}</h1>
      {children}
    </div>
    {message &&
      <MessageContainer {...message}></MessageContainer>
    }
  </header>
)
