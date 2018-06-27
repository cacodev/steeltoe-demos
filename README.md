# Steeltoe Demos

This repo contains several sample/demo apps that demonstrate usage of the components in the steeltoe toolbox

__Note:__ most of these project rely on docker for 3rd party services like eureka and spring boot config

## circuit-breaker

This project demonstrates a failover when main service calls another service and service 1 is unavailable.

### Getting started

1. Start eureka by executing `./circuit-breaker/dockerrun-eurekaserver.sh`
2. Start `main-app` via dotnet run command `dotnet run -p circuit-breaker/main-app`
3. At this point, you can observe that the circuit breaker is working by visiting http://localhost:5000/api/values
4. Start `service1-app` via dotnet run command `dotnet run -p circuit-breaker/service1-app`
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

## config-app

This project demostrates how the spring config server is used to provide dynamic 

### Getting started
1. Start config server by executing `./config-app/dockerrun-configserver.sh`
2. Call http://localhost:8888/application/default and note that it returns a json value including the values from the `application.yml`
3. Start `main-app` via dotnet run command `dotnet run -p config-app`
4. Call http://localhost:5000/api/configvalues and note that the values from `application.yml` are returned by this api

Code to look at:

`Program.cs` - In the `BuildWebHost` function, notice that `.AddConfigServer()` is added to use the spring config service

`Startup.cs` - Note the addition for the registration of the configuration

`appsettings.json` - Note the spring cloud server configuration.

`Models/ConfigServerValues.cs` - Model for the config values

`Controllers/ConfigValuesController.cs` - Returns the values from the config server

`config/application.yml` - Values used by the config server

`dockerrun-configserver.sh` - Note that I am passing in the path for config values

## discovery

This project demostrates how the main service leverages discovery to find service 1 and service 2 to call

1. Start eureka by executing `./discovery/dockerrun-eurekaserver.sh`
2. Start `main-app` via dotnet run command `dotnet run -p discovery/main-app`
3. Start `service1-app` via dotnet run command `dotnet run -p discovery/service1-app`
4. Look at http://localhost:5000/api/values/1 and Observe that the client is returning the values from service-1. Service 1 values are being retrieved at http://localhost:5001/api/values
5. Start `service2-app` via dotnet run command `dotnet run -p discovery/service2-app`
6. Look at http://localhost:5000/api/values/2 and Observe that the client is returning the values from service-2. Service 2 values are being retrieved at http://localhost:5002/api/values
7. Call http://localhost:5000/api/values and it will return values for both service 1 and service 2


### main-app

Project that contains the circuit breaker code.

Some code to look at:

`Startup.cs` - Observe the service registration in the configuration functions

`appsettings.json` - Observe the eureka settings

`Services/BaseDiscoveryService.cs` - Abstract base client class for calling services that register with the local eureka discovery server. This was boiler plate code I copied from the steeltoe examples

`Services/IService1.cs` - Interface for calling service 1

`Services/Service1.cs` - Class that calls service 1.  Inherits `BaseDiscoveryService.cs`

`Services/IService2.cs` - Interface for calling service 2

`Services/Service2.cs` - Class that calls service 2.  Inherits `BaseDiscoveryService.cs`

`Services/GetService1Values.cs` - Implementation of the circuit breaker


### service1-app

Project that registers with the discovery server and returns strings when the api is invoked

## mgmt-app

This project demonstrates the various management points provided by steeltoe

### Getting started

1. Start `mgmt-app` via dotnet run command `dotnet run -p mgmt-app`
2. Observe the management endpoints:
    - http://localhost:5000/info
    - http://localhost:5000/loggers
    - http://localhost:5000/trace
    - http://localhost:5000/dump
    - http://localhost:5000/heapdump
    - http://localhost:5000/health

Code to look at:

`Startup.cs` - Observe the service registration in the configuration functions

`appsettings.json` - Observe the management settings
