using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projects.Middleware
{
  public class GraphQLMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IDocumentWriter _writer;
    private readonly IDocumentExecuter _executer;
    private readonly ISchema _schema;

    public GraphQLMiddleware(RequestDelegate next,
                              IDocumentWriter writer,
                              IDocumentExecuter executer,
                              ISchema schema)
    {
      _next = next;
      _writer = writer;
      _executer = executer;
      _schema = schema;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (httpContext.Request.Path.StartsWithSegments("/api/graphql")
          && string.Equals(httpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
      {
        string body;

        using (var streamReader = new StreamReader(httpContext.Request.Body))
        {
          body = await streamReader.ReadToEndAsync();

          GraphQLRequest request = JsonConvert.DeserializeObject<GraphQLRequest>(body);

          ExecutionResult result = await _executer.ExecuteAsync(doc =>
          {
            doc.Schema = _schema;
            doc.Query = request.Query;
          }).ConfigureAwait(false);

          string json = _writer.Write(result);

          await httpContext.Response.WriteAsync(json);
        }
      }
      else
      {
        await _next(httpContext);
      }
    }
  }
}
