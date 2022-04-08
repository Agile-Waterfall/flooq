// interface HistoySpyInstance extends jest.SpyInstance {
//   getParamHistory(): any[][]
//   getLastParams(): any[]
//   getHistoryLength(): number
//   clearHistory(): void
// }

// function createHistorySpy( spy: jest.SpyInstance<any> ): HistoySpyInstance {
//   const historySpy: HistoySpyInstance = {
//     getParamHistory: function (): any[][] {
//       throw new Error( 'Function not implemented.' )
//     },
//     getLastParams: function (): any[] {
//       throw new Error( 'Function not implemented.' )
//     },
//     getHistoryLength: function (): number {
//       throw new Error( 'Function not implemented.' )
//     },
//     clearHistory: function (): void {
//       throw new Error( 'Function not implemented.' )
//     },
//     getMockName: spy.getMockName,
//     // mock: undefined,
//     mockClear: () => {
//       spy.mockClear(); return historySpy
//     },
//     mockReset: function (): HistoySpyInstance {
//       spy.mockReset(); return historySpy
//     },
//     mockRestore: spy.mockRestore,
//     getMockImplementation: spy.getMockImplementation,
//     mockImplementation: function ( fn?: ( ...args: any ) => any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockImplementationOnce: function ( fn: ( ...args: any ) => any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockName: function ( name: string ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockReturnThis: function (): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockReturnValue: function ( value: any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockReturnValueOnce: function ( value: any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockResolvedValue: function ( value: any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockResolvedValueOnce: function ( value: any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockRejectedValue: function ( value: any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mockRejectedValueOnce: function ( value: any ): HistoySpyInstance {
//       throw new Error( 'Function not implemented.' )
//     },
//     mock:
//   }

//   // return ( returnFunction: () => any ): () => any => {
//   //   let received: any
//   //   spy.mockImplementation( ( ...data ) => {
//   //     received = data
//   //     return returnFunction()
//   //   } )
//   //   return () => received
//   // }
// }
