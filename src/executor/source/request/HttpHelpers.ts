export function getDataFieldNameForMethod( method: string ): 'query' | 'body' {
  method = method.toUpperCase()
  return ['GET', 'DELETE'].includes( method ) ? 'query' : 'body'
}
