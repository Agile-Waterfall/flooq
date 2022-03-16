# Executor Application

## Getting Started

1. Install prerequisites
  - [Node v17.7.0](https://nodejs.org/en/download/current/)
2. Install dependencies

```bash
npm install
```

3. Start dev setup

```bash
npm run dev
```

4. Start application

```bash
npm start
```


## Tests

The tests can be executed by running:

```bash
npm run test
```

## Run using docker

To run the executor using docker the following steps are required.

1. Create a new docker image from the code

    ```bash
    docker build . -t ghcr.io/agile-waterfall-inc/flooq-executor
    ```

2. Create a network if you don't have setup one already.

    ```bash
    docker network create flooq-network
    ```
    This network allows docker containers to talk to one another.

3. Run the docker container you just build.

    ```bash
    docker run -d -p 3500:3500 \
      --name flooq-executor \
      --network=flooq-network \
      --env=API_BASE_URL=http://localhost:8080 \
      ghcr.io/agile-waterfall-inc/flooq-executor
    ```

    This runs the docker container and exposes the port `3500`. The container is now in the `flooq-network`.

4. Visit `http://localhost:3500` to verify that the container is running.

## Environment
### Variables
- `API_URL`
- `API_PORT`
- `API_TOKEN`
