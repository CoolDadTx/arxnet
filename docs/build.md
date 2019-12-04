# Building the Code 

[Building the C++ Code](#building-the-c++-code)\
[Building the C# Code](#building-the-c#-code)

## Building the C++ Code

To build the native version set the solution configuration to one of the following configurations.

- `Debug (Native)`
- `Release (Native)`

Before the code will build the following steps must be completed.

### Install and Configure VCPkg

Ensure [vcpkg](https://docs.microsoft.com/en-us/cpp/build/vcpkg?view=vs-2019) is installed. This only needs to be done once. 

```shell
git clone <github path to vcpkg>
```

If it is already installed then update the source.

```shell
git pull
```

If the code has been updated then rebuild it.

```shell
.\bootstrap-vcpkg.bat
```

If vcpkg has not yet been integrated with Visual Studio then run the integration command.

```shell
vcpkg integrate install
```

### Install Required Libraries

Install each required library if it is not yet available.

- `glew`
- `sdl2`
- `sfml`

```shell
vcpkg install <library>
```

If a library is already available then ensure the latest version is installed.

```shell
vcpkg upgrade

### Update the libraries
vcpkg upgrade --no-dry-run
```

## Build the C# Code

To build the C# version set the solution configuration to one of the following configurations.

- `Debug`
- `Release`
