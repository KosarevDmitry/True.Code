using Microsoft.AspNetCore.Http.Features;
using Swashbuckle.AspNetCore.Annotations; //additional nuget package
using True.Code.ToDoListAPI.Helper;

namespace True.Code.ToDoListAPI.Extensions;

public static class ProducesResponseExtension
{
    public static IApplicationBuilder UseProducesOrSwaggerResponseCheck(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SwaggerResponseCheck>();
    }
}

public class SwaggerResponseCheck
{
    private readonly RequestDelegate _next;

    public SwaggerResponseCheck(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        using (var buffer = new MemoryStream())
        {
            var stream = context.Response.Body;
            context.Response.Body = buffer;
            await _next(context);

            var metadata = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;
            IEnumerable<ProducesResponseTypeAttribute>? attributes = null;
            if (metadata != null)
            {
                attributes = metadata.OfType<SwaggerResponseAttribute>()
                    .Union(metadata.OfType<ProducesResponseTypeAttribute>());
            }

            //  affect only for Action with relevant attributes
            if (attributes != null)
            {
                var list = attributes.ToList();
                if (list.Count() != 0 && list.Any(a => a.StatusCode == context.Response.StatusCode))
                {
                    context.Response.StatusCode = 500;
                    buffer.Seek(0, SeekOrigin.Begin);
                    await context.Response.WriteAsync(
                        $"OpenAPI specification exception, unsupported status code: {context.Response.StatusCode}\npath: {context.Request.Path}");
                }
            }
            
            buffer.Seek(0, SeekOrigin.Begin);
            await context.Response.Body.CopyToAsync(stream);
            context.Response.Body = stream;
        }
    }
}