import { FC, useCallback } from 'react'
import { useReactFlow } from 'react-flow-renderer/dist/nocss'
import { updateNodeParameter } from '../../helper/nodes'
// import { Code } from '../form/code'
import { Input } from '../form/input'
import { Select } from '../form/select'
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
    reactFlowHook.setNodes( reactFlowHook.getNodes().map( n => updateNodeParameter( n, id, params ) ) )
  }, [id, reactFlowHook] )

  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={`${process.env.NEXT_PUBLIC_EXECUTOR_BASE_URL}/flow/${data.dataFlowId}`}
          disabled={true}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.params.method}
          onChange={( e ): void => updateNode( { ...data.params, method: e.target.value } )}
        />
        {/* <Code
          label="Sample Body"
          value={data.params.sampleBody}
          onChange={( value ): void => updateNode( { ...data.params, sampleBody: value } )}
        /> */}
      </div>
    </Node>
  )
}
