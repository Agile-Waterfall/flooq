import axios, { AxiosResponse } from 'axios'
import https from 'https'

const apiBaseAddress = 'https://localhost:8080/api/'

export async function getApiVersion(): Promise<string> {
    //This needs to be done since a self-signed certificate is used. This shoud NOT be used in production
    const agent = new https.Agent({  
      rejectUnauthorized: false
    })
    const response: AxiosResponse = await axios.get(apiBaseAddress + 'Version', {httpsAgent: agent})
    return response.data
}