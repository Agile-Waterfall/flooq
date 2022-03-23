# helm chart

## Installation guide

Working directory is the repository root.

- Install prerequisite (use brew on MacOS)
  - `minikube`
  - `helm`
- Start `minikube`
  ```bash
  $ minikube start
  ```
- Install dependencies
  ```bash
  $ helm dependency update ./helm
  ```
- Create a persistent volume
  ```bash
  $ kubectl apply -f ./helm/local-pv.yaml
  ```
- Install helm
  ```bash
  $ helm install flooq ./helm -f ./helm/values.yml
  ```

## Cleanup

To delete all pods and configurations (except persistent volumes) run:

```bash
$ helm delete flooq
```

Sometimes you might want to clean the persistent volume as well. For this use:

```bash
$ kubectl delete --all pv
$ kubectl delete --all pvc
```

## Add Data to the Database

Use this to add some default data to the database.

```
kubectl exec flooq-postgresql-0 -it -- psql -U postgres

\c flooqdatabase

SELECT * FROM "Versions";
INSERT INTO "Versions" ("Tag", "Name", "Notes") VALUES ('v0.0.0','0.0.0','Test');
```
