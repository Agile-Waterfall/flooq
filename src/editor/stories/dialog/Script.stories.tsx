import React from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { useDarkMode } from 'storybook-dark-mode'
import { ScriptNodeDialog } from '../../components/graph/script-node-dialog'

export default {
  title: 'Molecule/Dialog',
  component: ScriptNodeDialog
} as ComponentMeta<typeof ScriptNodeDialog>

const Template: ComponentStory<any> = ( args ) => {
  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      gap: '10px',
      justifyContent: 'flex-start',
      alignItems: 'flex-start'
    }}>
      <ScriptNodeDialog
        theme={useDarkMode() ? 'vs-dark' : 'vs-light'}
        {...args}
      />
    </div>
  )
}

export const ScriptEditor = Template.bind( {} )
ScriptEditor.args = {
  isOpen: true,
  setIsOpen: (): void => {},
  value: 'async (a) => {\n\treturn a\n}\n',
  setValue: (): void => {}
}
