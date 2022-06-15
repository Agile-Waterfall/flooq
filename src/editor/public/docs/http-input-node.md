# HTTP Input Node

This node returns the contents of the request triggering the data flow execution.

## Fields

| Field Name  | Function                                                                                                                                                                                                               |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Endpoint    | Calling this URL triggers the data flow execution.                                                                                                                                                                     |
| HTTP Method | Choose which HTTP method the data flow should listen for. Depending on which method is takes the data flow uses either the body (`POST`, `PUT`, `PATCH`) or the url params (`GET`, `DELETE`) to pass to the next node. |