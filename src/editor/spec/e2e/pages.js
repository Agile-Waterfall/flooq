describe( 'Test for the Pages of flooq.io', function () {
  test( 'Test the Developer page', function ( browser ) {
    browser
      .url( 'https://editor-staging.flooq.io/developer' )
      .waitForElementVisible( 'main' )
      .assert.elementPresent( 'main' )
      .assert.titleContains( 'Flooq | Developer' )
      .assert.not.urlContains( 'http://' )
      .assert.textContains( 'li:nth-child(1)', 'running' )
      .assert.textContains( 'li:nth-child(2)', 'connected' )
      .end()
  } )
} )
