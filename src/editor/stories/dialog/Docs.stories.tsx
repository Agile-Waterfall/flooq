import React, { useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { EditDataFlowDialog } from '../../components/flow/edit-dialog'
import { DocsDialog } from '../../components/docs-dialog'
import { Button } from '../../components/form/button'

export default {
  title: 'Molecules/Dialog',
  component: DocsDialog
} as ComponentMeta<typeof DocsDialog>

const Template: ComponentStory<any> = ( ) => {
  const [areDocsOpen, setAreDocsOpen] = useState( false )
  const content = `# A Title

Some text with some \`inline code\`, some **bold text** and some *italic text*.

> A quote.

## A Subtitle

- First item
- Second item
- Third item

### A third-level subtitle with a multiline-codeblock

\`\`\`js
const a = (b, c) => {
  return b + c;
}
  \`\`\``

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      gap: '10px',
      justifyContent: 'flex-start',
      alignItems: 'flex-start'
    }}>
      <Button primary onClick={(): void => setAreDocsOpen( true )}>Open Dialog</Button>
      <DocsDialog
        areDocsOpen={areDocsOpen}
        setAreDocsOpen={(): void => setAreDocsOpen( false ) }
        content={content} />
    </div>
  )
}

export const Docs = Template.bind( {} )
