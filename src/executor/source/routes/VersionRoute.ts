import express from 'express'
import { getApiVersion } from '../ApiInterface'
import Logger from '../utils/logging/Logger'

const versionRouter = express.Router()

versionRouter.get('', async (req, res) => {
    getApiVersion().then((apiResponse) => {
        res.status(200).send(apiResponse)
    }).catch((reason) => {
        Logger.error(reason)
        res.status(500).send()
    })
})

export default versionRouter;