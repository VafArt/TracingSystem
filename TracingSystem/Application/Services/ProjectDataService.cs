using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Services
{
    public class ProjectDataService : IProjectDataService
    {
        public Project? Project { get; private set; }

        public event Action? NameChanged;

        public event Action? StateChanged;

        public event Action? ProjectChanged;

        public event Action? SelectedElementChanged;

        private Element selectedElement;
        public Element SelectedElement 
        { 
            get
            {
                return selectedElement;
            }
            set
            {
                selectedElement = value;
                if(SelectedElementChanged != null)
                    SelectedElementChanged();
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
                if (NameChanged != null)
                    NameChanged();
            }
        }

        private ProjectState state;
        public ProjectState State
        {
            get
            {
                return state;
            }
            private set
            {
                state = value;
                if (StateChanged != null)
                    StateChanged();
            }
        }

        public void ChangeProject(Project project, ProjectState state)
        {
            Project = project;
            Name = project?.Name;
            State = state;
            if (ProjectChanged != null)
                ProjectChanged();
        }

        public void PerformProjectChangeAction()
        {
            if (ProjectChanged != null)
                ProjectChanged();
        }

        public void ChangeProjectName(string name)
        {
            Project.Name = name;
            Name = name;
        }
    }
}
