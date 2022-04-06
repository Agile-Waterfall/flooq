import { FC, useCallback } from 'react'
import { useReactFlow } from 'react-flow-renderer/dist/nocss'
import { Input } from '../form/input'
import { Select } from '../form/select'
import { TextArea } from '../form/textarea'
import { FlooqNode, Node } from './node'

const httpMethods = [
  { value: 'get', name: 'GET' },
  { value: 'post', name: 'POST' },
  { value: 'put', name: 'PUT' },
  { value: 'patch', name: 'PATCH' },
  { value: 'delete', name: 'DELETE' },
]

export const HttpInputNode: FC<FlooqNode> = ( { id, data, ...rest } ) => {
  const reactFlowHook = useReactFlow()
  const updateNode = useCallback( ( input ): void => {
    reactFlowHook.setNodes( reactFlowHook.getNodes().map( n => {
      if ( n.id !== id ) return n
      return {
        ...n,
        data: {
          ...( n.data as FlooqNode ),
          input
        }
      }
    } ) )
  }, [id, reactFlowHook] )

  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={data.input.url}
          disabled={true}
          onChange={( e ): void => updateNode( { ...data.input, url: e.target.value } )}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.input.method}
          onChange={( e ): void => updateNode( { ...data.input, method: e.target.value } )}
        />
        <Input
          label="Content Type"
          value={data.input.contentType}
          onChange={( e ): void => updateNode( { ...data.input, contentType: e.target.value } )}
        />
        <TextArea
          label="Sample Body"
          value={data.input.sampleBody}
          placeholder="Sample Body"
          onChange={( e ): void => updateNode( { ...data.input, sampleBody: e.target.value } )}
        />
      </div>
    </Node>
  )
}

