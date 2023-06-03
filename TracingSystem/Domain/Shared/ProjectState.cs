using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracingSystem.Domain.Shared
{
    public enum ProjectState
    {
        Startup,//только открыть или создать проект
        OpenedProject,//можно добавить проектные данные и выбрать плату
        //PcbSelected,//можно добавлять/удалять элементы/трассы
        ConfiguredData,//можно настроить алгоритм
        ConfiguredAlgorithm,//можно трассировать
        Traced,//можно расслаивать
        Bundled
    }
}
