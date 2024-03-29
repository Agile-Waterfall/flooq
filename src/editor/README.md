# Editor Application

## Getting Started

1. Install dependencies

```bash
npm install
```

2. Start development server

```bash
npm run dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

## Tests

The tests can be executed by running:

```bash
npm run test
```

## Run using docker

To run the editor using docker the following steps are required.

1. Create a new docker image from the code

    ```bash
    docker build . -t ghcr.io/agile-waterfall/flooq-editor
    ```

2. Create a network if you don't have setup one already.

    ```bash
    docker network create flooq-network
    ```
    This network allows docker containers to talk to one another.

3. Run the docker container you just build.

    ```bash
    docker run -d -p 3000:3000 \
      --name flooq-editor \
      --network=flooq-network \
      --env=BASE_URL=http://localhost:3000 \
      --env=API_BASE_URL=http://localhost:8080 \
      ghcr.io/agile-waterfall/flooq-editor
    ```

    This runs the docker container and exposes the port `3000`. The container is now in the `flooq-network`.

4. Visit `http://localhost:3000` to verify that the container is running.

## Environment
### Variables
- `API_URL`
- `API_PORT`
- `API_TOKEN`
