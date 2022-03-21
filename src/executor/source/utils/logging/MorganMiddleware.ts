import morgan, { StreamOptions } from "morgan";
import Logger from "./Logger";

const stream: StreamOptions = {
  write: (message) => Logger.http(message),
};

//Only log if development
const skip = () => {
  const env = process.env.NODE_ENV || "development";
  return env !== "development";
};

const MorganMiddleware = morgan(
  ":method :url :status :res[content-length] - :response-time ms",
  { stream, skip }
);

export default MorganMiddleware;