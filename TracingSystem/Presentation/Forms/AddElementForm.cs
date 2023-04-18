using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OriginalCircuit.AltiumSharp;
using OriginalCircuit.AltiumSharp.Drawing;
using OriginalCircuit.AltiumSharp.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracingSystem.Application.Projects.Queries.GetProjectByName;
using TracingSystem.Application.Services;

namespace TracingSystem.Presentation.Forms
{
    public partial class AddElementForm : Form
    {
        private readonly IMediator Mediator;

        private readonly IProjectDataService _project;

        public string SelectedComponentName { get; private set; }

        public int SelectedComponentCount { get; private set; }

        public AddElementForm(ICollection<string> elementNames)
        {
            Mediator = Program.ServiceProvider.GetRequiredService<IMediator>();
            _project = Program.ServiceProvider.GetRequiredService<IProjectDataService>();

            InitializeComponent();

            foreach (string name in elementNames)
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

                button.Click += AddElement_Click;

                panel.Controls.Add(button);
            }
        }

        private async void AddElement_Click(object? sender, EventArgs e)
        {
            var elementCountStr = Microsoft.VisualBasic.Interaction.InputBox("Введите количество элементов", "Добавление элемента");
            int.TryParse(elementCountStr, out var elementCount);
            if(elementCount == 0) { MessageBox.Show("Некорректный ввод", "Ошибка!"); return; }
            SelectedComponentName = (sender as Button).Text;
            SelectedComponentCount = elementCount;
            Close();
        }
    }
}
