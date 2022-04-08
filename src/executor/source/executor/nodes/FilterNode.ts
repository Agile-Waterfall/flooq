import { Node } from '../../Dataflow'

export type Condition = 'ne' | 'eq' | 'gt' | 'lt' | 'ge' | 'le' | 'nn' | 're'

export interface FilterNode extends Node {
  data: {
      fieldName: string;
      condition: Condition;
      filterValue: string;
  }
}

/**
 * Filters an array of objects by an attribute.
 *
 * @param node to execute
 * @param inputs of the node as an object, with the handle ids as the keys and the inputs as the values.
 *               Must only have one entry. The entry must be an array of objects.
 * @returns the filtered array
 */
export function executeFilterNode(
  node: FilterNode,
  inputs: Record<string, Record<string, any>[]>
): Record<string, any>[] {
  if ( Object.values( inputs ).length !== 1 ) throw new Error( 'Filter node can only handle one input' )
  const input = Object.values( inputs )[0]
  switch( node.data.condition ) {
    case 'ne':
      return input.filter( e => e[node.data.fieldName] !== node.data.filterValue )
    case 'eq':
      return input.filter( e => e[node.data.fieldName] === node.data.filterValue )
    case 'gt':
      return input.filter( e => e[node.data.fieldName] > node.data.filterValue )
    case 'lt':
      return input.filter( e => e[node.data.fieldName] < node.data.filterValue )
    case 'ge':
      return input.filter( e => e[node.data.fieldName] >= node.data.filterValue )
    case 'le':
      return input.filter( e => e[node.data.fieldName] <= node.data.filterValue )
    case 'nn':
      return input.filter( e => e[node.data.fieldName] )
    case 're':
      return input
        .filter( e => new RegExp( node.data.filterValue ).test( e[node.data.fieldName] ) )
  }
}
