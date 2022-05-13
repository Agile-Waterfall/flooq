import { ChangeEventHandler } from 'react'

interface InputProps {
  label: string,
  value?: any,
  defaultValue?: any,
  disabled?: boolean,
  placeholder?: string,
  rest?: any[],
  onChange?: ChangeEventHandler<HTMLInputElement>
}

export const Input = ( { label, value, defaultValue, onChange, disabled = false, ...rest }: InputProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 dark:text-gray-300 text-xs">{label}</span>
    <input
      value={value}
      onChange={onChange}
      disabled={disabled}
      className="
      p-2 border rounded-sm w-full \
      bg-gray-50 dark:bg-gray-900 \
      text-gray-900 dark:text-gray-100 text-sm \
      disabled:text-gray-400 disabled:bg-gray-200 disabled:border-gray-200 \
      disabled:dark:text-gray-500 disabled:dark:bg-gray-700 disabled:dark:border-gray-700"
      defaultValue={defaultValue}
      {...rest}
    />
  </label>
)
