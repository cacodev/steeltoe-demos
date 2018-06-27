# Steeltoe Demos

This repo contains several sample/demo apps that demonstrate usage of the components in the steeltoe toolbox

__Note:__ most of these project rely on docker for 3rd party services like eureka and spring boot config

## circuit-breaker

This project demonstrates a failover when main service calls another service and service 1 is unavailable.

### Getting started

1. Start eureka by executing `dockerrun-eurekaserver.sh`
2. Start `main-app` via dotnet run command `dotnet run -p main-app`
3. At this point, you can observe that the circuit breaker is working by visiting http://localhost:5000/api/values
4. Start `service1-app` via dotnet run command `dotnet run -p service1-app`
5. Look at http://localhost:5000/api/values again. Observe that the circuit breaker is now closedand returning the values from service-1. Service 1 values are being retrieved at http://localhost:5001/api/values

### main-app

Project that contains the circuit breaker code.

Some code to look at:

`Startup.cs` - Observe the service registration in the configuration functions

`appsettings.json` - Observe the eureka settings

`Services/BaseDiscoveryService.cs` - Abstract base client class for calling services that register with the local eureka discovery server. This was boiler plate code I copied from the steeltoe examples

`Services/IService1.cs` - Interface for calling service 1

`Services/Service1.cs` - Class that calls service 1.  Inherits `BaseDiscoveryService.cs`

`Services/GetService1Values.cs` - Implementation of the circuit breaker


### service1-app

Project that registers with the discovery server and returns strings when the api is invoked