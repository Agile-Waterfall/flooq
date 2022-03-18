interface TextAreaProps {
  label: string,
  value: any,
  rest?: any[]
}

export const TextArea = ( { label, value, ...rest }: TextAreaProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 text-xs">{label}</span>
    <textarea
      className="p-2 border rounded-sm border-gray-200 w-full bg-gray-50 dark:bg-gray-900 text-sm"
      {...rest}
    >
      {value}
    </textarea>
  </label>
)
