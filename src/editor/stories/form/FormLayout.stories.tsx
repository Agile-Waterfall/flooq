import React, { useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { Input } from '../../components/form/input'
import { FormList } from '../../components/form/form-list'
import { FormGroup } from '../../components/form/form-group'
import { Button } from '../../components/form/button'

export default {
  title: 'Molecules/Form',
  component: FormList
} as ComponentMeta<typeof FormList>

const Template: ComponentStory<typeof FormList> = ( args ) => {
  const [userName, setUserName] = useState<string>( 'Username' )
  const [email, setEmail] = useState<string>( 'email@sample.com' )

  return (
    <div className="px-4 py-6 sm:px-0 flex flex-col gap-6">
      <FormList title={args.title} description={args.description}>
        <FormGroup onSubmit={console.log}>
          <div className="grid md:grid-cols-2 gap-6 items-end">
            <Input
              label="E-Mail"
              disabled={true}
              onChange={( e ): void => setEmail( e.target.value )}
              value={email}
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
      <FormList title="Form List without action" description={args.description}>
        <FormGroup>
          <div className="grid md:grid-cols-2 gap-6 items-end">
            <Input
              label="Plan"
              disabled={true}
              defaultValue="Free"
            />
            <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
            At this moment no other plans are available.<br />
            Sign up for the newsletter to receive updates.
            </p>
          </div>
        </FormGroup>
      </FormList>
      <FormList title="Form List with Button" description="Warning: The actions you take in this section are irreversible">
        <FormGroup>
          <div className="grid md:grid-cols-3 gap-6 items-end">
            <Button dangerous onClick={console.log} type="button">
                  Delete Account
            </Button>
            <p className="mt-1 text-sm text-gray-500 dark:text-gray-400 col-span-2">
                  All your data flows will be deleted and cannot be recovered after deleting your account.
            </p>
          </div>
        </FormGroup>
      </FormList>
    </div>
  )
}

export const FormLayout = Template.bind( {} )
FormLayout.args = {
  title: 'Profile',
  description: 'This information is used to identify your account.'
}
