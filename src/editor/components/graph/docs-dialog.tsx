import { useEffect, useState } from 'react'
import { Dialog } from '../dialog'
import { Markdown } from '../markdown'

interface DocsDialogProps {
  isDocsDialogOpen: boolean;
  setIsDocsDialogOpen: ( open: boolean ) => void;
  filePath: string
}

export const DocsDialog = ( { isDocsDialogOpen, setIsDocsDialogOpen, filePath }: DocsDialogProps ): JSX.Element => {
  const [content, setContent] = useState<string>( '*Loading…*' )

  const loadDocument = async ( path: string ): Promise<void> => {
    const res = await fetch( `/api/flows/docs?${new URLSearchParams( { path } )}` )
    if ( res.ok ) {
      const text = await res.text()
      setContent( text )
    } else {
      setContent( `*Could not load documentation*.<br><br>Error message:<br>\`\`\`${res.statusText}\`\`\`` )
    }
  }

  useEffect( () => {
    loadDocument( filePath )
  }, [filePath] )

  return (
    <Dialog
      title="Node Documentation"
      isOpen={isDocsDialogOpen}
      onClose={(): void => setIsDocsDialogOpen( false )}>
      <Markdown>
        {content}
      </Markdown>
    </Dialog>
  )
}
