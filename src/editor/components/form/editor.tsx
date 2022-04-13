import Editor from '@monaco-editor/react'
import { useEffect, useState } from 'react'

interface EditorOptions {
  height?: any,
  width?: any,
  value: any,
  language: string
  onChange( e: any ): void
  rest?: any,
  options?: any
}

export const CodeEditor = ( { height, value, language, options, onChange, ...rest }: EditorOptions ): JSX.Element => {

  const [theme, setTheme] = useState<string>()

  useEffect( () => {
    const colorScheme = window.matchMedia( '(prefers-color-scheme: dark)' )
    colorScheme.addEventListener( 'change', e => toggleTheme( e.matches ) )
    toggleTheme( colorScheme.matches )
    return colorScheme.removeEventListener( 'change', e => toggleTheme( e.matches ) )
  }, [] )

  const toggleTheme = ( isDarkMode: boolean ): void => {
    setTheme( isDarkMode ? 'vs-dark' : 'vs-light' )
  }

  const onMountEditor = ( editor: any, monaco: any ): void => {
    monaco.editor.defineTheme( 'vs-dark', {
      base: 'vs-dark',
      inherit: true,
      rules: [],
      colors: {
        'editor.background': '#111827',
      },
    } )
  }

  return (
    <Editor
      height={height}
      value={value}
      options={options}
      language={language}
      theme={theme}
      onChange={onChange}
      onMount={onMountEditor}
      {...rest}
    />
  )
}
