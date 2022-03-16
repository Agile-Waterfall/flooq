import { Input } from '../form/input'
import { Select } from '../form/select'
import { Node } from './node'

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

interface FilterNodeProps {
  data: any,
  type: any
}

export const FilterNode = ( { data, type }: FilterNodeProps ): JSX.Element => {
  return (
    <Node data={data} type={type}>
      <div className="flex flex-col gap-3">
        <Select
          label="Field Name"
          options={fieldNames}
          selected={data.filter.field}
        />
        <Select
          label="Condition"
          options={conditions}
          selected={data.filter.condition}
        />
        <Input
          label="Filter Value"
          value={data.filter.value}
        />
      </div>
    </Node>
  )
}
