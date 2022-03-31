import winston from 'winston'
import DailyRotateFile from 'winston-daily-rotate-file'

const levels = {
  error: 0,
  warn: 1,
  info: 2,
  http: 3,
  debug: 4
}

const level = (): string => {
  const env = process.env.NODE_ENV || 'development'
  const isDevelopment = env === 'development'
  return isDevelopment ? 'debug' : 'warn'
}

const silent = (): boolean => {
  const env = process.env.NODE_ENV || 'development'
  return env === 'test'
}

const colors = {
  error: 'red',
  warn: 'yellow',
  info: 'green',
  http: 'magenta',
  debug: 'white',
}

winston.addColors( colors )

const fileFormat = winston.format.combine(
  winston.format.timestamp( { format: 'YYYY-MM-DD HH:mm:ss:ms' } ),
  winston.format.printf(
    ( info ) => `${info.timestamp} ${info.level}: ${info.message}`,
  ),
)

const consoleFormat = winston.format.combine(
  winston.format.timestamp( { format: 'YYYY-MM-DD HH:mm:ss:ms' } ),
  winston.format.colorize( { all: true } ),
  winston.format.printf(
    ( info ) => `${info.timestamp} ${info.level}: ${info.message}`,
  ),
)

const transports = [
  new winston.transports.Console( { format: consoleFormat } ),
  new winston.transports.File( {
    filename: 'logs/error.log',
    level: 'error',
    format: fileFormat
  } ),
  new DailyRotateFile( {
    filename: 'logs/daily/executor-%DATE%.log',
    datePattern: 'YYYY-MM-DD-HH',
    zippedArchive: true,
    maxSize: '20m',
    maxFiles: '14d',
    format: fileFormat
  } )
]

const Logger = winston.createLogger( {
  level: level(),
  levels,
  transports,
  silent: silent()
} )

export default Logger
