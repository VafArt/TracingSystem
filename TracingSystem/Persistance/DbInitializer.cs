using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Domain;

namespace TracingSystem.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(TracingSystemDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var project = new Project()
            {
                Name = "Test project",
                PcbLib = new byte[] { 123, 128, 129, 255, 255, 255 },
                Pcbs = new List<Pcb> 
                { 
                    new Pcb()
                    {
                        Layers= new List<Layer>()
                        {
                            new Layer()
                            {
                                Traces = new List<Trace>()
                                {
                                    new Trace()
                                    {
                                        Length = 15,
                                        DirectionChangingCoords = new List<TracePoint>()
                                        {
                                            new TracePoint()
                                            {
                                                X = 0,
                                                Y = 0,
                                            },
                                            new TracePoint()
                                            {
                                                X = 15,
                                                Y = 0
                                            }
                                        }
                                    }
                                },
                                Elements = new List<Element>()
                                {
                                    new Element()
                                    {
                                        Name = "Test Element",
                                        Location = new ElementPoint()
                                        {
                                            X = 35,
                                            Y= 25
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            };
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();
        }
    }
}
