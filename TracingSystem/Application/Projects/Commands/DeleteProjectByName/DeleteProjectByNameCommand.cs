using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions.CQRS;

namespace TracingSystem.Application.Projects.Commands.DeleteProjectByName
{
    public sealed record DeleteProjectByNameCommand(string ProjectName) : ICommand;
}
