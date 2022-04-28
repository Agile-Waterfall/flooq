module.exports = {
  // An array of folders (excluding subfolders) where your tests are located;
  // if this is not specified, the test source must be passed as the second argument to the test runner.
  src_folders: ['spec/e2e'],

  webdriver: {
    start_process: true,
    port: 9515,
    server_path: require( 'chromedriver' ).path,
    cli_args: [
      // very verbose geckodriver logs
      // '-vv'
    ]
  },

  test_settings: {
    default: {
      desiredCapabilities : {
        browserName : 'chrome'
      }
    }
  }
}
