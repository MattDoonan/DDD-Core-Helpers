# DDD.Core

Domain Driven Design Core is a lightweight helper library for implementing **Domain-Driven Design (DDD)** in .NET applications.  
It provides abstract base classes for common building blocks such as **Value Objects** and **Entities**, along with a simple **Result** type to standardize success/failure handling.

---

## ✨ Features
- 🔹 Abstract **Entity** base class with identity handling.
- 🔹 Abstract **ValueObject** base record class with equality and immutability support.
- 🔹 Abstract **Event** base record class with equality and immutability support.
- 🔹 **Result** class for clean success/failure operations without exceptions.
- 🔹 Designed for **.NET 10.0** and modern C#.

---

## ✨ Result Types

The package introduces different **Results** to represent the outcome of operations, allowing for clear handling of success, failure, and error propagation. Results are designed to be **hierarchically convertible**, meaning lower-level results can be implicitly converted into higher-level results while preserving their data.

There are two main categories of results:

- **Non-typed results:** These contain general information about the operation, including:
    - Success status
    - Failure types (if any)
    - Error messages
    - The layer in which the failure occurred (e.g., Service layer)

- **Typed results:** These inherit all functionality from non-typed results, in addition to:
  - the result's output type
  - The result's output (will throw error if the result is a failure)

### Hierarchical Conversion

Results can be converted up the hierarchy. For instance, an **EntityResult** can be implicitly converted to a **ServiceResult**. This conversion retains all original values, and if the target result type is associated with a specific layer, the conversion will automatically assign the layer information.

### Result Hierarchy

From lowest to highest in the hierarchy:

- 🔹 **ValueObjectResult:** Used for creating value objects.
- 🔹 **EntityResult:** Used within entity classes for domain-specific operations
- 🔹 **MapperResult:** Used for object mapping operations.
- 🔹 **InfraResult:** Used in the infrastructure layer for non-repository infrastructure operations.
- 🔹 **RepoResult:** Used in the infrastructure layer for repository operations.
- 🔹 **ServiceResult:** Used in the service layer for domain-specific applications.
- 🔹 **UseCaseResult:** Used in the use case layer for application-specific operations.
- 🔹 **Result:** A generic result type that can be used anywhere in the application.


## 📦 Installation

```powershell
dotnet add package DDD.Core.Components
