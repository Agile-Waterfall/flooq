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
  const updateNode = useCallback( ( params ): void => {
    reactFlowHook.setNodes( reactFlowHook.getNodes().map( n => {
      if ( n.id !== id ) return n
      return {
        ...n,
        data: {
          ...( n.data as FlooqNode ),
          params
        }
      }
    } ) )
  }, [id, reactFlowHook] )

  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={data.params.url}
          disabled={true}
          onChange={( e ): void => updateNode( { ...data.params, url: e.target.value } )}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.params.method}
          onChange={( e ): void => updateNode( { ...data.params, method: e.target.value } )}
        />
        <Input
          label="Content Type"
          value={data.params.contentType}
          onChange={( e ): void => updateNode( { ...data.params, contentType: e.target.value } )}
        />
        <TextArea
          label="Sample Body"
          value={data.params.sampleBody}
          placeholder="Sample Body"
          onChange={( e ): void => updateNode( { ...data.params, sampleBody: e.target.value } )}
        />
      </div>
    </Node>
  )
}

