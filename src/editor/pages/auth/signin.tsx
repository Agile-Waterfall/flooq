import { NextPage } from 'next'
import { BuiltInProviderType, ProviderType } from 'next-auth/providers'
import { ClientSafeProvider, getProviders, signIn } from 'next-auth/react'
import { Button } from '../../components/form/button'
import { GitHubIcon } from '../../components/icons/github'

interface SignInProps {
  providers: any
}

const SignIn: NextPage<SignInProps> = ( { providers } ) => {
  const gitHubProvider: ClientSafeProvider = providers['github']

  return (
    <div className="flex items-center justify-center py-6">
      {gitHubProvider !== undefined &&
        <Button onClick={(): Promise<any> => signIn( gitHubProvider.id, { callbackUrl: '/' } )} primary>
          <div className="flex gap-2">
            <GitHubIcon className="w-6 h-6 fill-white" />
            Sign in with {gitHubProvider.name}
          </div>
        </Button>
      }
    </div>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const providers = await getProviders()
  return {
    props: { providers },
  }
}


export default SignIn
