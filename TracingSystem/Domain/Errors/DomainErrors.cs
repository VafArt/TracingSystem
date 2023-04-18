using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Project
        {
            public static readonly Error AlreadyHas = new Error(
                "Project.AlreadyHas",
                "Проект с таким названием уже существует!");

            public static readonly Error ProjectTableIsEmpty = new Error(
                "Project.ProjectTableIsEmpty",
                "Нет ни одного проекта!");

            public static readonly Error ProjectNotFound = new Error(
                "Project.NotFound",
                "Проект не найден!");
        }

        public static class Pcb
        {
            public static readonly Error PcbNotFound = new Error(
                "Pcb.NotFound",
                "Плата не найдена!");
        }
    }
}
