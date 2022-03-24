# Flooq

[![CI Build - Editor](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/ci.editor.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/ci.editor.yml)
[![CI Build - API](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/ci.api.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/ci.api.yml)
[![CI Build - Executor](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/ci.executor.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/ci.executor.yml)

A tool that enables developers to connect multiple applications without the need to develop any interface.

![Title](./docs/assets/title-image.svg)

## Environments

|Env|Service|Status|
|-|-|-|
|`staging`|[Editor](https://flooq-editor-staging.k8s.init-lab.ch/)|[![CD - Deploy Staging](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.staging.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.staging.yml)|
|`staging`|[Executor](https://flooq-executor-staging.k8s.init-lab.ch/)|[![CD - Deploy Staging](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.staging.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.staging.yml)|
|`prod`|[Editor](https://flooq-editor-prod.k8s.init-lab.ch/)|[![CD - Deploy Prod](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.prod.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.prod.yml)|
|`prod`|[Executor](https://flooq-executor-prod.k8s.init-lab.ch/)|[![CD - Deploy Prod](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.prod.yml/badge.svg)](https://github.com/Agile-Waterfall-Inc/flooq/actions/workflows/cd.deploy.prod.yml)|

## Documentation

- [Docs](./docs/README.md)
  - [Idea](./docs/idea.md)
  - [Architecture](./docs/architecture.md)
  - [Glossary](./docs/glossary.md)
- [Code Quality](https://sonarcloud.io/organizations/agile-waterfall/projects)

## Setup

- [Frontend](./src/editor/README.md)
- [API](./src/api/README.md)
- [Executor](./src/executor/README.md)
