import Markdown from 'markdown-to-jsx'
import { Dialog } from './dialog'

interface DocsDialogProps {
  areDocsOpen: boolean;
  setAreDocsOpen: ( open: boolean ) => void;
  content: string
}

export const DocsDialog = ( { areDocsOpen, setAreDocsOpen, content }: DocsDialogProps ): JSX.Element => (
  <Dialog
    isOpen={ areDocsOpen }
    onClose={ (): void => setAreDocsOpen( false ) }>
    <Markdown className="dark:text-gray-100 pb-5 markdown-body max-w-2xl max-h-[90vh] overflow-y-auto" options={{ forceBlock: true }}>
      {content}
    </Markdown>
  </Dialog>

)
