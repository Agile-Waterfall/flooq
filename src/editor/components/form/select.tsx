import React from 'react'

interface SelectProps {
  label: String,
  options: any,
  selected: boolean
}

export const Select = ( { label, options, selected }: SelectProps ): JSX.Element => (
  <label className="text-left">
    <span className="text-gray-600 text-xs">{label}</span>
    <select className="p-2 border rounded-sm border-gray-200 w-full bg-gray-50 dark:bg-gray-900 text-sm">
      <option disabled>Select {label}</option>
      {options.map( ( option: any, i: number ) => (
        <option
          key={i}
          value={option.value}
          selected={option.value === selected}
        >
          {option.name}
        </option>
      ) )}
    </select>
  </label>
)
