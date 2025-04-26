# GdUnit4NetExamples

This repository contains example projects and test cases demonstrating how to use [gdUnit4Net](https://github.com/MikeSchulze/gdUnit4Net), a C# unit testing framework for Godot 4.

## Table of Contents

- [Quick Start](#quick-start)
- [Project Structure](#project-structure)
- [Requirements & Prerequisites](#requirements--prerequisites)
- [Installation](#installation)
- [Running the Examples](#running-the-examples)
    - [Using JetBrains Rider](#running-the-examples-in-jetbrains-rider)
- [Examples Overview](#examples-overview)
    - [Unit Tests](#unit-tests)
    - [Integration Tests](#integration-tests)
    - [2D & 3D Examples](#examples)
- [Code Quality and Best Practices](#code-quality-and-best-practices)
    - [Build Configuration](#build-configuration)
    - [Code Style Enforcement](#code-style-enforcement)
    - [Documentation Practices](#documentation-practices)
    - [IDE Integration](#ide-integration)
- [Contributing](#contributing)
- [License](#license)

## Quick Start

For those familiar with Godot and C# testing who want to get started immediately:

```bash
# Clone the repository
git clone https://github.com/MikeSchulze/gdUnit4NetExamples.git

# Build the project (requires .NET 9.0)
cd gdUnit4NetExamples
dotnet build

# Open in your IDE or Godot to explore examples
```

The most basic example can be found in [UnitTests/DotNetObjectTest.cs](UnitTests/DotNetObjectTest.cs), which demonstrates simple assertions with .NET objects.

## Project Structure

The repository is organized into the following structure:

```
GdUnit4NetExamples/
├── UnitTests/          # Basic unit test examples
├── IntegrationTests/   # Integration test examples
├── Examples/           # Sample implementations to test
│   ├── MenuDemo2D/     # 2D menu testing example
│   └── RoomDemo3D/     # 3D room testing example
└── docs/               # Detailed documentation
```

For detailed documentation on specific testing techniques, see the [docs folder](docs/).

## Requirements & Prerequisites

To work with this project, you'll need:

### Software Requirements

- **Godot Engine**: Version 4.4.x with .NET/Mono support enabled
- **.NET SDK**: Version 9.0.x
- **IDE**: One of the following:
    - Visual Studio 2022
    - Visual Studio Code with C# extension
    - JetBrains Rider (recommended for best testing experience)

### Project Specifications

- **Framework**: .NET 9.0
- **Language Version**: C# 12.0
- **Code Style**: Standard .NET naming conventions and formatting (enforced via .editorconfig)
- **Documentation**: XML documentation required for all public members

---

## Installation

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/MikeSchulze/gdUnit4NetExamples.git
   cd gdUnit4NetExamples
   ```

2. **Verify your installed .NET SDKs**
   ```bash
   dotnet --list-sdks
    7.0.404 [C:\Program Files\dotnet\sdk]
    8.0.201 [C:\Program Files\dotnet\sdk]
   ```
   Ensure .NET 9.0.x is listed among the installed SDKs.

   **If .NET 9.0 is not installed:**

   a. Download the .NET 9.0 SDK from the [official .NET download page](https://dotnet.microsoft.com/download/dotnet)

   b. Follow the installation instructions for your operating system

   c. After installation, verify with:
   ```bash
   dotnet --list-sdks
    7.0.404 [C:\Program Files\dotnet\sdk]
    8.0.201 [C:\Program Files\dotnet\sdk]
    9.0.203 [C:\Program Files\dotnet\sdk]
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```
   This will restore all required NuGet packages and build the project.

4. **Open the project in Godot**
    - Launch Godot 4.4.x
    - Select "Import" and navigate to the cloned repository
    - Open the project.godot file

---

## Running the Examples

### Running the Examples in JetBrains Rider

1. **Open the project in Rider**
    - Launch JetBrains Rider
    - Select "Open" and navigate to the cloned repository
    - Open the `GdUnit4NetExamples.sln` solution file

2. **View Test Explorer**
    - Go to the "Tests" tab in the left sidebar
    - The test explorer should display your test classes and methods
    - If tests aren't visible, try refreshing the test explorer

3. **Configure Test Runner Settings**
    - From the test explorer, click the settings gear icon (⚙️)
    - Select "Unit Testing Settings..." from the dropdown menu
      ![Rider Test Settings](assets/rider-test-settings1.png)


4. **Set Up .runsettings File**
    - In the Settings dialog, navigate to Build, Execution, Deployment → Unit Testing → Test Runner
    - Under "Test settings," check "Use specific .runsettings/.testsettings settings file:"
    - Click the browse button and select the `.runsettings` file located in the project root
      ![Rider Test Runner Settings](assets/rider-test-settings2.png)

5. **Configure Additional Test Settings (Optional)**
    - While in the Settings dialog, you can also:
        - Enable "Capture output" to see console output from tests
        - Adjust the logging level as needed
    - Click "Save" to apply your settings

6. **Run the Tests**
    - In the test explorer, you can run tests at different levels:
        - Run all tests by clicking the run button at the top of the test explorer
        - Run a specific test class by right-clicking on it and selecting "Run"
        - Run a single test by right-clicking on the test name and selecting "Run"
    - You can also run tests directly from the code editor by clicking the green "run" icons in the gutter next to test methods or classes

---

## Examples Overview

### Unit Tests

The unit tests demonstrate basic testing capabilities with gdUnit4Net:

- Testing .NET objects vs Godot objects
- Basic assertions and test structure
- Test attributes and lifecycle methods

[View detailed unit testing documentation](docs/unit-testing.md)

### Integration Tests

The integration tests show how to test interactions between multiple components:

- Testing signals between objects
- Testing scene loading and instantiation
- Testing complex behaviors

[View detailed integration testing documentation](docs/integration-testing.md)

### Examples

- **Menu Demo (2D)**: Examples demonstrating how to test 2D menu interfaces
  [View Menu Demo documentation](docs/menu-demo.md)

- **Room Demo (3D)**: Examples showing how to test 3D environments and interactions
  [View Room Demo documentation](docs/room-demo.md)

---

## Code Quality and Best Practices

This example project demonstrates not only how to use GdUnit4Net for testing but also showcases best practices for maintaining high-quality C# code in Godot projects.

### Build Configuration

#### Directory.Build.props

We use a `Directory.Build.props` file to centralize common build settings across all projects. This ensures consistent code quality enforcement without having to duplicate settings
in each project file.

Key features:

- Enables .NET code analyzers
- Enforces code style rules during build
- Treats warnings as errors to maintain strict quality standards
- Generates XML documentation from code comments
- Includes StyleCop and Microsoft analyzers for comprehensive rule checking

```xml
<PropertyGroup>
  <EnableNETAnalyzers>true</EnableNETAnalyzers>
  <AnalysisLevel>latest</AnalysisLevel>
  <AnalysisMode>All</AnalysisMode>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

### Code Style Enforcement

#### .editorconfig

The repository includes a comprehensive `.editorconfig` file that defines coding standards. This file is automatically recognized by Visual Studio, VS Code, Rider, and other
editors that support the EditorConfig standard.

Benefits:

- Consistent code formatting across all team members
- Automatic enforcement of naming conventions
- Standardized indentation, spacing, and line breaks
- Enforced documentation practices

### Documentation Practices

All code in this example project includes proper XML documentation:

- Classes are documented with `<summary>` tags explaining their purpose
- Methods include parameter descriptions and return value documentation
- Examples demonstrate good practices for documenting test intentions

Example:

```csharp
/// <summary>
/// Demonstrates comparing two Godot.Vector2 instances.
///
/// This test verifies that Godot's Vector2 equality comparison works correctly.
/// Note that this is testing Godot's Vector2 type, not the System.Numerics.Vector2 type.
/// </summary>
[TestCase]
public void CompareGodotVector2() => AssertThat(new Vector2(1, 1))
    .IsEqual(new Vector2(1, 1));
```

### IDE Integration

This project is configured to work seamlessly with major IDEs:

#### Visual Studio

- Format on save is enabled through EditorConfig
- Code analysis runs in real-time
- XML documentation is validated during build

#### Visual Studio Code

- Settings in .vscode/settings.json configure the C# extension
- OmniSharp is configured to respect EditorConfig rules
- Format on save and analysis are enabled

#### JetBrains Rider

- EditorConfig support is enabled
- Code inspection profiles match the project standards
- Automatic formatting on save is configured

### Benefits for Your Projects

By following these patterns, you can:

1. Ensure consistent code quality across your team
2. Catch issues early through static analysis
3. Maintain readable and well-documented code
4. Reduce the effort needed for code reviews
5. Make your codebase more maintainable in the long term

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - [see the LICENSE file](LICENSE) for details.
