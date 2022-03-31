interface Option {
  value: any,
  name: string
}

interface SelectProps {
  label: string,
  options: Option[],
  selected: string,
  onChange( e: any ): void
}

export const Select = ( { label, options, selected, onChange }: SelectProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 text-xs">{label}</span>
    <select onChange={onChange} value={selected} className="p-2 border rounded-sm border-gray-200 w-full bg-gray-50 dark:bg-gray-900 text-sm">
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
