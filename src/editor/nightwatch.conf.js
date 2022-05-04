module.exports = {
  src_folders: ['spec/e2e'],

  test_settings: {
    default: {
      desiredCapabilities: {
        browserName: 'chrome',
        'goog:chromeOptions': {
          w3c: true,
          args: [
            '--headless'
          ]
        }
      },
      webdriver: {
        start_process: true,
        server_path: require( 'chromedriver' ).path
      }
    }
  }
}
