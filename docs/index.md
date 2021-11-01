# Spinner

### About
  Spinner is a simple object mapper, it's useful to communicate to any system that use positional string.

### Get Started

```csharp
  [ContextProperty(lenght: 50)]
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
    
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>(nothing);
 var stringResponse = spinner.WriteAsString();   
 // stringresponse = "              spinner            www.spinner.com.br   "
        
```

### Support

Having trouble with spinner? let we know opening an [issue](https://github.com/SpinnerAlloc/Spinner/issues)
