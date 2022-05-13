import { NextPage } from 'next'
import { User } from 'next-auth'
import { getSession } from 'next-auth/react'
import Head from 'next/head'
import { FormEvent, useState } from 'react'
import { DeleteAccountAction } from '../components/actions/delete-account-action'
import { Button } from '../components/form/button'
import { FormGroup } from '../components/form/form-group'
import { FormList } from '../components/form/form-list'
import { Input } from '../components/form/input'
import { Select } from '../components/form/select'
import { Message, MessageType } from '../components/message'
import { PageTitle } from '../components/page-title'

interface Plan {
  displayName: string,
  billingOption: string,
  price: string,
}

interface AccountProps {
  user: User
  plan: Plan
}

const billingOptions = [
  { value: 'monthly', name: 'Monthly' },
  { value: 'yearly', name: 'Yearly' }
]

export const Account: NextPage<AccountProps> = ( { user, plan } ) => {
  const [openDeleteAction, setOpenDeleteAction] = useState( false )
  const [userName, setUserName] = useState<string>( user.name || '' )
  const [globalMessage, setMessage] = useState<Message>()
  let messageTimeout: NodeJS.Timeout

  const updateMessage = ( message: Message ): void => {
    setMessage( message )
    clearTimeout( messageTimeout )
    messageTimeout = setTimeout( () => setMessage( undefined ), 1500 )
  }

  const submitProfileForm = async ( e: FormEvent<any> ): Promise<void> => {
    e.preventDefault()
    console.log( userName )
    updateMessage( { text: 'Profile data update has not yet been implemented', type: MessageType.Warning } )
  }

  const deleteAccount = (): void => {
    setOpenDeleteAction( false )
    updateMessage( { text: 'Deleting your account has not yet been implemented', type: MessageType.Warning } )
  }

  return (
    <>
      <Head>
        <title>Flooq | Account</title>
      </Head>
      <PageTitle name="Account Settings" message={globalMessage} />
      <main>
        <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <div className="px-4 py-6 sm:px-0 flex flex-col gap-6">
            <FormList title="Profile" description="This information is used to identify your account.">
              <FormGroup onSubmit={submitProfileForm}>
                <div className="grid md:grid-cols-2 gap-6 items-end">
                  <Input
                    label="E-Mail"
                    disabled={true}
                    defaultValue={user.email}
                  />
                  <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
                    The E-Mail address is used to uniquely identify your account and cannot be changed.
                  </p>
                </div>
                <div className="grid md:grid-cols-2 gap-6 items-end">
                  <Input
                    label="Username"
                    onChange={( e ): void => setUserName( e.target.value )}
                    value={userName}
                  />
                </div>
              </FormGroup>
            </FormList>

            <FormList title="Plan & Billing" description="This information describes your plan and billing">
              <FormGroup>
                <div className="grid md:grid-cols-2 gap-6 items-end">
                  <Input
                    label="Plan"
                    disabled={true}
                    defaultValue={plan.displayName}
                  />
                  <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
                    At this moment no other plans are available.<br />
                    Sign up for the newsletter to receive updates.
                  </p>
                </div>
                <div className="grid md:grid-cols-2 gap-6 items-end">
                  <Select
                    label="Billing"
                    disabled={true}
                    options={billingOptions}
                    selected={plan.billingOption}
                    onChange={console.log}
                  />
                  <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
                    Changing the billing option is not yet supported<br />
                    Contact <a className="text-blue-500" href="mailto:info@flooq.io">info@flooq.io</a> for help.
                  </p>
                </div>
              </FormGroup>
            </FormList>

            <FormList title="Danger Zone" description="Warning: The actions you take in this section are irreversible">
              <FormGroup>
                <div className="grid md:grid-cols-3 gap-6 items-end">
                  <Button dangerous onClick={(): void => setOpenDeleteAction( true )} type="button">
                    Delete Account
                  </Button>
                  <DeleteAccountAction open={openDeleteAction} setOpen={setOpenDeleteAction} onDelete={deleteAccount} />
                  <p className="mt-1 text-sm text-gray-500 dark:text-gray-400 col-span-2">
                    All your data flows will be deleted and cannot be recovered after deleting your account.
                  </p>
                </div>
              </FormGroup>
            </FormList>
          </div>
        </div>
      </main>
    </>
  )
}

export const getServerSideProps = async ( context: any ): Promise<any> => {
  const session = await getSession( context )
  if ( !session ) {
    return {
      redirect: {
        destination: '/login',
        permanent: false,
      },
    }
  }
  context.res.setHeader(
    'Cache-Control',
    'public, s-maxage=10, stale-while-revalidate=59'
  )
  return {
    props: {
      user: session.user,
      plan: {
        displayName: 'Free',
        billingOption: 'Monthly',
        price: '$ 0'
      }
    }
  }
}


export default Account
