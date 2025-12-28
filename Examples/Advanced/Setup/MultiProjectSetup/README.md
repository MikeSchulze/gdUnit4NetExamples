# Multi-Project Test Setup with Symbolic Links

An advanced example demonstrating how to test Godot projects in a multi-project solution using symbolic links to preserve resource paths
and maintain clean separation between production and test code.

## What This Example Shows

This project demonstrates:

- **Multi-project test architecture** with separate test and production projects
- **Symbolic link strategy** for resource path preservation
- **Single assembly compilation** merging test and production code
- **Cross-platform compatibility** for Windows, Linux, and macOS
- **Git-friendly setup** with proper .gitignore configuration
- **CI/CD ready structure** with automated symlink creation

## Why Use Multi-Project Setup?

### Advantages over Single Project

- **Clean Separation**: Tests don't bloat production builds
- **Independent Dependencies**: Test-only packages stay in test project
- **Better Organization**: Clear boundaries between test and production code
- **Flexible Deployment**: Ship production without test dependencies
- **IDE Performance**: Faster IntelliSense with smaller project scopes

### When to Use

- ✅ Large projects requiring extensive test coverage
- ✅ Team projects with dedicated QA/test developers
- ✅ Production builds where size and dependencies matter
- ✅ Projects following enterprise architecture patterns
- ✅ When you need different build configurations for test vs production

## Files Overview

### Main Project (ExampleProject/)

- **`ExampleProject.csproj`** - Production project with Godot SDK
- **`project.godot`** - Godot project configuration
- **`src/`** - Source code, scenes, and resources

### Test Project (ExampleProject.Test/)

- **`ExampleProject.Test.csproj`** - Test project with GdUnit4 configuration
- **`.gitignore`** - Excludes symlinked folders from version control
- **`test/`** - Test files and test-specific resources
- **`src/`** - Symlink to main project's src folder (created at build time)

## Key Configuration Sections

### 1. Test Project Configuration

```xml
<Project Sdk="Godot.NET.Sdk/4.5.1">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <RootNamespace>GdUnit4.Examples.Advanced.Setup.MultiProjectSetup</RootNamespace>
  </PropertyGroup>

  <PropertyGroup>
    <!-- Ensures all dependencies are copied to output for test execution -->
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
    <!-- Identifies this as a GdUnit4 test project for test discovery -->
    <TestFramework>GdUnit4</TestFramework>
  </PropertyGroup>
  
  <!-- IMPORTANT: Exclude symlinked src folder from compilation -->
  <ItemGroup>
    <!-- Prevent compilation of C# files from symlinked folder -->
    <Compile Remove="src/**/*.cs" />
    <None Remove="src/**/*.cs" />
  </ItemGroup>
  
  <ItemGroup>
    <!-- Reference to main project -->
    <ProjectReference Include="../ExampleProject/ExampleProject.csproj"/>
    
    <!-- Core .NET testing infrastructure -->
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.1"/>
    <!-- Test frameworks -->
    <PackageReference Include="gdUnit4.api" Version="5.1.0-rc3"/>
    <!-- Test adapter to integrate GdUnit4 with .NET test runners -->
    <PackageReference Include="gdUnit4.test.adapter" Version="3.0.0"/>
  </ItemGroup>
</Project>
```

### 2. Symbolic Link Creation

```xml
<Target Name="CreateSymlinks" BeforeTargets="BeforeBuild">
  <PropertyGroup>
    <SrcSource>../ExampleProject/src</SrcSource>
    <SrcTarget>$(MSBuildProjectDirectory)/src</SrcTarget>
  </PropertyGroup>
  
  <!-- Creates symlink automatically during build -->
</Target>
```

### 3. Git Configuration

```gitignore
# .gitignore in test project
.godot/
src        # Symlinked folder - don't commit
```

## Project Structure

```shell
Solution/
├── ExampleProject/
│   ├── ExampleProject.csproj
│   ├── project.godot
│   └── src/
│       ├── ExampleScene.tscn
│       ├── ExampleScene.cs
│       ├── Calculator.cs
│       ├── scenes/
│       │   └── LabelScene.tscn
│
├── ExampleProject.Test/
│   ├── ExampleProject.Test.csproj
│   ├── .gitignore
│   ├── src -> ../ExampleProject/src  [SYMLINK - created at build]
│   └── test/
│       ├── CalculatorTest.cs
│       └── ExampleSceneTest.cs
│
└── ExampleProject.sln
```

## Setting Up the Project

### Prerequisites

- .NET 9.0 SDK or later
- Godot 4.5+ (compatible with your Godot.NET.Sdk version)
- GdUnit4 test adapter
- **Windows**: Administrator privileges or Developer Mode enabled (for symlinks)
- **Linux/macOS**: Standard user permissions

### Initial Setup

1. **Clone the repository**

   ```bash
   git clone <repository>
   cd MultiProjectSetup
   ```

2. **Build the solution**

   ```bash
   dotnet build
   ```

   The symlink will be created automatically during the first build.

3. **Run tests**

   ```bash
   cd ExampleProject.Test
   dotnet test
   ```

### Windows-Specific Setup

If symlink creation fails on Windows:

- **Option 1: Enable Developer Mode**
  1. Open Settings → Update & Security → For Developers
  2. Enable "Developer Mode"
  3. Rebuild the project

