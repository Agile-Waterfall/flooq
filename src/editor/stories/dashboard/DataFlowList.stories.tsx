import React, { useState } from 'react'
import { ComponentStory, ComponentMeta } from '@storybook/react'
import { object } from '@storybook/addon-knobs'
import { DataFlowListItem, DataFlowListItemProps } from '../../components/dashboard/data-flow-list-item'
import { List } from '../../components/list/list'
import dayjs from 'dayjs'

export default {
  title: 'Molecules/Dashboard',
  component: DataFlowListItem
} as ComponentMeta<typeof DataFlowListItem>

const Template: ComponentStory<any> = ( args ) => {

  const [items, setItems] = useState( Object.values<DataFlowListItemProps>( args ) )
  const byLastEdited = ( a: any, b: any ): number => dayjs( b.lastEdited ).diff( dayjs( a.lastEdited ), 'seconds' )

  return (
    <div style={{ maxWidth: '1440px' }}>
      <List
        title="DataFlows"
        description="These are the DataFlows you have access to."
        action={{
          label: 'Create',
          onClick: () => setItems( [...items, { id: items.length, name: `New Item #${items.length}`, lastEdited: new Date().toISOString(), status: 'Disabled' }] )
        }}
      >{items.sort( byLastEdited )?.map( ( item: DataFlowListItemProps, i: number ): any => (
          <DataFlowListItem key={i} {...item} />
        ) )}
      </List>
    </div>
  )
}

export const DataFlowList = Template.bind( {} )

DataFlowList.args = object( 'List of DataFlow Items', [
  {
    id: 1,
    name: 'List Element #1',
    lastEdited: dayjs().subtract( 2, 'days' ).toISOString(), // prevent constant changes in CI
    status: 'Active'
  }
] )
