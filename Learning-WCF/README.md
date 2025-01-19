# Learning WCF, by Michele Leroux Bustamante, ©2007

## Chapter 1: Hello Indigo

WCF was formerly code-named "Indigo".

Services can be accessed over a variety of supported protocols:

-   Named pipes
-   TCP
-   HTTP
-   MSMQ

WCF supports all of the core emerging web service standards.

WCF provides all of the plumbing for security, transactions, and reliable messaging over any protocol.

WSDL - Web Services Description Language

### Service Oriented Architecture

-   [Organization for the Advancement of Structured Information Standards - OASIS](http://www.oasis-open.org/)

*   Service Oriented Architecture (SOA) is a paradighm for organizing and utilizing distributed capabilities that may be under the control of different ownership domains.

#### From OOP to SOA

_Component-oriented programming_ introduces the concept of sharing binaries that encapsulate reusable classes.

Invoking a remote component of any kind requires serializing a messageand passing it accross applicable process or machine boundaries.

_Service orientation_ delivers a solution to these problems by introducing (via web services) the concept of contracts, policies and interopability.

#### What Is A Service?

_Service_ refers to the entry point or "window" through which business functionality can be reached.

Services are not necessarily web services -- they are merely chunks of business functionality exposed in some way such that they respect the tenets of SOA.

#### Tenets Of SOA

-   Service boundaries are explicit.
-   Services are autonomous.
-   Clients and services share contracts, not code.
-   Compatibility is based on policy.

##### Service boundaries are explicit

Services are responsible for exposing a specific set of business functionality through a wel-defined contract,
where the contract describes a set of concrete operations and messages supported by the service.

As for ASMX and WCF, contracts are described in Web Services Description Language (WSDL).

The contract is independent of the location of the service in all cases.

##### Services are autonomous

Services encapsulate business functionality, but they must also encapsulate other dependencies of the business tier.

In this way, the entire service should be moveable or even replaceable without impact to other services or system functionality.

It should be able to perform key functions without external dependencies. This is what is meant by _atomicity_.

Part of atomicity also dictates the following:

-   The service boundary must act as an independent unit for versioning. Changes to business components may require versioning of the service contract, for example.
-   The service boundary is the deployment boundary for callers.
-   The service must operate in isolation and fault-tolerant. That is, exceptions behind the service tier should not impact other services.

##### Clients and services share contracts, not code

The contract must not change once published, or must at a minimum remain backward compatible to existing clients -- and this requires discipline.

In theory, contracts are not tied to a particular technology or platform.

ASMX web services, and WCF services, all support this tenet since they all are capable of publishing a contract that is consumed by clients without sharing code (tpe libraries or WSDL respectively).

##### Compatibility is based on policy

Policy describes other constraints, such as communication protocols, security requirements, and reliability requirements.

ASMX with Web Services Enhancements (WSE) and WCF support publishing policy requirements.

#### Big SOA, Little SOA

_Big SOA_ refers to connecting disparate systems across application, department, corporate and even industry boundaries.

_Little SOA_ refers to describing how applications are designed as chunks of business functionality that are isolated behind explicit service boundaries.

### WCF Services

### Fundamental WCF Concepts

_System.ServiceModel_ is the assembly that contains core functionality for WCF.

#### Message Serialization

Remote Procedure Calls (RPC) and XML messaging formats are two common ways for applications to communicate.

To achieve interoperability, systems rely on a standard format for messages understood by both ends of the communication. Applications still exchange messages, but they are formatted in XML according to known protocols.

The technology used to support this is traditionally associated with web servcies such as ASMX, WSE, or WCF.

WCF can be used to achieve both RPC-style messaging and web service messaging.

#### Services

WCF applications expose functionality through services. A _service_ is a Common Language Runtime (CLR) type that enscapsulates business functionality and exposes a set of methods that can be accessed by remote clients.

A service must implement a service contract.

A _service contract_ is defined by appling the `ServiceContractAttribute` to a class or interface.

Methods exposed by a class or interface must be decorated with the _OperationContractAttribute_ to be considered part of the service contract. Methods with this attribute are considered _service operations_.

#### Hosting

Hosting options include:

-   Self-hosting - console applications, Windows Forms or WPF applications, or Windows Services
-   Internet Information Services (IIS)
-   Windows Activation SErvice (WAS) - This is similar to IIS hosting, but is only available to IIS 7.0.

Basically, to host any service, you construct a `ServiceHost`, provide it with a service type to activate for incoming messages, provide it with one or more addresses where the service can be located along with the service contract supported by each address, and provide it with the supported communication protocol.

#### Endpoints

When the `ServiceHost` opens a communication channel for a service, it must expose at least one endpoint for the service so that clients can invoke operations.

An _endpoint_ describes where services can be reached, how they can be reached, and what operations can be reached.
Endpoints have three key parts:

-   Address - refers to the URI where messages can be sent to the service
-   Binding - Bindings indicate the protocols supported when the messages are sent to a particular address
-   Contract - Each address supports a specific set of operations, as described by a service ocntract.

The `ServiceHost` is provided with a list of endpoints before the communication channel is opened. These endpoints each receive messages for their associated operations over the specified protocols.

##### Addresses

Format: scheme://domain[:port]/[path]

Some examples of valid base addresses:

-   net.tcp://localhost:9000
-   net.pipe:/mymachinename
-   http://localhost:8000
-   http://www.anydomain.com
-   net.msmq://localhost

A _path_ is usually provided as part of the address to disambiguate service endpoints.

-   net.tcp://localhost:9000/ServiceA
-   net.pipe:/mymachinename/ServiceB
-   http://localhost:8000/ServiceA
-   http://www.anydomain.com/ServiceA
-   net.msmq://localhost/ServiceA

##### Bindings

A _binding_ describes the protocols supported by a particular endpoint, specifically , the following:

-   The _transport protocol_, which can be TCP, named pipes, HTTP, or MSMQ.
-   The _message encoding format_, which determines whether messages are serialized as binary or XML, for example
-   Other _messaging protocols_ related to security and reliability, or custom protocols

There are number of predefined bindings (called _standard bindings_) provided by the server model.

##### Metadata

ONce the `ServiceHost` is configured for one or more endpoints, and communcation channels are open, ser vice operations can be invoked at each endpoint.

Information about service endpoints are part of the _metadata_ for a particular service. Clients rely on this metadata to generate proxies to invoke the service.

Metadata can be accessed in two ways. The `ServiceHost` can expose a metadata exchange endpoint to access metadata at runtime, or it can be used to generate a WSDL document representing the endpoints and protocols supported by the service. In either case, clients use tools to generate proxies to invoke the service.

##### Proxies

Clients communicate with services using proxies. A _proxy_ is a type that exposes operations representative of a service contract that hides the serialization details from the client application when invoking service operations.

Tools also exist to generate proxies and endpoint configurations from metadata.

##### Channels

Channels facilitate communication between clients and services in WCF.

The `ServiceHost` creates a _channel listener_ for each endpoint, which generates a communication channel.

The channel stack includes a transport channel, a message encoding channel, and any number of message processing channels for security, reliability, and other features.

##### Behaviors

Behaviours also influence how messages are processed by the service model.

A _behavior_ modifies the way messages are processed as they flow through the channel stack.

There are behaviors to control many service model features such as exposing metadata, authentication and authorization, transactions, message throttling, and more.

Behaviors are enabled either in configuration or by applying behavior attributes in client proxies and services.

### Creating a New Service from Scratch

1. Create a new service contract and service implementation.
1. Programmatically configure a service host, its endpoints, and bindings.
1. Create a client application and open a client channel proxy to invoke the service.

Visual Studio 2022 will need to be opened as Administrator to run this application.

#### Assembly Allocation

Three projects: one for the service, another for the host, and another for the client.

This approach allows you to expose a service behind the firewall over TCP, and yet also allow remote, interoperable clients to consume it over HTTP. These two approaches require distinct hosting environments (specifically, a Windows service and IIS).

It is recommended that you create a separate projectx for service contracts and services.

Business logic has no place in the service assembly.

Business functionality should never be coupled with the service implementation because it is possible that multiple services and applications may need to reuse the same business logic. If business logic is stored in its own assemblies, this type of sharing is made easy.

Another reason to decouple business logic from service implementation is to improve manageability and versioning.
The service tier may need to coordinate logging activities and exception handling around calls to business components, and the service tier may need to be versioned, while business components and associated functionality have not changed.

A separate set of assemblies for:

-   Host
-   Service
-   Business Components
-   Data Access

#### Defining a Service

The first step in creating a service is to define a service contract. YOu can create a service contract by applying the `ServiceContractAttribute` to an interface or type. Methods on the interface or type will not be included in the service contract until the `OperationContractAttribute` is applied. In a typical service contract, all methods will be included in the contract -- after alll, the entire reason for defining a service contract is to expose operations as part of a service.

Business interfaces should not be directly converted into service contracts. LIkewise, business components should not be directly converted to services. The service tier should instead be explicitly defined with the sole purpose of exposing public functionality and should internally consume business components, rather than embed business logic with the service implementation.

When you implement a service contract as an interface, the service type implements this interface.

An alternative to this approach is to apply both the `ServiceContractAttribute` and the `OperationContractAttribute` directly to the service type.

#### Hosting a Service

Any managed process can host services. Within that process, you can create one or more `ServiceHost` instances, each associated with a particular service type and one or more endpoints for that type.

Before opening the `ServiceHOst` instance, you can also provide it with base addresses if you are planning to create relative endpoints. In order to reach the service, at least one endpoint is required.

#### Exposing Service Endpoints

Endpoints expose service functionality at a particular address. Each endpoint is associated with a particular contract and a set of protocols as defined by the binding configuration. For each service, one or more endpoints may be exposed if multiple contracts are present or if multiple protocols are desired to access service functionality.

##### Addresses

The address can be a complete URI or a relative address like that used in the lab.

Complete URI:

```
using (ServiceHost host = new ServiceHost(typeof(HelloIndigo.HelloIndigoService))
{
    host.AddServiceEndpoint(typeof(HelloIndigo.IHelloIndigoService), new BasicHttpBinding(), "http://localhost:8000/HelloIndigo/HelloIndigoService");
    // other code
}
```

Relative Address:

```
    using (ServiceHost host = new ServiceHost(typeof(HelloIndigo.HelloIndigoService), new Uri("http://localhost:8000/HelloIndigo")))
    {
        host.AddServiceEndpoint(typeof(HelloIndigo.IHelloIndigoService), new BasicHttpBinding(), "HelloIndigoService");
        // other code
    }
```

In practice, a base address should be supplied for each transport protocol over which the service can be accessed -- for example, HTTP, TCP, named pipes or MSMQ.

Using relative endpoint addressing makes it possible to modify the base URI to move all associated relative endpoints to a new domain or port. This can siplify the deployment process.

##### Bindings

The binding provided to an endpoint can be any of the standard bindings supplied by the service model.

host.AddServiceEndpoint(typeof(HelloIndigo.IHelloIndigoService), **new BasicHttpBinding()**, "HelloIndigoService");

The choice of binding defines the communication channel. `BasicHttpBinding` supports requests over HTTP protocol sent in text format without any additional protocols for addressing, reliable messaging, security or transactions.

##### Contracts

Each endpoint is associated with a particular service contract that determines the operations available at the endpoint.

A service with multiple contracts could expose a different endpoint for each contract it wants to make accessible to clients.

#### Creating a Client Proxy

Clients use a proxy to consume a service endpoint. A proxy can be created manually using a channel factory, or it can be generated using tools.

The bare necessities reuired to communicate with a service are:

-   The address of the service endpoint
-   The protocols required to communicate with the service endpoint or the binding
-   The service contract metadata as described by the service contract associated with endpoint

```
    EndpointAddress ep = new EndpointAddress("http://localhost:8000/HelloIndigo/HelloIndigoService");
    IHelloIndigoService proxy = ChannelFactory<IHelloIndigoService>.CreateChannel(new BasicHttpBinding(), ep);
```

`ChannelFactory<T>` is a service model type that can generate the client proxy and underlying channel stack. You provide the address, binding, and service contract type and call CreateChannel() to generate the channel stack discussed earlier.

In order for communication between the client and service to succeed, the binding must be equivalent to the binding specified at the service endpoint. _Equivalence_ means that the transport protocol is the same, the message-encoding format is the same, and any additional messaging protocols used at the service to serialize messages are also used at the client.

### Generating a Service and Client Proxy

You'll use the following:

-   Visual Studio service tmeplates
-   Service Configuration Editor
-   ServiceModel Metadata Utility (SvcUtil)

You will use declarative configuration settings instead of code to configure the service host and client. To enable proxy generation, you'll access service metadata, which involves enabling a service behavior. In addition, you learn more about service configuration settings for base addresses, endpoints, bindings and behaviors.

#### Using the WCF Service template

1. In the Host project, Add -> New Item and select WCF Service, and name the file _HelloIndigoService.cs_.
1. In the Host project, right click the App.config and select Edit WCF Configuration.

#### Generating a proxy using the Service Model Metadata Utility

```
cd D:\drs\WCF\GitHub\WCF\Learning-WCF\ch01\CreateANewService\ClientTwo>
svcutil /d:. /o:serviceproxy.cs /config:app.config http://localhost:8000/HelloIndigo
```

#### Service Templates

_WCF Service Library_

#### ServiceModel Metadata Utility

Executable named _svcutil.exe_.

This tool can be used for two key purposes:

-   To generate code and configuration settings from metdata.
-   To generate metadata from code and configuration.

With SvcUtil you can export and import metadata, validate services, and manipulate how types are generated and shared between services and clients.

Add Service Reference uses SvcUtil to generate proxies and configuration, but from the command line, you can exercise greater control over this process.

```
svcutil /d:. /o:serviceproxy.cs /config:app.config http://localhost:8000/HelloIndigo
```

To see all the options for SvcUtil, from the command line you can type:

`svcutil /?`

#### Service Configuration Editor

_svcconfigeditor.exe_

You can edit the \<service.serviceModel\> section for any client or host application file.

The wizard guides you through steps to configure services, bindings, behaviors, and more, which is particularly useful when you are new to WCF since you may not be familiar with each section of te configuration file.

When starting from scratch, you can use the tool to add new services or client endpoints, to attach behaviors to services or endpoints, to supply base addresses for the host, to supply metadata exchange endpoints, and even to customize binding configurations.

For existing applications, you may just use the tool to view settings and make minor changes.

#### ServicModel Configuration

All configuration settings related to the service model are contained within the \<system.serviceModel\> section.

Sections

-   \<services\>
-   \<client\>
-   \<bindings\>
-   \<behaviors\>

Service model configuration settings can also be set at runtime through the proxy or `ServiceHost`; however, declarative configuration is often preferred to hardcoding settings in code.

#### ServiceHost Initialization

Consider the `ServiceHost` constructor:

`ServiceHost myServiceHost = new ServiceHost(typeof(HelloIndigo.HelloIndigoService));`

```
<service behaviorConfiguration="serviceBehavior" name="HelloIndigo.HelloIndigoService">
    <host>...</host>
    <endpoint>...</endpoint>
    <endpoint>...</endpoint>
</service>
```

The `<service>` element can include base addresses and service endpoints.

```
<host>
    <baseAddresses>
    <add baseAddress="http://localhost:8000/HelloIndigo" />
    <add baseAddress="http://localhost:9000/HelloIndigo" />
    <add baseAddress="net.pipe://localhost:8000/HelloIndigo" />
</host>
```

One or more `<endpoint>` sections may be provided.
An endpoint is defined by an address, contract, and binding.

```
<endpoint binding="basicHttpBinding" name="basicHttp" contract="Host.IHelloIndigoService" />
<endpoint address="HelloIndigoService" binding="basicHttpBinding" name="basicHttp" contract="Host.IHelloIndigoService" />
<endpoint address="http://localhost:8001/HelloIndigo/HelloIndigoService" binding="basicHttpBinding" name="basicHttp" contract="Host.IHelloIndigoService" />
```

Reasons why a service may expose multiple endpoints:

-   The service implements multiple contracts, each requiring its own endpoint.
-   The same or different service contracts must be accessible over multiple protocols.
-   The same or different service contracts must be accessible by clients with different binding requirements, possibly related to security, reliable messaging, message size, or transactions.
