import { FC, useCallback, useEffect, useState } from 'react'
import dynamic from 'next/dynamic'
import '@uiw/react-textarea-code-editor/dist.css'
import { FlooqNode, Node } from './node'
import { useReactFlow, useUpdateNodeInternals } from 'react-flow-renderer/dist/nocss'

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
  const reactFlowHook = useReactFlow()

  const [value, setValue] = useState( data.input.function )
  const [incomingHandles, setIncomingHandles] = useState( data.incomingHandles )
  const updateNodeInternals = useUpdateNodeInternals()

  useEffect( () => {
    updateNodeInternals( id )
  }, [incomingHandles, id, updateNodeInternals] )

  const addNewHandle = async (): Promise<void> => {
    const newId = incomingHandles.length + 1
    const newIncomingHandles = [
      ...incomingHandles,
      { id: newId, name: String.fromCharCode( 96 + newId ) }
    ]

    await setIncomingHandles( newIncomingHandles )
    const newFunctionString = updateFunctionHeader( newIncomingHandles )
    setValue( newFunctionString )
    updateNode( newFunctionString, newIncomingHandles )
  }

  const updateNode = useCallback( ( functionValue, newIncomingHandles ): void => {
    reactFlowHook.setNodes( reactFlowHook.getNodes().map( n => {
      if ( n.id !== id ) return n
      return {
        ...n,
        data: {
          ...( n.data as FlooqNode ),
          input: { function: functionValue },
          incomingHandles: newIncomingHandles
        }
      }
    } ) )
  }, [id, reactFlowHook] )

  const updateFunctionHeader = ( newIncomingHandles: any[] ): string => {
    const original = data.input.function
    const regex = /^async \((.*)\)/
    const match = regex.exec( original )
    const length = match !== null ? match[0].length : 0

    return `async (${newIncomingHandles.map( i => i.name ).join( ', ' )}) ${original.substring( length, original.length )}`
  }

  const updateValue = ( value: string ): void => {
    updateNode( value, incomingHandles )
  }


  return (
    <Node
      id={id}
      data={{
        ...data,
        incomingHandles: incomingHandles,
        canAddTargetHandle: true,
        onAddedTargetHandle: addNewHandle
      }}
      {...rest}
    >
      <div className="font-mono min-h-48">
        <CodeEditor
          value={value}
          language="js"
          placeholder="Add your custom javascript code."
          onChange={( e: any ): void => updateValue( e.target.value )}
        />
      </div>
    </Node>
  )
}
