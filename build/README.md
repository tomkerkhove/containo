# Building Containo
This documentation describes how you can build the Docker containers and release to Kubernetes & Service Fabric Mesh with Visual Studio Team Services (VSTS).

In order to achieve this, you can create builds based on the YAML templates that are provided:
- `release-containers.yaml` - **Builds, tags and pushes the containers to a Docker container registry.**
- `release-kubernetes.yaml` - **Deploys Containo on a Kubernetes cluster.**
- `release-service-fabric-mesh.yaml` - **Deploys Containo on Azure Service Fabric Mesh.**

These are natively supported by VSTS which is documented [here](https://docs.microsoft.com/en-us/vsts/pipelines/build/yaml?view=vsts#manually-create-a-yaml-build-definition).

I'm currently using VSTS for this as well which you can find [here](https://tomkerkhove.visualstudio.com/Containo).

----------------------------

:rotating_light: _Currently I'm using builds to release to Kubernetes & Service Fabric Mesh given I only have 1 environment. In the real world, you should be using a release pipeline instead but the same concepts can be used._

----------------------------

## Builds, tags and pushes the containers to a Docker container registry
You will have to create a new Docker Hub service endpoint and replace it with the `Docker Hub (Tom Kerkhove)` value in `dockerRegistryEndpoint` ([docs]((https://docs.microsoft.com/en-us/vsts/pipelines/library/service-endpoints?view=vsts#sep-docreg))).

## Deploying Containo on a Kubernetes cluster
Before you can deploy to a Kubernetes cluster you will have to:
- Create a new Kubernetes service endpoint ([docs](https://docs.microsoft.com/en-us/vsts/pipelines/library/service-endpoints?view=vsts#sep-kuber))
- Assign the new endpoint in `kubernetesServiceConnection` instead of `Containo`
- Prepare your deployment declaration as described [here](./../deploy/README.md#deploying-containo-on-a-kubernetes-cluster)

## Deploying Containo on Azure Service Fabric Mesh
Before you can deploy to Azure Service Fabric Mesh you will have to:
- Create a new Azure Resource Manager service endpoint ([docs](https://docs.microsoft.com/en-us/vsts/pipelines/library/service-endpoints?view=vsts#sep-azure-rm))
- Change the YAML template to use the correct `azureSubscription`, `resourceGroupName` & `location`
- Prepare your deployment declaration as described [here](./../deploy/README.md#deploying-containo-on-azure-service-fabric-mesh)
