# DenevCloud.Extensions.Types
A set of type extensions for .NET Core
## Usage for DenevCloud.Extensions.Types
```csharp

using DenevCloud.Extensions.Types;

namespace DenevCloud;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var x = 60;

        var _result = GetResult(x);

        if (_result)
        {
            Console.WriteLine($"The value is: {_result.Unwrap()}");
        }
        else
        {
            Console.WriteLine("There is no value.");
        }

        if(_result == Option.Some)
        {
            Console.WriteLine($"The value is: {await _result.UnwrapAsync(true)}");
        }
        else
        {
            Console.WriteLine("There is no value.");
        }
        
    }

    public static OptionResult<int> GetResult(int x)
    {
        if (x < 50)
        {
            return new Some<int>(x);
        }
        else
        {
            return new None<int>();
        }
    }
}

```