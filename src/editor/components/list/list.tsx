import { Action, ListHeader } from './list-header'

interface ListProps {
  title: string,
  description: string,
  action?: Action,
  children: any
}

export const List = ( { title, description, action, children }: ListProps ): JSX.Element => (
  <div className="flex flex-col">
    <ListHeader
      title={title}
      description={description}
      action={action}
    />
    {children}
  </div>
)
