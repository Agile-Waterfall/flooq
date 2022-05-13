module.exports = {
  'env': {
    'browser': true,
    'es6': true
  },
  'extends': ['next/core-web-vitals'],
  'parserOptions': {
    'ecmaVersion': 2021,
    'sourceType': 'module'
  },
  'rules': {
    'indent': ['error', 2, {
      'SwitchCase': 1
    }],
    'linebreak-style': ['error', 'unix'],
    'quotes': ['error', 'single', {
      'allowTemplateLiterals': true
    }],
    'semi': ['error', 'never'],
    'eol-last': ['error', 'always'],
    'eqeqeq': ['error', 'always'],
    'spaced-comment': ['error', 'always', {
      'markers': ['/']
    }],
    'block-spacing': ['error', 'always'],
    'no-whitespace-before-property': ['error'],
    'no-trailing-spaces': ['error'],
    'space-in-parens': ['error', 'always'],
    'object-curly-spacing': ['error', 'always'],
    'no-unused-vars': ['off'],
    'brace-style': 'off'
  }
}
