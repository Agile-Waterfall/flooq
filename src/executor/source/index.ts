import express, { Request, Response } from 'express'

const app = express()

app.get( '/', ( req: Request, res: Response ): void => {
  res.send( 'Hello World' )
} )

app.listen( 6000 )
console.log( 'Started Webserver at http://localhost:6000' )
