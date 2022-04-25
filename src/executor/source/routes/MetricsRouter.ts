import express from 'express'
import { register } from 'prom-client'

const MetricsRouter = express.Router()

MetricsRouter.get( '', async ( req, res ) => {
  res.status( 200 ).send( register.metrics() )
} )

export default MetricsRouter
