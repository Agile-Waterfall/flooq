describe( 'Test for the Pages of flooq.io', function () {
  test( 'Test the Developer page', function ( browser ) {
    browser
      .url( 'https://editor-staging.flooq.io/developer' )
      .assert.not.urlContains( 'http://' )
      .waitForElementVisible( 'main' )
      .assert.elementPresent( 'header' )
      .assert.elementPresent( 'main' )
      .assert.titleContains( 'Flooq | Developer' )
      .assert.textContains( 'header h1', 'Developer' )
      .assert.textContains( 'main li:nth-child(1)', 'running' )
      .assert.textContains( 'main li:nth-child(2)', 'connected' )
      .end()
  } )
  test( 'Test the Dashboard page', function ( browser ) {
    browser
      .url( 'https://editor-staging.flooq.io/' )
      .assert.not.urlContains( 'http://' )
      .waitForElementVisible( 'body' )
      .assert.elementPresent( 'header' )
      .assert.elementPresent( 'main' )
      .assert.elementPresent( 'main button' )
      .assert.titleContains( 'Flooq | Dashboard' )
      .assert.textContains( 'header h1', 'Dashboard' )
      .assert.textContains( 'main button', 'Add new Data Flow' )

      .end()
  } )
} )
