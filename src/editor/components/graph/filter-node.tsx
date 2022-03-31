import { FC } from 'react'
import { Input } from '../form/input'
import { Select } from '../form/select'
import { FlooqNode, Node } from './node'

const conditions = [
  { value: 'eq', name: 'Equals (==)' },
  { value: 'ne', name: 'Not equals (!=)' },
  { value: 'g', name: 'Greater (>)' },
  { value: 'ge', name: 'Greater or equal (>=)' },
  { value: 's', name: 'Smaller (<)' },
  { value: 'se', name: 'Smaller or equal (<=)' },
  { value: 'nn', name: 'Not empty or null' },
  { value: 'mr', name: 'Match Regex' }
]

const fieldNames = [
  { value: 'id', name: 'Id' },
  { value: 'title', name: 'Title' },
  { value: 'tags', name: 'Tags' },
]

export const FilterNode: FC<FlooqNode> = ( { id, data, ...rest } ) => {
  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Select
          label="Field Name"
          options={fieldNames}
          selected={data.filter.field}
          onChange={console.log}
        />
        <Select
          label="Condition"
          options={conditions}
          selected={data.filter.condition}
          onChange={console.log}
        />
        <Input
          label="Filter Value"
          value={data.filter.value}
          onChange={console.log}
        />
      </div>
    </Node>
  )
}
