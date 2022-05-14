export const forceUpdateSession = (): void => {
  const event = new Event( 'visibilitychange' )
  document.dispatchEvent( event )
}
