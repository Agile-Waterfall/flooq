import { classNames } from '../../helper/class'

interface ButtonProps {
  disabled?: boolean,
  rest?: any[],
  children: any,
  primary?: boolean,
  secondary?: boolean,
  small?: boolean,
  onClick( e: any ): void
}

export const Button = ( { onClick, disabled = false, primary = false, secondary = false, small = false, children, ...rest }: ButtonProps ): JSX.Element => (
  <button
    className={classNames(
      primary ? 'bg-blue-500 hover:bg-blue-400 text-gray-100 ' : '',
      secondary ? 'bg-gray-300 hover:bg-gray-200 text-gray-800 dark:bg-gray-300 dark:' : '',
      small ? 'px-4 py-2 text-sm' : 'px-6 py-2',
      'disabled:bg-gray-100 dark:disabled:bg-gray-700 disabled:text-gray-300 dark:disabled:text-gray-500 rounded-full'
    )}
    onClick={onClick}
    disabled={disabled}
    {...rest}
  >
    {children}
  </button>
)
