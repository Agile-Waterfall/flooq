interface TextAreaProps {
  label: string,
  value: any,
  placeholder: string,
  rest?: any[],
  onChange( e: any ): void
}

export const TextArea = ( { label, value, placeholder, onChange, ...rest }: TextAreaProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 text-xs">{label}</span>
    <textarea
      className="p-2 border rounded-sm border-gray-200 w-full bg-gray-50 dark:bg-gray-900 text-sm"
      value={value}
      placeholder={placeholder}
      onChange={onChange}
      {...rest}
    />
  </label>
)
