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

export const HttpOutputNode: FC<FlooqNode> = ( { id, data, ...rest } ) => {
  return (
    <Node id={id} data={data} {...rest}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={data.output.url}
          disabled={true}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.output.method}
        />
        <Input
          label="Content Type"
          value={data.output.contentType}
        />
        <TextArea
          label="Request Header"
          value={data.output.header}
          placeholder="Request Header"
        />
        <TextArea
          label="Request Body"
          value={data.output.body}
          placeholder="Request Body"
        />
      </div>
    </Node>
  )
}

