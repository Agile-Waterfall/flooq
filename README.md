# Flooq

[![CI Build - Landing](https://github.com/agile-waterfall/flooq/actions/workflows/ci.landing.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/ci.landing.yml)
[![CI Build - Editor](https://github.com/agile-waterfall/flooq/actions/workflows/ci.editor.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/ci.editor.yml)
[![CI Build - API](https://github.com/agile-waterfall/flooq/actions/workflows/ci.api.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/ci.api.yml)
[![CI Build - Executor](https://github.com/agile-waterfall/flooq/actions/workflows/ci.executor.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/ci.executor.yml)
[![CI Build - Identity](https://github.com/agile-waterfall/flooq/actions/workflows/ci.identity.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/ci.identity.yml)

A tool that enables developers to connect multiple applications without the need to develop any interface.

![Title](./docs/assets/title-image.svg)

## Environments

|Env|Service|Status|
|-|-|-|
|`staging`|[Landing](https://landing-staging.flooq.io/)|[![CD - Deploy Staging](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.staging.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.staging.yml)|
|`staging`|[Editor](https://editor-staging.flooq.io/)|[![CD - Deploy Staging](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.staging.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.staging.yml)|
|`staging`|[Executor](https://executor-staging.flooq.io/version)|[![CD - Deploy Staging](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.staging.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.staging.yml)|
|`prod`|[Landing](https://flooq.io/)|[![CD - Deploy Prod](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.prod.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.prod.yml)|
|`prod`|[Editor](https://editor.flooq.io/)|[![CD - Deploy Prod](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.prod.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.prod.yml)|
|`prod`|[Executor](https://executor.flooq.io/)|[![CD - Deploy Prod](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.prod.yml/badge.svg)](https://github.com/agile-waterfall/flooq/actions/workflows/cd.deploy.prod.yml)|

## Documentation

- [Docs](./docs/README.md)
  - [Idea](./docs/idea.md)
  - [Architecture](./docs/architecture.md)
  - [Glossary](./docs/glossary.md)
- [Code Quality](https://sonarcloud.io/organizations/agile-waterfall/projects)

## Setup

- [Landing](./src/landing/README.md)
- [Editor](./src/editor/README.md)
- [API](./src/api/README.md)
- [Executor](./src/executor/README.md)
- [Identity](./src/identity/README.md)