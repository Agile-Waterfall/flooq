interface NodeTypeOptionProps {
  title: string,
  description: string,
  onClick(): void
}

export const NodeTypeOption = ( { title, description, onClick }: NodeTypeOptionProps ): JSX.Element => (
  <div className="rounded-md shadow hover:shadow-sm border cursor-pointer" onClick={onClick}>
    <div className="p-3">
      <h4 className="font-bold">{title}</h4>
      <p className="break-words py-2 text-xs">
        {description}
      </p>
    </div>
  </div>
)
