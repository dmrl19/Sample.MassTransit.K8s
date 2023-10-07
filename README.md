# Sample.MassTransit.K8s

This is a sample project to use Mass Transit in your local K8s cluster.

## Prerequisites

It will be good to have a basic knowledge of:
* [RabbitMq](https://www.rabbitmq.com/getstarted.html)
* [Docker](https://docs.docker.com/get-started/)
* [Kubernetes](https://kubernetes.io/docs/concepts/)
* [Mass Transit](https://masstransit.io/documentation/concepts)

## Setup RabbitMq in your Local cluster

By default your K8s cluster does not have the [RabbitMQ Cluster Kubernetes Operator] installed. For that you will need to first [installed](https://github.com/rabbitmq/cluster-operator#rabbitmq-cluster-kubernetes-operator) in your cluster. You can do it by executing it the following command:

```sh
kubectl apply -f https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml
```

After this you can run RabbitMq in your k8s cluster. You can go to [deploy-rabbitmq-k8s.yml](src/Sample.MassTransit.K8s/K8s/deploy-rabbitmq-k8s.yml) and execute the following command:
```sh
kubectl apply -f deploy-rabbitmq-k8s.yml
```


By executing this command this will also create a couple of `secrets`, one of them is the username & password to access to the dashboard. You can access them by executing the following commands
```sh
# Get RabbitMq Username
kubectl get secret rabbitmqcluster-sample-default-user -o jsonpath='{.data.username}' | base64 --decode
# Get RabbitMq Password
kubectl get secret rabbitmqcluster-sample-default-user -o jsonpath='{.data.password}' | base64 --decode
```


You can find more info about it [here](https://blog.rabbitmq.com/posts/2020/08/deploying-rabbitmq-to-kubernetes-whats-involved/)

## Setup your services Locally in K8s

To run the project in K8s, first you will need to build the docker images of each project located in:
* [Api-Dockerfile](src/Sample.MassTransit.K8s/Sample.MassTransit.K8s.Api/Dockerfile)
* [Consumer-Worker-Dockerfile](src/Sample.MassTransit.K8s/Sample.MassTransit.K8s.Worker.Consumer/Dockerfile)
* [Cronjob-Producer-Dockerfile](src/Sample.MassTransit.K8s/Sample.MassTransit.K8s.Wroker.CronJob.Producer/Dockerfile)

To build them you will need to execute the following command:
```sh
docker build . -f Sample.MassTransit.K8s.Api/Dockerfile -t "[ImageName]:[ImageTag]"
```

After executing it on each project, you can go to the [K8s](src/Sample.MassTransit.K8s/K8s/) folder, where you will find the yaml files to deploy the services in your k8s cluster. There you will find the following files:
* [deploy-webapi-k8s.yaml](src/Sample.MassTransit.K8s/K8s/deploy-webapi-k8s.yml)
* [deploy-worker-consumer-k8s.yaml](src/Sample.MassTransit.K8s/K8s/deploy-worker-consumer-k8s.yml)
* [deploy-worker-cronjob-producer-k8s.yaml](src/Sample.MassTransit.K8s/K8s/deploy-worker-cronjob-producer-k8s.yml)

To run them you will need to run the following command:
```sh
kubectl apply -f [K8sYamlFile]
```

After executing it on each deployment file, you will be able to see them by executing the command `kubectl get all` and if you want to see the logs of it you can execute the command `kubectl logs [podName]`.

