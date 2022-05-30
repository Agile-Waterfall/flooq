import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { Markdown } from '../components/markdown'

export default {
  title: 'Molecules',
  component: Markdown
} as ComponentMeta<typeof Markdown>

const markdownString = `# A title
## A subtitle

> A quote.

Some text with \`inline code\`, *italic* and **bold** sections.

### A second-level subtitle with a table

| Header 1 | Header 2 |
|-|-|
| Row 1 | Row 1 |
| Row 2 | Row 2 |

#### A third-level subtitle with a code block

\`\`\`js
const add = (a, b) => {
  return a + b
}
\`\`\``

const Template: ComponentStory<any> = ( { children } ) => (
  <Markdown>
    {children}
  </Markdown>
)

export const MarkdownWrapper = Template.bind( {} )

MarkdownWrapper.args={ children: markdownString }
