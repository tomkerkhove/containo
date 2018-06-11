# Deploying Containo
This documentation describes how you can use the deployment declarations to deploy Containo on Kubernetes & Service Fabric Mesh.

In order to achieve this, you can create builds based on the YAML templates that are provided:
- `orders-declaration-kubernetes.yaml` - **Deploys Containo on a Kubernetes cluster.**
- `orders-declaration-service-fabric-mesh.json` - **Deploys Containo on Azure Service Fabric Mesh via Azure Resource Management.**

## Deploying Containo on a Kubernetes cluster
Before you can deploy to a Kubernetes cluster you will have to:
- Create a secret and assign it to the `ServiceBus_ConnectionString` secret ([docs](https://kubernetes.io/docs/concepts/configuration/secret/#creating-a-secret-manually))
- Create a secret and assign it to the `TableStorage_ConnectionString` secret ([docs](https://kubernetes.io/docs/concepts/configuration/secret/#creating-a-secret-manually))

When you want to use your own container registry, you need to:
- Update the declaration to use the images from your registry

You can either deploy to a Kubernetes cluster via a [VSTS build]() or via `kubectl`:
```
kubectl apply -f orders-declaration-kubernetes.yaml --namespace="<your-namespace>"
```

## Deploying Containo on Azure Service Fabric Mesh
Before you can deploy to Azure Service Fabric Mesh you will have to:
- Change the YAML template to use the correct `azureSubscription`, `resourceGroupName` & `location`
- Configure the connection string to Service Bus in a variable named `Orders_ServiceBus_ConnectionString`
- Configure the connection string to Azure Storage in a variable named `Orders_TableStorage_ConnectionString`

When you want to use your own container registry, you need to:
- Update the declaration to use the images from your registry
