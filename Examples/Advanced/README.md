# Advanced Examples

Welcome to the advanced gdUnit4Net examples! This section covers sophisticated testing techniques, professional configurations, and complex scenarios you'll encounter in real-world Godot projects.

## Prerequisites

Before diving into advanced examples, ensure you understand:
- Basic gdUnit4Net concepts from [Basics](../Basics/README.md)
- Test project setup and configuration
- Environment variables vs configuration files
- Basic Godot object lifecycle and memory management

## Getting Started with Advanced Topics

**Ready for more complex testing?** Explore in this order:
1. [Global Test Settings](Setup/TestWithRunsettings/README.md) - Professional test configuration using .runsettings files
2. [Compile-time Analysis](Setup/TestWithAnalyzers/README.md) - Static analysis and error prevention with gdUnit4.analyzers
3. [Complex Test Scenarios](Tests/) - Advanced testing patterns and techniques

## Advanced Topics Structure

The advanced examples are organized into:

```
├── Setup/              # Advanced project configurations
│   ├── TestWithRunsettings/    # Professional test settings configuration
│   └── TestWithAnalyzers/      # Compile-time validation and code analysis 
└── Tests/              # Complex testing scenarios and patterns
    ├── SignalTesting/          # Testing Godot signals and events
    ├── SceneManagement/        # Loading and testing scenes
    ├── AsyncTesting/           # Testing async operations
    ├── MockingAndStubs/        # Test doubles and dependency injection
    └── PerformanceTesting/     # Benchmarking and performance validation
```

## What Each Section Covers

### Setup/
Professional project configurations for production-ready test environments:

- **TestWithRunsettings**: Centralized configuration using .runsettings files
    - Alternative to environment variables for team and CI/CD scenarios
    - Multiple output formats and advanced logging
    - Environment-specific configurations
    - Better IDE integration and consistent team settings

- **TestWithAnalyzers**: Compile-time validation with gdUnit4.analyzers
    - Static analysis to catch common testing mistakes at compile time
    - Prevents runtime errors by detecting missing [RequireGodotRuntime] attributes
    - IDE integration with real-time feedback and quick fixes
    - Enforces gdUnit4Net best practices automatically
    - Improves developer productivity and code quality

### Tests/
Advanced testing patterns and complex scenarios:
- **Signal Testing**: Verifying Godot signal emissions and connections
- **Scene Management**: Loading, instantiating, and testing complex scenes
- **Async Testing**: Handling coroutines, timers, and async operations
- **Mocking and Stubs**: Using test doubles for isolated unit testing
- **Performance Testing**: Benchmarking and validating performance requirements

## Key Concepts You'll Learn

### Configuration Management
- Using .runsettings for team consistency and CI/CD integration
- Environment-specific test configurations
- Advanced logging and reporting options
- Static analysis integration for error prevention

### Code Quality and Analysis
- Compile-time validation of test code
- Preventing common gdUnit4Net mistakes before runtime
- IDE integration for immediate feedback
- Enforcing coding standards and best practices

### Testing Strategies
- Isolation techniques for complex dependencies
- Testing interactions between multiple systems
- Handling Godot-specific async patterns
- Performance and load testing approaches

### Production Readiness
- Test organization for large codebases
- Continuous integration best practices
- Test reporting and documentation
- Debugging complex test failures

## Complexity Progression

| Level | Focus | Examples |
|-------|-------|----------|
| **Basics** | Learning fundamentals | Simple assertions, basic setup |
| **Advanced** | Professional techniques | Configuration management, static analysis, complex scenarios |

## When to Use Advanced Techniques

### Use Advanced Configuration When:
- ✅ Working in a team environment
- ✅ Setting up CI/CD pipelines
- ✅ Need multiple test output formats
- ✅ Managing different environments (dev, staging, prod)
- ✅ Want consistent configuration across team members

### Use Static Analysis When:
- ✅ Teams wanting to enforce gdUnit4Net best practices automatically
- ✅ Projects requiring high test reliability and quality
- ✅ Developers who want immediate feedback on testing mistakes
- ✅ Codebases with many contributors to maintain consistency
- ✅ CI/CD pipelines that should fail fast on test configuration errors

### Use Advanced Testing When:
- ✅ Testing complex interactions between systems
- ✅ Need to isolate dependencies
- ✅ Testing performance-critical code
- ✅ Working with large, complex scenes

## Setup Examples Comparison

| Aspect | TestWithRunsettings | TestWithAnalyzers |
|--------|-------------------|-------------------|
| **Primary Focus** | Test execution configuration | Code quality and error prevention |
| **Configuration Method** | .runsettings files | gdUnit4.analyzers package |
| **Team Benefits** | Consistent test settings | Enforced coding standards |
| **CI/CD Integration** | Multiple output formats | Early error detection |
| **Developer Experience** | Professional test reports | Real-time feedback in IDE |
| **Error Prevention** | Runtime configuration issues | Compile-time validation |

## Next Steps

After mastering advanced techniques:
- Apply learned concepts to your own projects
- Combine .runsettings configuration with analyzer validation
- Contribute your own advanced examples
- Explore integration with external testing tools
- Share your testing strategies with the community

## Contributing Advanced Examples

Have an advanced testing scenario not covered here? We welcome contributions! Consider adding examples for:
- Custom test adapters
- Integration with external tools
- Advanced CI/CD configurations
- Performance profiling techniques
- Custom analyzer rules
