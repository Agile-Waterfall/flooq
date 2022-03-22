import { classNames } from '../helper/class'

describe( 'ClassNames Helper', () => {
  it( 'filters class names correctly', () => {

    const testTrue = true
    const testFalse = false

    const classnames = [
      testTrue ? 'class1--true' : 'class1--false',
      testFalse ? 'class2--true' : 'class2--false',
    ]

    const result = classNames( ...classnames )
    expect( result ).toBe( 'class1--true class2--false' )
  } )
} )

export {}
