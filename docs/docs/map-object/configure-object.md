---
sidebar_position: 2
---

# Configuring an Object

To configure an object, you shold use ` ObjectMapper ` and ` WriteProperty ` proprties to map string layout.

```csharp
[ObjectMapper(lenght: 50)]
public struct Nothing
{
  public Nothing(string name, string adress)
  {
    this.Name = name;
    this.Adress = adress;
  }
  
  [WriteProperty(lenght: 20, order: 1, paddingChar: ' ')]
  public string Name { get; private set; }
  
  [WriteProperty(lenght: 30, order: 2, paddingChar: ' ')]
  public string Adress { get; private set; }
}
```
