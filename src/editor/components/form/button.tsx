import { classNames } from '../../helper/class'

interface ButtonProps {
  disabled?: boolean,
  rest?: any[],
  children: any,
  primary?: boolean,
  secondary?: boolean,
  dangerous?: boolean,
  small?: boolean,
  type?: 'submit' | 'rest' | 'button',
  onClick?( e: any ): void
}

export const Button = ( { onClick, disabled = false, primary = false, secondary = false, small = false, dangerous = false, children, ...rest }: ButtonProps ): JSX.Element => (
  <button
    className={classNames(
      'rounded-full',
      primary ? 'bg-blue-500 hover:bg-blue-400 text-gray-100' : '',
      secondary ? 'bg-gray-300 hover:bg-gray-200 text-gray-800 dark:bg-gray-300' : '',
      dangerous ? 'bg-red-500 text-gray-100 disabled:bg-red-200 disabled:text-gray-50 dark:disabled:bg-red-400 dark:disabled:opacity-60' : 'disabled:bg-gray-100 dark:disabled:bg-gray-700 disabled:text-gray-300 dark:disabled:text-gray-500 ',
      small ? 'px-4 py-2 text-sm' : 'px-6 py-2'
    )}
    onClick={onClick}
    disabled={disabled}
    {...rest}
  >
    {children}
  </button>
)
