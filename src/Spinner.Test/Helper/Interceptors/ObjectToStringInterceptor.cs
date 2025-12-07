using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    /// <summary>
    /// Interceptor que retorna um objeto StringBuilder para testar o cenário value.ToString()
    /// </summary>
    internal sealed class ObjectToStringInterceptor : IInterceptor<object>
    {
        public object Parse(string obj)
        {
            // Retorna um objeto que não é string, forçando o uso de ToString()
            return new System.Text.StringBuilder(obj.Trim());
        }
    }
}
