using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FlurlApiClientExample.StoreApi;

public static class UserAgentGenerator
{
    private const string DefaultProductName = "MyProductName";

    public static string Generate()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var product = new ProductInfoHeaderValue(assembly.GetName().Name ?? DefaultProductName, assembly.GetName().Version?.ToString());

        var dotnetVersion = Environment.Version.ToString();
        var osVersion = RuntimeInformation.OSDescription;
        var osArchitecture = RuntimeInformation.OSArchitecture.ToString();

        var comment = new ProductInfoHeaderValue($"(API Client; .NET {dotnetVersion}; {osVersion}; {osArchitecture} +https://myapp.com)");

        return $"{product} {comment}";
    }
}