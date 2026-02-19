using Microsoft.AspNetCore.Http;

namespace Blocks.AspNetCore.Extensions
{
    public static class Extensions
    {
        public static string? BaseUrl(this HttpRequest request)
        {
            if (request is null)
                return null;

            var uri = new UriBuilder(request.Scheme,request.Host.Host, request.Host.Port ?? -1);
            if(uri.Uri.IsDefaultPort)
                uri.Port = -1;

            return uri.Uri.AbsoluteUri;
        }
    }
}
