
/**
 * Replaces all token placeholders with the value set by the user
 * @param str the string to replace placeholders in
 * @param data the tokens of the user
 * @returns a string with all token placeholders replaced
 */
export function replaceTokens( str: string, data: Record<any, any> ): string {
  return str.replaceAll(
    /"?\{\{\s?token.([^{}\s]+)\s?\}\}"?/gm,
    ( _fullMatch, path ) => JSON.stringify( data[path] || 'undefined' )
  )
}
