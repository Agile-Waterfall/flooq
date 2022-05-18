export function replaceTokens( str: string, data: Record<any, any> ): string {
  return str.replaceAll(
    /"?\{\{\s?token.([^{}\s]+)\s?\}\}"?/gm,
    ( _fullMatch, path ) => JSON.stringify( data[path] || 'undefined' )
  )
}
