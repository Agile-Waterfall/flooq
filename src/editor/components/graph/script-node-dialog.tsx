import Editor from '@monaco-editor/react'
import { Dialog } from '../dialog'

interface ScriptNodeProps {
  isOpen: boolean,
  setIsOpen( value: boolean ): void
  value: any
  setValue( value: any ): void
  theme?: string
}

export const ScriptNodeDialog = ( { isOpen, setIsOpen, theme, value, setValue }: ScriptNodeProps ): JSX.Element => {
  return (
    <Dialog
      isOpen={isOpen}
      onClose={(): void => setIsOpen( false )}
      title="JavaScript Editor"
    >
      <Editor
        height="80vh"
        width="80vw"
        value={value}
        theme={theme}
        language="javascript"
        onChange={setValue}
      />
    </Dialog>
  )
}
