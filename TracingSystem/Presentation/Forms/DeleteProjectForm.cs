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
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Projects.Commands.DeleteProjectByName;
using TracingSystem.Application.Services;
using TracingSystem.Persistance;

namespace TracingSystem
{
    public partial class DeleteProjectForm : Form
    {
        private readonly ITracingSystemDbContext _dbContext;
        private readonly IMediator Mediator;

        public DeleteProjectForm(List<string> projectNames)
        {
            Mediator = Program.ServiceProvider.GetRequiredService<IMediator>();
            _dbContext = Program.ServiceProvider.GetRequiredService<ITracingSystemDbContext>();

            InitializeComponent();

            foreach (string name in projectNames)
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

                button.Click += DeleteProject_Click;

                panel.Controls.Add(button);
            }
        }

        private async void DeleteProject_Click(object? sender, EventArgs args)
        {
            var projectToDeleteName = (sender as Button).Text;
            var deleteProjectByNameCommand = new DeleteProjectByNameCommand(projectToDeleteName);
            await Mediator.Send(deleteProjectByNameCommand);
            Close();
        }
    }
}
