using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions.CQRS;

namespace TracingSystem.Application.Projects.Queries.GetAllProjectNames
{
    public sealed record GetAllProjectNamesQuery() : IQuery<List<string>>;
}
