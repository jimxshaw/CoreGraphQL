using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projects
{
  public class HelloGraphQLSchema : Schema
  {
    public HelloGraphQLSchema(HelloGraphQLQuery query)
    {
      Query = query;
    }
  }
}
