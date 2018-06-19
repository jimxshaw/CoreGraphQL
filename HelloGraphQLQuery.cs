using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projects
{
  public class HelloGraphQLQuery : ObjectGraphType
  {
    public HelloGraphQLQuery()
    {
      Field<StringGraphType>(
        name: "hello",
        resolve: context => "graphql"
      );

      Field<StringGraphType>(
        name: "what",
        resolve: context => "is this?"
      );
    }
  }
}
