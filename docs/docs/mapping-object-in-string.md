---
sidebar_position: 2
---

# Write Object

## Mapping object in string

To configure an object, you should use ` ObjectMapper ` and ` WriteProperty ` properties to map string layout.

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

:::info

The sum `length` of all mapped property should not be more than ObjectMapper `length`.

:::

## Instanciate

To map you object as string, you need instantiate ``` Spinner ``` passing the object type in T and an instance of the object in the constructor.

```csharp
  Nothing nothing = new Nothing("spinner", "www.spinner.com.br");
  Spinner<Nothing> spinner = new Spinner<Nothing>(nothing);  
```

## Write an Object

After configured the object, you can call the ` WriteAsString ` method to write the object in a string format.

```csharp
  string stringResponse = spinner.WriteAsString();
```
