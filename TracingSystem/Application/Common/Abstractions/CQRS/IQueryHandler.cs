using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Common.Abstractions.CQRS
{
    public interface IQueryHandler<TQuery,TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
