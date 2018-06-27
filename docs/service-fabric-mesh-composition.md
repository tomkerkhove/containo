# Service Fabric Mesh
The application is modelled in to one **application** which contains two services - The API & an asynchronous worker.

**Services** allow you to define parts of an application that can be scaled independently from other services. In our scenario we would prefer to scale the worker out when messages on the queue pile up while we'd like to scale our API based on CPU and/or memory.

A service can have multiple **code packages** which are basically Docker containers running next to each other and form a scale unit. In our case, this is represented by our API container which is using the orders validator [sidecar](https://docs.microsoft.com/en-us/azure/architecture/patterns/sidecar).

![Service Fabric Mesh](./../media/docs/service-fabric-mesh-composition.png)

The API service is part of a **network** which is exposing the API to the internet. To achive this, the service defines an endpoint with its internal port which is referenced in the network configuration and assigns a public port to leverage port forwarding.
