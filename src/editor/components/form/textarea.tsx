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
      value={value}
      disabled={disabled}
      placeholder={placeholder}
      onChange={onChange}
      {...rest}
      className="
      p-2 border rounded-sm w-full \
      bg-gray-50 dark:bg-gray-900 \
      text-gray-900 dark:text-gray-100 text-sm \
      disabled:text-gray-400 disabled:bg-gray-200 disabled:border-gray-200 \
      disabled:dark:text-gray-500 disabled:dark:bg-gray-700 disabled:dark:border-gray-700"
    />
  </label>
)
