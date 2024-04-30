---
sidebar_position: 4
---

# Interceptor

If you require extracting positional strings and converting the values into their final forms, such as obtaining a string representing a datetime or decimal currency format, you can create an interceptor reader by implementing the `IInterceptor` interface. This allows you to perform custom actions before the value is set to the final property.

here and example of intercept the property read.

```csharp

    public class WebSiteInterceptor : IInterceptor
    {
        public object Parse(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
        
    [ReadProperty(start: 9, length: 20, type: typeof(WebSiteInterceptor))]
    
```