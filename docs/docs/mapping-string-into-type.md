---
sidebar_position: 4
---

# Type Parser

If you require extracting positional strings and converting the values into their final forms, such as obtaining a string representing a datetime or currency, you can create an interceptor reader by implementing the `ITypeParser` interface. This allows you to perform custom actions before the value is set to the final property.

here and example of intercept the property read.

```csharp

    public class ParserWebSite : ITypeParser
    {
        public object Parser(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
        
    [ReadProperty(start: 9, length: 20, type: typeof(ParserWebSite))]
    
```