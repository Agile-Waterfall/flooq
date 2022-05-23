# helm chart

## Installation guide

Working directory is the repository root.

- Install prerequisite (use homebrew on MacOS)
  - `minikube`
  - `helm`
- Start `minikube`
  ```bash
  $ minikube start
  ```
- Copy the `values-staging.yaml` file to `values.yaml` and fill in the secrets.
- Install dependencies
  ```bash
  $ helm dependency update ./.helm-own
  ```
- Install helm
  ```bash
  $ helm install flooq ./.helm-own -f ./.helm/values.yaml
  ```

## Cleanup

To delete all pods and configurations run:

```bash
$ helm delete flooq
```
