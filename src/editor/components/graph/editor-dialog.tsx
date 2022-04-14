import { Dialog } from '../dialog'
import { CodeEditor } from '../form/editor'

interface ScriptNodeProps {
  isOpen: boolean,
  setIsOpen( value: boolean ): void
  value: any
  setValue( value: any ): void
  language: string
}

export const EditorDialog = ( { isOpen, setIsOpen, language, value, setValue }: ScriptNodeProps ): JSX.Element => {
  return (
    <Dialog
      isOpen={isOpen}
      onClose={(): void => setIsOpen( false )}
      title="Code Editor Dialog"
    >
      <CodeEditor
        height="80vh"
        width="80vw"
        value={value}
        language={language}
        onChange={setValue}
      />
    </Dialog>
  )
}
