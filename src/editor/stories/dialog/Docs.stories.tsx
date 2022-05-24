import React, { useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { DocsDialog } from '../../components/docs-dialog'
import { Button } from '../../components/form/button'

export default {
  title: 'Molecules/Dialog',
  component: DocsDialog
} as ComponentMeta<typeof DocsDialog>

const MarkdownExample = require( './markdown-example.md' ).default( {} )

const Template: ComponentStory<any> = ( ) => {
  const [areDocsOpen, setAreDocsOpen] = useState( false )
  return (
    <>
      <Button primary onClick={(): void => setAreDocsOpen( true )}>Open Dialog</Button>
      <DocsDialog
        areDocsOpen={areDocsOpen}
        setAreDocsOpen={(): void => setAreDocsOpen( false ) }
      >
        <MarkdownExample/>
      </DocsDialog>
    </>
  )
}

export const Docs = Template.bind( {} )
