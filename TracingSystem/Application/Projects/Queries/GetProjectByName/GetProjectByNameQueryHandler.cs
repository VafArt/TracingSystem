using Microsoft.EntityFrameworkCore;
using OriginalCircuit.AltiumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Application.Controls;
using TracingSystem.Domain;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;
using ImageConverter = TracingSystem.Domain.Shared.ImageConverter;

namespace TracingSystem.Application.Projects.Queries.GetProjectByName
{
    public sealed class GetProjectByNameQueryHandler : IQueryHandler<GetProjectByNameQuery, Project>
    {
        private readonly ITracingSystemDbContext _dbContext;
        private readonly PcbLibReader _pcbLibReader;

        public GetProjectByNameQueryHandler(ITracingSystemDbContext dbContext, PcbLibReader pcbLibReader)
        {
            _dbContext = dbContext;
            _pcbLibReader = pcbLibReader;
        }

        public async Task<Result<Project>> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                .Include(project => project.Pcbs)
                .ThenInclude(pcb => pcb.Layers)
                .ThenInclude(layer => layer.Elements)
                .ThenInclude(element => element.Pads)
                .Include(project => project.Pcbs)
                .ThenInclude(pcb => pcb.Layers)
                .ThenInclude(layer => layer.Traces)
                .ThenInclude(trace => trace.DirectionChangingCoords)
                .FirstOrDefaultAsync(project => project.Name == request.Name);
            if (project == null) return Result.Failure(project, DomainErrors.Project.ProjectNotFound);
            if (project.PcbLib?.Length != 0 && project.PcbLib?.Length is not null)
            {
                var mem = new MemoryStream(project.PcbLib);
                var components = _pcbLibReader.Read(mem).Items;
                var elements = project.Pcbs
                    .SelectMany(pcb => pcb.Layers.SelectMany(layer => layer.Elements));
                foreach(var element in elements)
                {
                    var image = ImageConverter.ByteArrayToImage(element.Image);
                    element.ElementControl = new ElementControl
                    {
                        Image = image,
                        Width = image.Width,
                        Height = image.Height,
                        PcbComponent = components.FirstOrDefault(component => component.Pattern == element.Name),
                        Element = element,
                        Location = new Point(element.LocationX, element.LocationY),
                    };
                }
                mem.Dispose();
            }

            return project;
        }
    }
}
