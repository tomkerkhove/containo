# Deploying Containo
This documentation describes how you can use the deployment declarations to deploy Containo on Kubernetes & Service Fabric Mesh.

In order to achieve this, you can create builds based on the YAML templates that are provided:
- `kubernetes\kubernetes-orders-declaration.yaml` - **Deploys Containo on a Kubernetes cluster.**
- `service-fabric-mesh\service-fabric-mesh-orders-declaration.json` - **Deploys Containo on Azure Service Fabric Mesh via Azure Resource Management.**

## Deploying Containo on a Kubernetes cluster
Before you can deploy to a Kubernetes cluster you will have to:
- Create a secret and assign it to the `ServiceBus_ConnectionString` secret ([docs](https://kubernetes.io/docs/concepts/configuration/secret/#creating-a-secret-manually))
- Create a secret and assign it to the `TableStorage_ConnectionString` secret ([docs](https://kubernetes.io/docs/concepts/configuration/secret/#creating-a-secret-manually))

When you want to use your own container registry, you need to:
- Update the declaration to use the images from your registry

You can either deploy to a Kubernetes cluster via a [VSTS build](./../build/README.md#deploying-containo-on-a-kubernetes-cluster) or via `kubectl`:
```
kubectl apply -f kubernetes\kubernetes-orders-declaration.yaml --namespace="<your-namespace>"
```

## Deploying Containo on Azure Service Fabric Mesh
Before you can deploy to Azure Service Fabric Mesh you will have to:
- Configure the connection string to Service Bus in a variable named `Orders_ServiceBus_ConnectionString`
- Configure the connection string to Azure Storage in a variable named `Orders_TableStorage_ConnectionString`

When you want to use your own container registry, you need to:
- Update the declaration to use the images from your registry

You can either deploy to Azure Service Fabric Mesh via a [VSTS build](./../build/README.md#deploying-containo-on-azure-service-fabric-mesh), Azure CLI or Azure Cloud Shell:
```
az mesh deployment create --resource-group containo-apps --template-uri <uri-to-declaration>
```

--------------------------------------------------------------------

:warning: _Currently there are issues when deploying Azure Service Fabric Mesh applications with parameterized name via ARM. It is best to change the template and hardcode the application name and update the port forwarding_

--------------------------------------------------------------------
