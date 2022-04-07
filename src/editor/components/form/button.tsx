interface ButtonProps {
  disabled?: boolean,
  rest?: any[],
  children: any,
  onClick( e: any ): void
}

export const Button = ( { onClick, disabled = false, children, ...rest }: ButtonProps ): JSX.Element => (
  <button
    className="bg-blue-500 hover:bg-blue-400 disabled:bg-gray-500 dark:disabled:bg-gray-700 disabled:text-gray-300 dark:disabled:text-gray-400 text-gray-100 px-6 py-2 rounded-full"
    onClick={onClick}
    disabled={disabled}
    {...rest}
  >
    {children}
  </button>
)