- **Option 2: Run as Administrator**
  1. Open terminal as Administrator
  2. Navigate to project directory
  3. Run `dotnet build`

- **Option 3: Use Junction (fallback)**

  ```cmd
  cd ExampleProject.Test
  mklink /J src ..\ExampleProject\src
  ```

## Resource Path Resolution

### How It Works

The symlink strategy ensures resource paths work identically in both projects:

**In Main Project:**

```csharp
var scene = GD.Load<PackedScene>("res://src/scenes/MainScene.tscn");
var icon = GD.Load<Texture2D>("res://src/assets/icon.png");
```

**In Test Project (with symlink):**

```csharp
// Same paths work because of the symlink!
var scene = GD.Load<PackedScene>("res://src/scenes/MainScene.tscn");
var icon = GD.Load<Texture2D>("res://src/assets/icon.png");
```

### Why Not Link project.godot?

The `project.godot` file contains project-specific settings like:

- `project/assembly_name="ExampleProject"` - conflicts with test assembly"
- Project-specific configurations that shouldn't affect tests
- Test project can have its own test-specific project settings

## Running Tests

### Command Line

```bash
# From solution root
dotnet test

# With specific verbosity
dotnet test -v detailed

# With filter
dotnet test --filter "FullyQualifiedName~PlayerTest"
```

### Visual Studio / Rider

1. Open the solution file
2. Build the solution (symlinks created automatically)
3. Use Test Explorer to run tests

### VS Code

1. Install C# and .NET Test Explorer extensions
2. Open the workspace
3. Tests appear in Testing sidebar
4. Run individually or all at once

## Comparison with Other Setups

| Aspect | Single Project | Multi-Project with Copy | Multi-Project with Symlinks |
|--------|---------------|------------------------|----------------------------|
| **Resource Paths** | Direct access | Path adjustments needed | Original paths preserved |
| **Build Time** | Fastest | Slower (file copying) | Fast (symlink creation) |
| **Disk Usage** | Minimal | Duplicated resources | Minimal (symlinks) |
| **Git Complexity** | Simple | Complex (duplicates) | Simple (symlinks ignored) |
| **CI/CD Setup** | Simple | Complex | Simple |
| **Team Workflow** | Mixed code | Clean separation | Clean separation |
| **Maintenance** | Harder | Medium | Easiest |

## Troubleshooting

### CS0436 Warning: Type Conflicts

**Problem:**

```shell
warning CS0436: The type "ExampleScene" in "ExampleProject.Test\src\ExampleScene.cs" 
conflicts with the imported type "ExampleScene" in "ExampleProject, Version=1.0.0.0"
```

**Cause:**
The symlinked `src` folder is being compiled by the test project, causing duplicate type definitions.
Types exist in both the referenced `ExampleProject.dll` and are being compiled again from the symlinked source files.

**Solution:**
Add the following to your `ExampleProject.Test.csproj` to exclude symlinked source files from compilation:

```xml
<ItemGroup>
  <!-- Exclude all .cs files from the symlinked src folder -->
  <Compile Remove="src/**/*.cs" />
  <None Remove="src/**/*.cs" />
</ItemGroup>
```

This ensures:

- ✅ C# types come only from the ProjectReference (ExampleProject.dll)
- ✅ Godot resources (.tscn, .tres) remain accessible via symlink
- ✅ No duplicate type warnings
- ✅ Clean compilation without conflicts

The symlink is used **only for resource loading**, not for code compilation.

### Symlink Creation Failed

**Windows:**

- Error: "You do not have sufficient privilege"
  - Solution: Enable Developer Mode or run as Administrator
  - Alternative: Use junction with `mklink /J` instead of `mklink /D`

**Linux/macOS:**

- Error: "Permission denied"
  - Solution: Check file system permissions
  - Verify source path exists

### Resources Not Found

- Error: "Cannot load resource: res://src/..."
  - Check symlink exists: `ls -la ExampleProject.Test/`
  - Manually create if needed: `ln -s ../ExampleProject/src src`
  - Verify .gitignore isn't excluding needed files

### Test Discovery Issues

- Ensure GdUnit4 test adapter is installed
- Check test class has `[TestSuite]` attribute
- Verify test methods have `[TestCase]` attribute
- Rebuild solution if tests aren't detected

### Different Assembly Names

- Test project uses its own assembly name
- This is intentional - avoids conflicts with main project
- Both assemblies are loaded during test execution

## Best Practices

1. **Keep Tests Isolated**: Don't modify shared resources in tests
2. **Use Test Data**: Create test-specific data when needed
3. **Clean State**: Reset any modified state after tests
4. **Parallel Safety**: Ensure tests can run in parallel
5. **Resource Loading**: Test resource paths early in development
6. **Documentation**: Document any special setup requirements
7. **Exclude Source from Compilation**: Always exclude symlinked .cs files to prevent CS0436 warnings

## Additional Resources

- [GdUnit4 Documentation](https://github.com/MikeSchulze/gdUnit4Net)
- [Godot Testing Best Practices](https://docs.godotengine.org/en/stable/tutorials/best_practices/)
- [.NET Testing Documentation](https://docs.microsoft.com/en-us/dotnet/core/testing/)
- [Symbolic Links on Windows](https://docs.microsoft.com/en-us/windows/security/threat-protection/security-policy-settings/create-symbolic-links)
