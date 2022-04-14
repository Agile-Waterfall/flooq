import { ArrowsExpandIcon } from '@heroicons/react/outline'
import { useState } from 'react'
import { EditorDialog } from '../graph/editor-dialog'
import { CodeEditor } from './editor'

interface CodeProps {
  label: string,
  value: any,
  rest?: any[],
  onChange( e: any ): void
}

export const Code = ( { label, value, onChange }: CodeProps ): JSX.Element => {
  const [isEditorOpen, setIsEditorOpen] = useState( false )

  return (
    <label className="text-left">
      <span className="text-gray-600 dark:text-gray-300 text-xs">{label}</span>
      <div className="p-1 border rounded-sm bg-gray-50 dark:bg-gray-900">
        <CodeEditor
          height="100px"
          value={value}
          onChange={onChange}
          language="json"
          options={{
            lineNumbers: 'off',
            minimap: {
              enabled: false
            }
          }}
        />
        <div className="p-1 flex gap-1 justify-start items-center">
          <ArrowsExpandIcon className="w-4 h-4" onClick={(): void => setIsEditorOpen( true )} />
        </div>
      </div>

      <EditorDialog
        isOpen={isEditorOpen}
        setIsOpen={setIsEditorOpen}
        value={value}
        setValue={onChange}
        language="json"
      />
    </label>
  )
}
