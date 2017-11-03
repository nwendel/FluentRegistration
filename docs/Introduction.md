# Introduction

A fluent API for registering services with Microsoft.Extensions.DependencyInjection

## What is FluentRegistration?

FluentRegistration is a small open source library which adds a few extension methods to IServiceCollection that allows registration of servies based on conventions.

Other existing IoC containers have similar APIs:
* Autofac has the [RegisterAssemblyTypes](http://docs.autofac.org/en/latest/register/scanning.html).
* StructureMap has the [Scan](http://structuremap.github.io/registration/auto-registration-and-conventions/) method.
* Ninject has a separate package [Ninject.Extensions.Conventions](https://github.com/ninject/Ninject.Extensions.Conventions/wiki/Overview).
* Windsor has the [Install](https://github.com/castleproject/Windsor/tree/master/docs) and [Register](https://github.com/castleproject/Windsor/tree/master/docs) methods.

## Why Use FluentRegistration?

This library is useful when you want to register services via conventions, but:
* You don't already use or don't want to use a full IoC container.  
* When developing a library where you don't want to force your prefered IoC container onto the users of the library.
* Are happy with the built in container, but want convention-based registration.
