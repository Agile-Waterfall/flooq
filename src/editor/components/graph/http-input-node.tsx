import { FC } from 'react'
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
  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={data.input.url}
          disabled={true}
          onChange={console.log}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.input.method}
          onChange={console.log}
        />
        <Input
          label="Content Type"
          value={data.input.contentType}
          onChange={console.log}
        />
        <TextArea
          label="Sample Body"
          value={data.input.sampleBody}
          placeholder="Sample Body"
          onChange={console.log}
        />
      </div>
    </Node>
  )
}

