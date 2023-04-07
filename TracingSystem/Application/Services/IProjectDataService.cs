using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Services
{
    public interface IProjectDataService
    {
        public Project Project { get; set; }

        public string Name { get; set; }

        public ProjectState State { get; set; }

        public event Action NameChanged;

        public event Action StateChanged;


    }
}
