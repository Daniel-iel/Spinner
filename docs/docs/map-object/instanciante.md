---
sidebar_position: 3
---

# Instanciate

To map you object as string, you need instantiate ``` Spinner ``` passing the object type in T and an instance of the object in the constructor.

```csharp
  Nothing nothing = new Nothing("spinner", "www.spinner.com.br");
  Spinner<Nothing> spinner = new Spinner<Nothing>(nothing);
  string response = spinner.WriteAsString();
```
