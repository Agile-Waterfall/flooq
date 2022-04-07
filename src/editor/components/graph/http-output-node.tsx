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

export const HttpOutputNode: FC<FlooqNode> = ( { id, data, ...rest } ) => {
  const reactFlowHook = useReactFlow()
  const updateNode = useCallback( ( output ): void => {
    reactFlowHook.setNodes( reactFlowHook.getNodes().map( n => {
      if ( n.id !== id ) return n
      return {
        ...n,
        data: {
          ...( n.data as FlooqNode ),
          output
        }
      }
    } ) )
  }, [id, reactFlowHook] )

  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={data.output.url}
          onChange={( e ): void => updateNode( { ...data.output, url: e.target.value } )}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.output.method}
          onChange={( e ): void => updateNode( { ...data.output, method: e.target.value } )}
        />
        <Input
          label="Content Type"
          value={data.output.contentType}
          onChange={( e ): void => updateNode( { ...data.output, contentType: e.target.value } )}
        />
        <TextArea
          label="Request Header"
          value={data.output.header}
          placeholder="Request Header"
          onChange={( e ): void => updateNode( { ...data.output, header: e.target.value } )}
        />
        <TextArea
          label="Request Body"
          value={data.output.body}
          placeholder="Request Body"
          onChange={( e ): void => updateNode( { ...data.output, body: e.target.value } )}
        />
      </div>
    </Node>
  )
}

