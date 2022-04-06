interface TextAreaProps {
  label: string,
  value: any,
  placeholder: string,
  disabled?: boolean,
  rest?: any[],
  onChange( e: any ): void
}

export const TextArea = ( { label, value, placeholder, disabled, onChange, ...rest }: TextAreaProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 dark:text-gray-300 text-xs">{label}</span>
    <textarea
      className="p-2 border rounded-sm border-gray-200 w-full bg-gray-50 dark:bg-gray-900 disabled:text-gray-400 text-gray-900 dark:text-gray-100 text-sm"
      value={value}
      disabled={disabled}
      placeholder={placeholder}
      onChange={onChange}
      {...rest}
    />
  </label>
)
