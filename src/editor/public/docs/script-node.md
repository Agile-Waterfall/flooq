# Script Node

The script node allows for the transformation of data with custom JavaScript snippets.

It must contain a function called `handler`:

```javascript
const handler = (a) => {
	return a
}
```

Each parameter of the `handler` function is mapped to an incoming handle. To add a new handle simply add the parameter to the function and it will appear on the left side of the node. To remove a handler the parameter needs to be removed.