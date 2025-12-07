namespace Spinner.Interceptors
{
    public interface IInterceptor<T>
    {
        T Parse(string value);
    }
}
