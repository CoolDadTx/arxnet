# Alternate Reality X (C#)

Playground project for seeing if I can get [Alternate Reality X](http://www.landbeyond.net/arx/index.php) running under C#/.NET.

Original Source Version: 0.82 \
Conversion Tool: [C++ to C# Converter](https://www.tangiblesoftwaresolutions.com/)

## Building Code 

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

### Build the Code

To build the native version set the solution configuration to `<configuration> (Native)`. To build the C# version set the solution configuration to `<configuration>`.

