using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracingSystem.Application.Projects.Queries.GetAllProjectNames;
using TracingSystem.Application.Projects.Queries.GetProjectByName;
using TracingSystem.Application.Services;
using TracingSystem.Domain.Shared;

namespace TracingSystem
{
    //форма для открытия проекта
    public partial class OpenProjectForm : Form
    {
        private readonly List<string> _projectNames;

        private readonly IMediator Mediator;

        private readonly IProjectDataService _project;

        public OpenProjectForm(List<string> projectNames)
        {
            Mediator = Program.ServiceProvider.GetRequiredService<IMediator>();
            _project = Program.ServiceProvider.GetRequiredService<IProjectDataService>();

            InitializeComponent();
            _projectNames = projectNames;

            foreach(string name in _projectNames)
            {
                var button = new Button
                {
                    Text = name,
                    Dock = DockStyle.Top,
                    Height = (int)Math.Round(DeviceDpi * 0.417),
                    FlatStyle = FlatStyle.Flat
                };
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI", 16);

                button.Click += OpenProject_Click;

                panel.Controls.Add(button);
            }
        }

        private async void OpenProject_Click(object? sender, EventArgs e)
        {
            var projectName = (sender as Button).Text;
            var getProjectByNameQuery = new GetProjectByNameQuery(projectName);
            var result = await Mediator.Send(getProjectByNameQuery);
            if (result.IsFailure) throw new Exception("Проект не найден в базе данных!");

            _project.ChangeProject(result.Value, result.Value.State);

            Close();
        }
    }
}
