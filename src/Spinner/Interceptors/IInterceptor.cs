namespace Spinner.Interceptors
{
    public interface IInterceptor
    {
        object Parse(string value);
    }

    public interface IInterceptor<T> : IInterceptor
    {
        new T Parse(string value);

        object IInterceptor.Parse(string value) => Parse(value);
    }
}
