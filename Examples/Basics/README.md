# Basics Examples

Welcome to the gdUnit4Net basics! This section provides step-by-step tutorials and fundamental examples to get you started with unit testing in Godot using C#.

## Prerequisites

To follow these examples, you need:
- Basic understanding of C# programming
- Godot 4.4+ with .NET support installed
- .NET 9.0 SDK
- An IDE (Visual Studio, VS Code, or JetBrains Rider recommended)

## Getting Started

**New to gdUnit4Net?** Follow this learning path:
1. [Project Setup Examples](Setup/) - Learn how to configure test projects
2. [Basic Test Examples](Tests/) - Master fundamental testing patterns and techniques

## Basics Structure

The basic examples are organized into:

```
├── Setup/              # Project setup and configuration examples
│   ├── MinimalTestProjectSetup/        # Pure .NET testing setup
│   └── RequireGodotTestProjectSetup/   # Godot runtime testing setup
└── Tests/              # Fundamental testing patterns and techniques
    ├── AssertionExamples/              # Common assertion patterns
    ├── TestAttributes/                 # Test lifecycle and attributes
    ├── SimpleGodotNodeTest/            # Basic Node testing
    └── MemoryManagement/               # Godot object cleanup patterns
```

## What Each Section Covers

### Setup/
Learn how to configure different types of test projects:
- **MinimalTestProjectSetup**: Testing pure .NET code without Godot dependencies
- **RequireGodotTestProjectSetup**: Testing Godot-specific classes and functionality
- Environment setup and configuration differences
- Project file structure and package requirements

### Tests/
Master fundamental testing concepts and patterns:
- **Assertion Examples**: Using AssertThat() with different data types
- **Test Attributes**: Understanding [TestSuite], [TestCase], lifecycle methods
- **Simple Godot Node Test**: Basic testing of Godot objects and types
- **Memory Management**: Proper cleanup of Godot objects with Free()

## Key Concepts You'll Learn

### Project Configuration
- Difference between .NET-only and Godot runtime testing
- Essential NuGet packages for gdUnit4Net
- Project file setup and commenting
- Environment variable configuration

### Testing Fundamentals
- Writing your first test with [TestCase]
- Using assertions effectively
- Test class organization with [TestSuite]
- Understanding test execution lifecycle

### Godot-Specific Testing
- When to use [RequireGodotRuntime]
- Testing Godot objects (Vector3, Node, GodotObject)
- Memory management with Free() calls
- Environment setup for Godot runtime tests

## Learning Progression

| Step | Focus | What You'll Build |
|------|-------|-------------------|
| **1. Setup** | Project configuration | Working test projects |
| **2. Basic Tests** | Fundamental concepts | Simple assertion tests |
| **3. Godot Tests** | Runtime-specific testing | Tests for Godot objects |
| **4. Patterns** | Common scenarios | Reusable testing patterns |

## When to Use Each Setup

### Use MinimalTestProjectSetup When:
- ✅ Testing pure C# logic and algorithms
- ✅ Testing utility classes and helper methods
- ✅ No Godot-specific types or functionality needed
- ✅ Want fastest test execution

### Use RequireGodotTestProjectSetup When:
- ✅ Testing classes that inherit from Godot types
- ✅ Using Vector3, Transform3D, or other Godot math types
- ✅ Testing Node behavior and scene interactions
- ✅ Need access to Godot's runtime environment

## Common Beginner Questions

**Q: Do I always need the Godot runtime for testing?**
A: No! Use MinimalTestProjectSetup for pure .NET code testing. Only use RequireGodotTestProjectSetup when you need Godot-specific functionality.

**Q: Why are my tests taking so long to run?**
A: Tests with [RequireGodotRuntime] need to start the Godot engine, which takes time. This is normal for Godot-specific tests.

**Q: What's the difference between [TestCase] and [RequireGodotRuntime]?**
A: [TestCase] marks a method as a test. [RequireGodotRuntime] tells gdUnit4Net this test needs the Godot engine to run.

**Q: Do I need to call Free() on all Godot objects?**
A: Yes! Always call Free() on Godot objects in tests to prevent memory leaks.

## Test Execution Overview

### Pure .NET Tests (MinimalTestProjectSetup)
```
Build → Run Tests → Results
~2-5 seconds total
```

### Godot Runtime Tests (RequireGodotTestProjectSetup)
```
Build → Start Godot → Initialize Runtime → Run Tests → Results
~30+ seconds first run, ~10-15 seconds subsequent runs
```

## Essential Testing Patterns

### Basic Assertion
```csharp
[TestCase]
public void BasicAssertion()
{
    AssertThat("Hello").IsEqual("Hello");
}
```

### Godot Object Testing
```csharp
[TestCase]
[RequireGodotRuntime]
public void GodotObjectTest()
{
    var node = new Node();
    AssertThat(node).IsNotNull();
    node.Free(); // Always cleanup!
}
```

### Test Class Structure
```csharp
[TestSuite]
public class MyTestClass
{
    [TestCase]
    public void MyTest() { /* test code */ }
}
```

## Troubleshooting Common Issues

**"No test is available" Error**
- Check if GODOT_BIN environment variable is set (for Godot runtime tests)
- Verify test methods have [TestCase] attribute
- Ensure test class has [TestSuite] attribute

**Build Errors After Project Conversion**
- Delete obj/ and bin/ directories
- Run `dotnet build` again

## Next Steps

After mastering the basics:
- Explore [Advanced examples](../Advanced/) for professional configurations
- Learn about complex testing scenarios and patterns
- Practice with your own Godot projects
- Contribute additional basic examples

## Contributing Basic Examples

Found something confusing or missing? We welcome contributions! Consider adding examples for:
- Additional assertion types
- Common testing scenarios
- Beginner-friendly explanations
- Step-by-step tutorials
