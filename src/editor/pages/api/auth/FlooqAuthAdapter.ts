import { Adapter } from 'next-auth/adapters'

const FlooqAuthAdapter = (): Adapter => {
  return {
    async createUser( user ): Promise<any> {
      console.log( 'Create', user )
      const response = await fetch( `${process.env.API_BASE_URL}/api/User/`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify( {
            userName: user.name,
            email: user.email,
            emailConfirmed: user.emailVerified !== undefined
          } )
        }
      )

      if( !response.ok ) {
        const error = await response.text()
        console.log( error )
        throw 'Failed to create user'
      }
      return await response.json()
    },
    async getUser( id ): Promise<any> {
      console.log( 'Get User:', id )
      return
    },
    async getUserByEmail( email ): Promise<any> {
      const response = await fetch( `${process.env.API_BASE_URL}/api/User/email/${email}` )
      if ( !response.ok ) {
        console.log( 'User is not found in API' )
        return
      }
      const user = await response.json()
      console.log( 'Retrieved user from API', {
        id: user.id,
        name: user.userName,
        email: user.email,
        emailVerified: user.emailConfirmed,
        image: user.image,
      } )
      return {
        id: user.id,
        name: user.userName,
        email: user.email,
        emailVerified: user.emailConfirmed,
        image: user.image,
      }
    },
    async getUserByAccount( { providerAccountId, provider } ): Promise<any> {
      console.log( 'Get User by account:', providerAccountId, provider )
      return
    },
    async updateUser( user ): Promise<any> {
      console.log( 'Update user:', user )
      return
    },
    async deleteUser( userId ): Promise<any> {
      console.log( 'Delete user:', userId )
      return
    },
    async linkAccount( account ): Promise<any> {
      console.log( 'Link account:', account )
      return
    },
    async unlinkAccount( { providerAccountId, provider } ): Promise<any> {
      console.log( 'Unlink account:', providerAccountId, provider )
      return
    },
    async createSession(): Promise<any> {
      throw 'Sessions not implemented.'
    },
    async getSessionAndUser(): Promise<any> {
      throw 'Sessions not implemented.'
    },
    async updateSession(): Promise<any> {
      throw 'Sessions not implemented.'
    },
    async deleteSession(): Promise<any> {
      throw 'Sessions not implemented.'
    },
    async createVerificationToken( { identifier, expires, token } ): Promise<any> {
      console.log( 'Create verification token :', identifier, expires, token )
      return
    },
    async useVerificationToken( { identifier, token } ): Promise<any> {
      console.log( 'Use verification token :', identifier, token )
      return
    },
  }
}

export default FlooqAuthAdapter
