import { FC, useCallback, useEffect, useState } from 'react'
import { FlooqNode, Node } from './node'
import { useReactFlow, useUpdateNodeInternals } from 'react-flow-renderer/dist/nocss'
import Editor from '@monaco-editor/react'
import { ArrowsExpandIcon } from '@heroicons/react/outline'
import { ScriptNodeDialog } from './script-node-dialog'

export const ScriptNode: FC<FlooqNode> = ( { id, data, ...rest } ): any => {
  const reactFlowHook = useReactFlow()

  const [theme, setTheme] = useState<string>()
  const [isEditorOpen, setIsEditorOpen] = useState( false )
  const [value, setValue] = useState( data.input.function )
  const [incomingHandles, setIncomingHandles] = useState( data.incomingHandles )
  const updateNodeInternals = useUpdateNodeInternals()

  useEffect( () => {
    updateNodeInternals( id )
  }, [incomingHandles, id, updateNodeInternals] )

  useEffect( () => {
    const colorScheme = window.matchMedia( '(prefers-color-scheme: dark)' )
    colorScheme.addEventListener( 'change', e => toggleTheme( e.matches ) )
    toggleTheme( colorScheme.matches )
    return colorScheme.removeEventListener( 'change', e => toggleTheme( e.matches ) )
  }, [] )

  const toggleTheme = ( isDarkMode: boolean ): void => {
    setTheme( isDarkMode ? 'vs-dark' : 'vs-light' )
  }

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

  const updateValue = ( newValue: string = '' ): void => {
    const regex = /^async \((.*)\)/
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
      <div className="font-mono min-h-full">
        <Editor
          height={200}
          width={300}
          value={value}
          theme={theme}
          options={{
            minimap: {
              enabled: false
            }
          }}
          language="javascript"
          onChange={( newValue?: string ): void => updateValue( newValue )}
        />

        <ScriptNodeDialog
          isOpen={isEditorOpen}
          setIsOpen={setIsEditorOpen}
          value={value}
          theme={theme}
          setValue={updateValue}
        />

        <div className="p-1" onClick={(): void => setIsEditorOpen( true )}>
          <ArrowsExpandIcon className="w-4 h-4" />
        </div>
      </div>
    </Node>
  )
}
