using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Domain;

namespace TracingSystem.Application.Projects.Commands.CreateProject
{
    public sealed record CreateProjectCommand(string Name) : Common.Abstractions.CQRS.ICommand<Project>;
}
