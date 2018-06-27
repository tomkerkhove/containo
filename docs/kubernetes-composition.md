# Kubernetes
The application is modelled in multiple **deployments** which each deploy **replica sets**.

Replica Sets are a way of what **pods** it should be running and can be scaled independently from other Replica Sets; either manually or automatically. In our scenario we will have a replica set for our API & our asynchornous worker so that they can be scaling based on their specific needs. In our scenario we would prefer to scale the worker out when messages on the queue pile up while we'd like to scale our API based on CPU and/or memory.

A pod can have multiple **containers** running next to each other and form a scale unit. In our case, this is represented by our API container which is using the orders validator [sidecar](https://docs.microsoft.com/en-us/azure/architecture/patterns/sidecar).

![Kubernetes](./../media/docs/kubernetes-composition.png)

Unfortunately Kuberenetes does not have the concept of "an application" which represents a group of replica sets _(at least not to my knowledge)_. However, it allows you to create isolated **namespaces** which isolate entities from another namespace so that they don't interfer with each other or another person changes your application.

Another interesting aspect is that every entity is also capable of providing metadata about itself by assinging **labels**. In our scenario every entity is deployed in a dedicated namespace and has at least the following labels - `app`, `microservice`, `service`, `type`.

By using a **service** we can expose our API to the internet. This entity is using the labels that we have assigned to forward traffic to the internal port of the container inside the pod by using port forwarding.
