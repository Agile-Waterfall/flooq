import MarkdownToJSX, { MarkdownToJSX as MarkdownToJSXNamespace } from 'markdown-to-jsx'
import styles from '../styles/markdown.module.scss'

interface MarkdownProps {
  children: string;
  options?: MarkdownToJSXNamespace.Options;
}

const defaultOptions: MarkdownToJSXNamespace.Options = {
  forceBlock: true,
}

export const Markdown = ( { children, options }: MarkdownProps ): JSX.Element => (
  <div className={styles.markdown}>
    <MarkdownToJSX options={Object.assign( {}, defaultOptions, options )}>
      {children}
    </MarkdownToJSX>
  </div>
)
