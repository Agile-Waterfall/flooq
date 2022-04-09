import { FC, useCallback, useEffect, useRef, useState } from 'react'
import { FlooqNode, Node } from './node'
import { useReactFlow, useUpdateNodeInternals } from 'react-flow-renderer/dist/nocss'

export const ScriptNode: FC<FlooqNode> = ( { id, data, ...rest } ): any => {
  const reactFlowHook = useReactFlow()
  const editorRef = useRef<any>()

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

    return `async (${newIncomingHandles.map( i => i.name ).join( ', ' )})${original.substring( length, original.length )}`
  }

  const updateValue = ( newValue: string ): void => {
    setValue( newValue )
    updateNode( newValue, incomingHandles )
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
      <div className="font-mono min-h-full">
        <textarea
          ref={editorRef}
          value={value}
          placeholder="Add your custom javascript code."
          onChange={( e: any ): void => updateValue( e.target.value )}
          className="
          p-2 rounded-sm w-full h-48 \
          bg-gray-100 dark:bg-gray-900 \
          text-gray-900 dark:text-gray-100 text-sm \
          disabled:text-gray-400 disabled:bg-gray-200 \
          disabled:dark:text-gray-500 disabled:dark:bg-gray-700"
        />
      </div>
    </Node>
  )
}
