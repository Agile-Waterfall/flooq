import { Dialog } from './dialog'
import styles from '../styles/markdown.module.scss'

interface DocsDialogProps {
  areDocsOpen: boolean;
  setAreDocsOpen: ( open: boolean ) => void;
  children: JSX.Element
}

export const DocsDialog = ( { areDocsOpen, setAreDocsOpen, children }: DocsDialogProps ): JSX.Element => (
  <Dialog
    title="Node Documentation"
    isOpen={ areDocsOpen }
    onClose={ (): void => setAreDocsOpen( false ) }>
    <div className={styles.markdown}>
      {children}
    </div>
  </Dialog>

)
