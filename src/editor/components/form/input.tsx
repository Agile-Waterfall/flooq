interface InputProps {
  label: string,
  value: any,
  disabled?: boolean,
  rest?: any[],
  onChange( e: any ): void
}

export const Input = ( { label, value, disabled = false, onChange, ...rest }: InputProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 text-xs">{label}</span>
    <input
      onChange={onChange}
      className="p-2 border rounded-sm border-gray-200 w-full bg-gray-50 dark:bg-gray-900 text-sm"
      {...rest}
    />
  </label>
)
