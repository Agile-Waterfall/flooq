import { FC, useCallback } from 'react'
import { useReactFlow } from 'react-flow-renderer/dist/nocss'
import { Input } from '../form/input'
import { Select } from '../form/select'
import { FlooqNode, Node } from './node'

const conditions = [
  { value: 'eq', name: 'Equals (==)' },
  { value: 'ne', name: 'Not equals (!=)' },
  { value: 'gt', name: 'Greater (>)' },
  { value: 'ge', name: 'Greater or equal (>=)' },
  { value: 'lt', name: 'Less (<)' },
  { value: 'le', name: 'Less or equal (<=)' },
  { value: 'nn', name: 'Not empty or null' },
  { value: 're', name: 'Match Regex' }
]

const fieldNames = [
  { value: 'id', name: 'Id' },
  { value: 'title', name: 'Title' },
  { value: 'tags', name: 'Tags' },
]

export const FilterNode: FC<FlooqNode> = ( { id, data, ...rest } ) => {
  const reactFlowHook = useReactFlow()
  const updateNode = useCallback( ( filter ): void => {
    reactFlowHook.setNodes( reactFlowHook.getNodes().map( n => {
      if ( n.id !== id ) return n
      return {
        ...n,
        data: {
          ...( n.data as FlooqNode ),
          filter
        }
      }
    } ) )
  }, [id, reactFlowHook] )

  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Select
          label="Field Name"
          options={fieldNames}
          selected={data.filter.field}
          onChange={( e ): void => updateNode( { ...data.filter, field: e.target.value } )}
        />
        <Select
          label="Condition"
          options={conditions}
          selected={data.filter.condition}
          onChange={( e ): void => updateNode( { ...data.filter, condition: e.target.value } )}
        />
        <Input
          label="Filter Value"
          value={data.filter.value}
          onChange={( e ): void => updateNode( { ...data.filter, value: e.target.value } )}
        />
      </div>
    </Node>
  )
}
