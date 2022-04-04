if ( localStorage.theme === 'dark' || ( !( 'theme' in localStorage ) && window.matchMedia( '(prefers-color-scheme: dark)' ).matches ) ) {
  document.documentElement.classList.add( 'dark' )
} else {
  document.documentElement.classList.remove( 'dark' )
}

let vh = window.innerHeight * 0.01
document.documentElement.style.setProperty( '--vh', `${vh}px` )
