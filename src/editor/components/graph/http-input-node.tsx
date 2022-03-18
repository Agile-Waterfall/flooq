import { Input } from '../form/input'
import { Select } from '../form/select'
import { TextArea } from '../form/textarea'
import { Node } from './node'

const httpMethods = [
  { value: 'get', name: 'GET' },
  { value: 'post', name: 'POST' },
  { value: 'put', name: 'PUT' },
  { value: 'patch', name: 'PATCH' },
  { value: 'delete', name: 'DELETE' },
]

interface HttpInputNodeProps {
  data: any,
  type: any
}

export const HttpInputNode = ( { data, type }: HttpInputNodeProps ): JSX.Element => {
  return (
    <Node data={data} type={type} hasInput={false}>
      <div className="p-2 flex flex-col gap-3">
        <Input
          label="Endpoint"
          value={data.input.url}
          disabled={true}
        />
        <Select
          label="HTTP Method"
          options={httpMethods}
          selected={data.input.method}
        />
        <Input
          label="Content Type"
          value={data.input.contentType}
        />
        <TextArea
          label="Sample Body"
          value={data.input.sampleBody}
          placeholder="Sample Body"
        />
      </div>
    </Node>
  )
}

