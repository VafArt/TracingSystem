using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracingSystem
{
    public static class ProjectData
    {
        public static event Action NameChanged;
        public static event Action StateChanged;
        private static string name;
        public static string Name 
        { 
            get 
            {
                return name;
            }
            set 
            {
                name = value;
                NameChanged();
            } 
        }

        private static State state;
        public static State State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                StateChanged();
            }
        }
    }

    public enum State
    {
        Startup,//только открыть или создать проект
        OpenedProject,//можно добавить проектные данные
        ConfiguredData,//можно настроить алгоритм
        ConfiguredAlgorithm,//можно трассировать
        Traced//можно расслаивать
    }
}
