import express, { Request, Response } from 'express'

const app = express()

app.get( '/', ( req: Request, res: Response ): void => {
  res.send( 'Hello World!' )
} )

app.listen( 3500 )
console.log( 'Started Webserver at http://localhost:3500' )
