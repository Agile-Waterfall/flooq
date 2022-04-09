// eslint-disable-next-line @typescript-eslint/explicit-function-return-type
const toggleDarkMode = ( isDarkMode ) => {
  if ( localStorage.theme === 'light' || ( !( 'theme' in localStorage ) && isDarkMode ) ) {
    document.documentElement.classList.add( 'dark' )
    localStorage.theme = 'dark'
  } else {
    document.documentElement.classList.remove( 'dark' )
    localStorage.theme = 'light'
  }
}

const colorScheme = window.matchMedia( '(prefers-color-scheme: dark)' )
colorScheme.addEventListener( 'change', e => toggleDarkMode( e.matches ) )
toggleDarkMode( colorScheme.matches )

let vh = window.innerHeight * 0.01
document.documentElement.style.setProperty( '--vh', `${vh}px` )
