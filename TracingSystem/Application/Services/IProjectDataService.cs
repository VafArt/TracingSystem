using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Algorithms;
using TracingSystem.Application.Controls;
using TracingSystem.Domain;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Services
{
    public interface IProjectDataService
    {
        public Project? Project { get; }

        public ObjectiveFunction ObjectiveFunction { get; set; }

        public TracePriority TracePriority { get; set; }

        public string Name { get; }

        public ProjectState State { get; }

        public Element? SelectedElement { get; set; }

        public Dictionary<int, List<Trace>>? SelectedBundle { get; set; }

        public Pad? SelectedPad { get; set; }

        public event Action SelectedPadChanged;

        public event Action NameChanged;

        public event Action StateChanged;

        public event Action ProjectChanged;

        public event Action SelectedElementChanged;

        public void ChangeProject(Project project, ProjectState state);
        public void PerformProjectChangeAction();

        public void ChangeProjectName(string name);
    }
}
