import express from 'express'
import { register } from 'prom-client'

const MetricsRouter = express.Router()

MetricsRouter.get( '', async ( _req, res ) => {
  res.status( 200 ).send( await register.metrics() )
} )

export default MetricsRouter
