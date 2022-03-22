interface ListProps {
  children: any
}

export const List = ( { children }: ListProps ): JSX.Element => (
  <div className="flex flex-col">
    {children}
  </div>
)
