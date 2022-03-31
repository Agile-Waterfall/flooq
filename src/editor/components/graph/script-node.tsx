import { FC, useState } from 'react'
import dynamic from 'next/dynamic'
import '@uiw/react-textarea-code-editor/dist.css'
import { FlooqNode, Node } from './node'

interface CodeEditorProps {
  value: string
  language: string
  placeholder?: string
  onChange( e: any ): void
}
const CodeEditor = dynamic<CodeEditorProps>(
  (): any => import( '@uiw/react-textarea-code-editor' ).then( ( mod ) => mod.default ),
  { ssr: false }
)

export const ScriptNode: FC<FlooqNode> = ( { id, data, ...rest } ): any => {

  const [value, setValue] = useState( data.input.function )

  return (
    <Node id={id} data={data} {...rest}>
      <div className="font-mono min-h-48">
        <CodeEditor
          value={value}
          language="js"
          placeholder="Add your custom javascript code."
          onChange={( e: any ): void => setValue( e.target.value )}
        />
      </div>
    </Node>
  )
}
