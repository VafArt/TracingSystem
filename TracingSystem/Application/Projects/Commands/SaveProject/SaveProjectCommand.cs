using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Domain;

namespace TracingSystem.Application.Projects.Commands.SaveProject
{
    public sealed record SaveProjectCommand(Project Project) : ICommand;
}
