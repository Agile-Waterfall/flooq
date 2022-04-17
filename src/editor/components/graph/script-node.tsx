import { FC, useCallback, useEffect, useState } from 'react'
import { FlooqNode, Node } from './node'
import { useReactFlow, useUpdateNodeInternals } from 'react-flow-renderer/dist/nocss'
import { ArrowsExpandIcon, HashtagIcon } from '@heroicons/react/outline'
import { EditorDialog } from './editor-dialog'
import { CodeEditor } from '../form/editor'

const FUNCTION_HEADER_REGEX = /const handler = \((.*)\)/

export const ScriptNode: FC<FlooqNode> = ( { id, data, ...rest } ): any => {
  const reactFlowHook = useReactFlow()

  const [isEditorOpen, setIsEditorOpen] = useState( false )
  const [showLineNumbers, setShowLineNumbers] = useState( false )
  const [isValid, setIsValid] = useState( true )
  const [value, setValue] = useState( data.params.function )
  const [incomingHandles, setIncomingHandles] = useState( data.incomingHandles )
  const updateNodeInternals = useUpdateNodeInternals()

  useEffect( () => {
    updateNodeInternals( id )
  }, [incomingHandles, id, updateNodeInternals] )

  useEffect( () => {
    const regex = FUNCTION_HEADER_REGEX
    const match = regex.exec( value )

    setIsValid( match !== null )

  }, [value, setIsValid] )

  const addNewHandle = async (): Promise<void> => {
    const newId = incomingHandles.length + 1
    const newName = String.fromCharCode( 96 + newId )
    const newIncomingHandles = [
      ...incomingHandles,
      { id: newName, name: newName }
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
          params: { function: functionValue },
          incomingHandles: newIncomingHandles
        }
      }
    } ) )
  }, [id, reactFlowHook] )

  const updateFunctionHeader = ( newIncomingHandles: any[] ): string => {
    const original = data.params.function
    const regex = FUNCTION_HEADER_REGEX
    const match = regex.exec( original )
    const length = match !== null ? match[0].length : 0
    const matchIndex = match !== null ? match.index : 0

    return `${original.substring( 0, matchIndex )}const handler = (${newIncomingHandles.map( i => i.name ).join( ', ' )})${original.substring( matchIndex + length, original.length )}`
  }

  const updateValue = ( newValue: string = '' ): void => {
    const regex = FUNCTION_HEADER_REGEX
    const match = regex.exec( newValue )

    let newIncomingHandles = incomingHandles
    if( match !== null && match[1] ) {
      const variables = match[1].replaceAll( ' ', '' ).split( ',' )

      newIncomingHandles = variables.filter( h => h !== '' ).map( h => ( { id: h, name: h } ) )
      setIncomingHandles( newIncomingHandles )
    }

    setValue( newValue )
    updateNode( newValue, newIncomingHandles )
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
      {!isValid &&
      <div className="p-1 bg-red-500 text-gray-50 text-left">
        Script node requires a <code className="font-bold">handler</code> function.
      </div>
      }
      <div className="font-mono min-h-full">
        <CodeEditor
          height={200}
          width={300}
          value={value}
          options={{
            lineNumbers: showLineNumbers ? 'on' : 'off',
            minimap: {
              enabled: false
            }
          }}
          language="javascript"
          onChange={updateValue}
        />

        <EditorDialog
          isOpen={isEditorOpen}
          setIsOpen={setIsEditorOpen}
          value={value}
          setValue={updateValue}
          language="javascript"
        />

        <div className="p-1 flex gap-1 justify-start items-center">
          <div className={`w-5 h-5 flex justify-center items-center rounded-full ${showLineNumbers ? 'bg-gray-500 text-gray-50' : ''}`}>
            <HashtagIcon  className="w-4 h-4" onClick={(): void => setShowLineNumbers( !showLineNumbers )} />
          </div>
          <ArrowsExpandIcon className="w-4 h-4" onClick={(): void => setIsEditorOpen( true )}/>
        </div>
      </div>
    </Node>
  )
}
