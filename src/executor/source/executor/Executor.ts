import { Dataflow } from '../Dataflow'

export async function execute( dataFlow: Dataflow, input: any ): Promise<any> {
  return Promise.resolve( { 'a': 'b' } )
}
