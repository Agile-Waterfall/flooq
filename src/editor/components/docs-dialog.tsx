import { useEffect, useState } from 'react'
import { Dialog } from './dialog'
import { Markdown } from './markdown'

interface DocsDialogProps {
  areDocsOpen: boolean;
  setAreDocsOpen: ( open: boolean ) => void;
  path: string
}

export const DocsDialog = ( { areDocsOpen, setAreDocsOpen, path }: DocsDialogProps ): JSX.Element => {
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
    loadDocument( path )
  }, [path] )

  return (
    <Dialog
      title="Node Documentation"
      isOpen={areDocsOpen}
      onClose={(): void => setAreDocsOpen( false )}>
      <Markdown>
        {content}
      </Markdown>
    </Dialog>
  )
}
