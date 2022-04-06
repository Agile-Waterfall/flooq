export interface Message {
  text: string,
  type: MessageType
}

export enum MessageType {
  Error,
  Warning,
  Info
}

const getColorFromState = ( type: MessageType ): string => {
  switch ( type ) {
    case MessageType.Error:
      return 'bg-red-500'
    case MessageType.Warning:
      return 'bg-orange-400'
    default:
      return 'bg-blue-500'
  }
}

export const MessageContainer = ( { text, type }: Message ): JSX.Element => (
  <div className={`${getColorFromState( type )} absolute w-[100%] z-10`}>
    <div className="text-gray-50 max-w-7xl mx-auto py-4 px-4 sm:px-6 lg:px-8 flex justify-between">
      {text}
    </div>
  </div>
)
