# EFCore 3.1 - DbContext Mocker
Simple Fluent API for mocking DbContexts and DbSets. Allows easy setups for Set&lt;T>, Model, e => e.Prop style accessors.

## Usage
Mocking a database context

```csharp
_mockDbContext = MockDbContextBuilder
    .BuildMockDbContext<ISampleDbContext>();
```

Mocking the result of a Set<T> call:
    
```csharp
MockDbContextBuilder
    .BuildMockDbContext<ISampleDbContext>();
    .AttachMockDbSetToSetMethodCall(MockDbContextBuilder.BuildMockDbSet(_persons));
    
// Usage:
var result = _mockDbContext.Object
    .Set<Person>()
    .Where(p => p.Identification == "123456-1234A")
    .ToList();
```

Mocking the result of a property call:

```csharp
// Mock:
MockDbContextBuilder
    .BuildMockDbContext<ISampleDbContext>();
    .AttachMockDbSetToPropertyCall(MockDbContextBuilder.BuildMockDbSet(_pets), context => context.Pets));
      
// Usage:
var result = _mockDbContext.Object
    .Pets
    .Where(p => p.FullName == "Bella");
```

Mocking the result of a Model["key"] call:

```csharp
// Mock:
MockDbContextBuilder
    .BuildMockDbContext<ISampleDbContext>();
    .AttachMockDbSetToModelCall(MockDbContextBuilder.BuildMockDbSet(_hobbies), "Hobbies");
    
// Usage:
var result = _mockDbContext.Object
    .Model["Hobbies"];
```


    
