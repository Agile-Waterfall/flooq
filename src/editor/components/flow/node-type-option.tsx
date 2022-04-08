interface NodeTypeOptionProps {
  title: string,
  description: string,

  onClick(): void
}

export const NodeTypeOption = ( { title, description, onClick }: NodeTypeOptionProps ): JSX.Element => (
  <div className="rounded-lg shadow shadow-md hover:shadow-xl cursor-pointer" onClick={onClick}>
    <div className="p-3">
      <h5 className="font-bold">
        {title}
      </h5>
      <hr/>
      <p className="break-words">
        {description}
      </p>
    </div>
  </div>
)
