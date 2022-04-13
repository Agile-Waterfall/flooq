interface Option {
  value: any,
  name: string
}

interface SelectProps {
  label: string,
  options: Option[],
  selected: string,
  disabled?: boolean
  onChange( e: any ): void
}

export const Select = ( { label, options, selected, disabled, onChange }: SelectProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 dark:text-gray-300 text-xs">{label}</span>
    <select
      onChange={onChange}
      value={selected}
      disabled={disabled}
      className="
      p-2 border rounded-sm w-full \
      bg-gray-50 dark:bg-gray-900 \
      text-gray-900 dark:text-gray-100 text-sm \
      disabled:text-gray-400 disabled:bg-gray-200 disabled:border-gray-200 \
      disabled:dark:text-gray-500 disabled:dark:bg-gray-700 disabled:dark:border-gray-700"
    >
      <option disabled>Select {label}</option>
      {options.map( ( option: any, i: number ) => (
        <option
          key={i}
          value={option.value}
        >
          {option.name}
        </option>
      ) )}
    </select>
  </label>
)
