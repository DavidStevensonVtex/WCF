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
