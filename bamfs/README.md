# bamfs

Command-line console application and NuGet-packaged entry point for the BAM file system toolkit.

## Overview

bamfs is a console executable that serves as the host application for the BAM file system tools. It uses the bam.console framework, bootstrapping via `BamConsoleContext.StaticMain(args)` which provides menu-driven console interaction, command-line argument parsing, and service registration.

The project is configured for NuGet packaging (version 2.0.0, authored by Bryan Apellanes / ThreeHeadz) with a custom nuspec file (`bamfs.nuspec`). The build produces a publish-ready layout suitable for distribution as a .NET tool.

An earlier version of this application (`Chunker.cs`) contained a `ChunkServer` host that started a streaming chunk server backed by `FileSystemChunkStorage`, but this file is excluded from compilation in the current build. The current `Program.cs` delegates entirely to the BAM console context, meaning the application's behavior is determined by the menus and services registered by the referenced bam.console and bam.base libraries.

## Key Classes

| Class | Description |
|---|---|
| `Program` | Application entry point. Calls `BamConsoleContext.StaticMain(args)` to start the menu-driven console. |
| `Chunker` (excluded) | Legacy command-line tool that started a `ChunkServer` with `FileSystemChunkStorage`. Excluded from the current build. |

## Dependencies

### Project References

- bam.base
- bam.console

### Target Framework

- net10.0

### Output Type

- Exe (console application)

### Packaging

- Package ID: `bamfs`
- Version: 2.0.0
- NuSpec: `bamfs.nuspec`

## Usage Examples

### Running bamfs

```bash
# Launch the interactive console
dotnet run --project submodules/bamfs/bamfs/bamfs.csproj

# Pass command-line arguments
dotnet run --project submodules/bamfs/bamfs/bamfs.csproj -- --someArg value
```

## Known Gaps / Not Yet Implemented

- **Minimal functionality in Program.cs** -- The entry point delegates entirely to `BamConsoleContext.StaticMain`. The application does not register any bamfs-specific menus, services, or commands of its own.
- **`Chunker.cs` excluded** -- The chunk server entry point is excluded from compilation. If a standalone chunk server is needed, this file would need to be re-enabled and its dependencies updated.
- **`SemanticAssemblyInfo.cs` excluded** -- Assembly version metadata file is excluded from the build.
- **Missing nuspec file** -- The .csproj references `bamfs.nuspec` but this file was not found in the project directory; packaging may fail without it.
