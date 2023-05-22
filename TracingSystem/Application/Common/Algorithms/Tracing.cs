using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracingSystem.Application.Common.Algorithms
{
    public enum TracingOptions
    {
        MinimalDistance,
        MinimalLayerCount,
    }
    public class Tracing
    {
        //public int[,] Layer { get; set; }

        public TracingOptions Options { get; set; }

        public Tracing(TracingOptions options)
        {
            Options = options;
        }

        public int[,] FindWay(Graph graph, int[,] layer)
        {
            var result = new List<List<Tuple<int, int>>>(); // все маршруты

            var distances = new List<Tuple<double, List<Point>>>();
            var matrixTraced = (int[,])layer.Clone(); // создаем копию матрицы слоя с проведенными трассами
            for (int i = 0; i < graph.Nodes.Length; i++)
            {
                var distance = Math.Sqrt(Math.Pow(graph.Nodes[i].Coordinates.First().X - graph.Nodes[i].Coordinates.Last().X, 2) + Math.Pow(graph.Nodes[i].Coordinates.First().Y - graph.Nodes[i].Coordinates.Last().Y, 2));
                distances.Add(new Tuple<double, List<Point>>(distance, graph.Nodes[i].Coordinates));
            }

            distances = distances.OrderByDescending((distances) => distances.Item1).ToList();

            //for (int i = 0; i < graph.Nodes.Length; i++)
            //    graph[i].Coordinates = distances[i].Item2;

            var matrix = (int[,])layer.Clone(); // создаем копию матрицы слоя
            for (int i = 0; i < graph.Nodes.Length; i++) // выполняем все что внутри для каждой вершины
            {
                var startMatrixX = graph.Nodes[i].Coordinates.First().X / 20;
                var startMatrixY = graph.Nodes[i].Coordinates.First().Y / 20;
                var endMatrixX = graph.Nodes[i].Coordinates.Last().X / 20;
                var endMatrixY = graph.Nodes[i].Coordinates.Last().Y / 20;
                if (Options == TracingOptions.MinimalDistance)
                {
                    matrix = (int[,])layer.Clone(); // создаем копию матрицы слоя
                    if (startMatrixY - 1 >= 0 && (matrix[startMatrixY - 1, startMatrixX] > -2 || matrix[startMatrixY - 1, startMatrixX] == matrix[startMatrixY, startMatrixX])) // если клетка сверху от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY - 1, startMatrixX] = 1; // присваеимваем этой клетки единицу

                    }
                    if (startMatrixY + 1 < matrix.GetLength(1) && (matrix[startMatrixY + 1, startMatrixX] > -2 || matrix[startMatrixY + 1, startMatrixX] == matrix[startMatrixY, startMatrixX])) // если клетка снизу от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY + 1, startMatrixX] = 1; // присваеимваем этой клетки единицу
                    }
                    if (startMatrixX - 1 >= 0 && (matrix[startMatrixY, startMatrixX - 1] > -2 || matrix[startMatrixY, startMatrixX - 1] == matrix[startMatrixY, startMatrixX])) // если клетка слева от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY, startMatrixX - 1] = 1; // присваеимваем этой клетки единицу
                    }
                    if (startMatrixX + 1 < matrix.GetLength(0) && (matrix[startMatrixY, startMatrixX + 1] > -2 || matrix[startMatrixY, startMatrixX + 1] == matrix[startMatrixY, startMatrixX])) // если клетка справа от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY, startMatrixX + 1] = 1; // присваеимваем этой клетки единицу
                    }

                    int counter = 1; // счетчик
                    while (true)
                    {
                        for (int k = 0; k < matrix.GetLength(0); k++) // проходимся по матрице
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                if (matrix[k, j] == counter) // ищем клетки которые равны счетчику (первоначально единице)
                                {
                                    if (k - 1 >= 0 && (matrix[k - 1, j] == 0 || k - 1 == endMatrixY && j == endMatrixX)) // если клетка сверху от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k - 1, j] = counter + 1; // присваиваем ей счетчик + 1
                                    }
                                    if (k + 1 < matrix.GetLength(0) && (matrix[k + 1, j] == 0 || k + 1 == endMatrixY && j == endMatrixX)) // если клетка снизу от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k + 1, j] = counter + 1; // присваиваем ей счетчик + 1
                                    }
                                    if (j - 1 >= 0 && (matrix[k, j - 1] == 0 || k == endMatrixY && j - 1 == endMatrixX)) // если клетка слева от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k, j - 1] = counter + 1; // присваиваем ей счетчик + 1
                                    }
                                    if (j + 1 < matrix.GetLength(1) && (matrix[k, j + 1] == 0 || k == endMatrixY && j + 1 == endMatrixX)) // если клетка справа от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k, j + 1] = counter + 1; // присваиваем ей счетчик + 1
                                    }

                                }
                            }
                        }
                        counter++; // увеличиваем счетчик
                        if (matrix[endMatrixY, endMatrixX] != matrix[startMatrixY, startMatrixX]) // вечный цикл до тех пор пока не дойдем до начальной или конечной клетки
                            break;
                    }

                    var current = (endMatrixY, endMatrixX); // в текущую клетку записываем клетку конца
                    result.Add(new List<Tuple<int, int>>()); // создаем лист куда будем записывать маршрут
                    result[i].Add(new Tuple<int, int>(endMatrixY, endMatrixX));
                    matrixTraced[endMatrixY, endMatrixX] = -1;
                    while (true) // опять вечный цикл
                    {
                        if (current.Item2 - 1 >= 0 && (matrix[current.Item1, current.Item2 - 1] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1, current.Item2 - 1] == matrix[startMatrixY, startMatrixX])) // если клетка слева существует и ее цифра на 1 меньше чем цифра в текущей клетке
                        {
                            current.Item2--; // текущая клетка становится этой клеткой
                            result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                            graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            if (matrixTraced[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                for (int j = 0; j < graph.Nodes.Length; j++)
                                    for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                        if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                            if (i != j)
                                                graph.Connect(i, j);
                            matrixTraced[current.Item1, current.Item2] = -1;
                        }
                        else if (current.Item2 + 1 < matrix.GetLength(1) && (matrix[current.Item1, current.Item2 + 1] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1, current.Item2 + 1] == matrix[startMatrixY, startMatrixX])) // если клетка справа существует и ее цифра на 1 меньше чем цифра в текущей клетке
                        {
                            current.Item2++; // текущая клетка становится этой клеткой
                            result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                            graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            if (matrixTraced[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                for (int j = 0; j < graph.Nodes.Length; j++)
                                    for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                        if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                            if (i != j)
                                                graph.Connect(i, j);
                            matrixTraced[current.Item1, current.Item2] = -1;
                        }
                        else if (current.Item1 - 1 >= 0 && (matrix[current.Item1 - 1, current.Item2] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1 - 1, current.Item2] == matrix[startMatrixY, startMatrixX])) // если клетка сверху существует и ее цифра на 1 меньше чем цифра в текущей клетке
                        {
                            current.Item1--; // текущая клетка становится этой клеткой
                            result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                            graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            if (matrixTraced[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                for (int j = 0; j < graph.Nodes.Length; j++)
                                    for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                        if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                            if (i != j)
                                                graph.Connect(i, j);
                            matrixTraced[current.Item1, current.Item2] = -1;
                        }
                        else if (current.Item1 + 1 < matrix.GetLength(0) && (matrix[current.Item1 + 1, current.Item2] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1 + 1, current.Item2] == matrix[startMatrixY, startMatrixX])) // если клетка снизу существует и ее цифра на 1 меньше чем цифра в текущей клетке
                        {
                            current.Item1++; // текущая клетка становится этой клеткой
                            result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                            graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            if (matrixTraced[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                for (int j = 0; j < graph.Nodes.Length; j++)
                                    for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                        if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                            if (i != j)
                                                graph.Connect(i, j);
                            matrixTraced[current.Item1, current.Item2] = -1;
                        }
                        if (current.Item1 == startMatrixY && current.Item2 == startMatrixX) break; // вечный цикл до тех пор пока не дойдем до начальной клетки
                    }
                    graph.Nodes[i].Coordinates.RemoveAt(0);
                    matrix[endMatrixY, endMatrixX] = matrix[startMatrixY, startMatrixX];
                }

                if (Options == TracingOptions.MinimalLayerCount)
                {
                    //может получится что вокруг трассы -1
                    if (startMatrixY - 1 >= 0 && (matrix[startMatrixY - 1, startMatrixX] == 0 || matrix[startMatrixY - 1, startMatrixX] == matrix[startMatrixY, startMatrixX])) // если клетка сверху от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY - 1, startMatrixX] = 1; // присваеимваем этой клетки единицу
                    }
                    if (startMatrixY + 1 < matrix.GetLength(1) && (matrix[startMatrixY + 1, startMatrixX] == 0 || matrix[startMatrixY + 1, startMatrixX] == matrix[startMatrixY, startMatrixX])) // если клетка снизу от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY + 1, startMatrixX] = 1; // присваеимваем этой клетки единицу
                    }
                    if (startMatrixX - 1 >= 0 && (matrix[startMatrixY, startMatrixX - 1] == 0 || matrix[startMatrixY, startMatrixX - 1] == matrix[startMatrixY, startMatrixX])) // если клетка слева от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY, startMatrixX - 1] = 1; // присваеимваем этой клетки единицу
                    }
                    if (startMatrixX + 1 < matrix.GetLength(0) && (matrix[startMatrixY, startMatrixX + 1] == 0 || matrix[startMatrixY, startMatrixX + 1] == matrix[startMatrixY, startMatrixX])) // если клетка справа от начальной клетки существует и равна 0 или значению в начальной клетке
                    {
                        matrix[startMatrixY, startMatrixX + 1] = 1; // присваеимваем этой клетки единицу
                    }

                    int counter = 1; // счетчик
                    var oldMatrix = (int[,])matrix.Clone();
                    while (true)
                    {
                        for (int k = 0; k < matrix.GetLength(0); k++)
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                oldMatrix[k, j] = matrix[k, j];
                            }
                        }

                        for (int k = 0; k < matrix.GetLength(0); k++) // проходимся по матрице
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                if (matrix[k, j] == counter) // ищем клетки которые равны счетчику (первоначально единице)
                                {
                                    if (k - 1 >= 0 && (matrix[k - 1, j] == 0 || k - 1 == endMatrixY && j == endMatrixX)) // если клетка сверху от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k - 1, j] = counter + 1; // присваиваем ей счетчик + 1
                                    }
                                    if (k + 1 < matrix.GetLength(0) && (matrix[k + 1, j] == 0 || k + 1 == endMatrixY && j == endMatrixX)) // если клетка снизу от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k + 1, j] = counter + 1; // присваиваем ей счетчик + 1
                                    }
                                    if (j - 1 >= 0 && (matrix[k, j - 1] == 0 || k == endMatrixY && j - 1 == endMatrixX)) // если клетка слева от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k, j - 1] = counter + 1; // присваиваем ей счетчик + 1
                                    }
                                    if (j + 1 < matrix.GetLength(1) && (matrix[k, j + 1] == 0 || k == endMatrixY && j + 1 == endMatrixX)) // если клетка справа от этой клетки существует и равна 0 или -3 (не стена)
                                    {
                                        matrix[k, j + 1] = counter + 1; // присваиваем ей счетчик + 1
                                    }

                                }
                            }
                        }
                        counter++; // увеличиваем счетчик

                        if (matrix[endMatrixY, endMatrixX] != matrix[startMatrixY, startMatrixX]) // вечный цикл до тех пор пока не дойдем до начальной или конечной клетки
                            break;

                        if (ArrayEquality(oldMatrix, matrix)) // вечный цикл до тех пор пока не дойдем до начальной или конечной клетки
                            break;
                    }

                    if (matrix[endMatrixY, endMatrixX] != matrix[startMatrixY, startMatrixX])
                    {
                        var current = (endMatrixY, endMatrixX); // в текущую клетку записываем клетку конца
                        result.Add(new List<Tuple<int, int>>()); // создаем лист куда будем записывать маршрут
                        result[i].Add(new Tuple<int, int>(endMatrixY, endMatrixX));
                        while (true) // опять вечный цикл
                        {
                            if (current.Item2 - 1 >= 0 && (matrix[current.Item1, current.Item2 - 1] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1, current.Item2 - 1] == matrix[startMatrixY, startMatrixX])) // если клетка слева существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item2--; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            }
                            else if (current.Item2 + 1 < matrix.GetLength(1) && (matrix[current.Item1, current.Item2 + 1] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1, current.Item2 + 1] == matrix[startMatrixY, startMatrixX])) // если клетка справа существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item2++; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            }
                            else if (current.Item1 - 1 >= 0 && (matrix[current.Item1 - 1, current.Item2] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1 - 1, current.Item2] == matrix[startMatrixY, startMatrixX])) // если клетка сверху существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item1--; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            }
                            else if (current.Item1 + 1 < matrix.GetLength(0) && (matrix[current.Item1 + 1, current.Item2] == matrix[current.Item1, current.Item2] - 1 || matrix[current.Item1 + 1, current.Item2] == matrix[startMatrixY, startMatrixX])) // если клетка снизу существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item1++; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                            }
                            if (current.Item1 == startMatrixY && current.Item2 == startMatrixX) break; // вечный цикл до тех пор пока не дойдем до начальной клетки
                        }
                        graph.Nodes[i].Coordinates.RemoveAt(0);
                        foreach (var coordinate in graph.Nodes[i].Coordinates)
                        {
                            matrix[coordinate.Y / 20, coordinate.X / 20] = -1;
                        }

                        for (int k = 0; k < matrix.GetLength(0); k++)
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                if (matrix[k, j] > 0) matrix[k, j] = 0;
                            }
                        }
                        matrix[endMatrixY, endMatrixX] = matrix[startMatrixY, startMatrixX];
                        matrixTraced = matrix;
                    }
                    else
                    {
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            for (int k = 0; k < matrix.GetLength(1); k++)
                            {
                                if (matrix[j, k] > 0) matrix[j, k] = 0;
                                if (layer[j, k] < 0) matrix[j, k] = layer[j, k];
                            }
                        }

                        //как в первом методе только теперь вместо matrixTraced будет matrix а вместо matrix надо создать чистую матрицу
                        var cleanMatrix = (int[,])layer.Clone();


                        if (startMatrixY - 1 >= 0 && (cleanMatrix[startMatrixY - 1, startMatrixX] > -2 || cleanMatrix[startMatrixY - 1, startMatrixX] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка сверху от начальной клетки существует и равна 0 или значению в начальной клетке
                        {
                            cleanMatrix[startMatrixY - 1, startMatrixX] = 1; // присваеимваем этой клетки единицу

                        }
                        if (startMatrixY + 1 < cleanMatrix.GetLength(1) && (cleanMatrix[startMatrixY + 1, startMatrixX] > -2 || cleanMatrix[startMatrixY + 1, startMatrixX] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка снизу от начальной клетки существует и равна 0 или значению в начальной клетке
                        {
                            cleanMatrix[startMatrixY + 1, startMatrixX] = 1; // присваеимваем этой клетки единицу
                        }
                        if (startMatrixX - 1 >= 0 && (cleanMatrix[startMatrixY, startMatrixX - 1] > -2 || cleanMatrix[startMatrixY, startMatrixX - 1] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка слева от начальной клетки существует и равна 0 или значению в начальной клетке
                        {
                            cleanMatrix[startMatrixY, startMatrixX - 1] = 1; // присваеимваем этой клетки единицу
                        }
                        if (startMatrixX + 1 < cleanMatrix.GetLength(0) && (cleanMatrix[startMatrixY, startMatrixX + 1] > -2 || cleanMatrix[startMatrixY, startMatrixX + 1] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка справа от начальной клетки существует и равна 0 или значению в начальной клетке
                        {
                            cleanMatrix[startMatrixY, startMatrixX + 1] = 1; // присваеимваем этой клетки единицу
                        }

                        counter = 1; // счетчик
                        while (true)
                        {
                            for (int k = 0; k < cleanMatrix.GetLength(0); k++) // проходимся по матрице
                            {
                                for (int j = 0; j < cleanMatrix.GetLength(1); j++)
                                {
                                    if (cleanMatrix[k, j] == counter) // ищем клетки которые равны счетчику (первоначально единице)
                                    {
                                        if (k - 1 >= 0 && (cleanMatrix[k - 1, j] == 0 || k - 1 == endMatrixY && j == endMatrixX)) // если клетка сверху от этой клетки существует и равна 0 или -3 (не стена)
                                        {
                                            cleanMatrix[k - 1, j] = counter + 1; // присваиваем ей счетчик + 1
                                        }
                                        if (k + 1 < cleanMatrix.GetLength(0) && (cleanMatrix[k + 1, j] == 0 || k + 1 == endMatrixY && j == endMatrixX)) // если клетка снизу от этой клетки существует и равна 0 или -3 (не стена)
                                        {
                                            cleanMatrix[k + 1, j] = counter + 1; // присваиваем ей счетчик + 1
                                        }
                                        if (j - 1 >= 0 && (cleanMatrix[k, j - 1] == 0 || k == endMatrixY && j - 1 == endMatrixX)) // если клетка слева от этой клетки существует и равна 0 или -3 (не стена)
                                        {
                                            cleanMatrix[k, j - 1] = counter + 1; // присваиваем ей счетчик + 1
                                        }
                                        if (j + 1 < cleanMatrix.GetLength(1) && (cleanMatrix[k, j + 1] == 0 || k == endMatrixY && j + 1 == endMatrixX)) // если клетка справа от этой клетки существует и равна 0 или -3 (не стена)
                                        {
                                            cleanMatrix[k, j + 1] = counter + 1; // присваиваем ей счетчик + 1
                                        }

                                    }
                                }
                            }
                            counter++; // увеличиваем счетчик
                            if (cleanMatrix[endMatrixY, endMatrixX] != cleanMatrix[startMatrixY, startMatrixX]) // вечный цикл до тех пор пока не дойдем до начальной или конечной клетки
                                break;
                        }

                        var current = (endMatrixY, endMatrixX); // в текущую клетку записываем клетку конца
                        result.Add(new List<Tuple<int, int>>()); // создаем лист куда будем записывать маршрут
                        result[i].Add(new Tuple<int, int>(endMatrixY, endMatrixX));
                        //matrixTraced[endMatrixY, endMatrixX] = -1;
                        while (true) // опять вечный цикл
                        {
                            if (current.Item2 - 1 >= 0 && (cleanMatrix[current.Item1, current.Item2 - 1] == cleanMatrix[current.Item1, current.Item2] - 1 || cleanMatrix[current.Item1, current.Item2 - 1] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка слева существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item2--; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                                if (matrix[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                    for (int j = 0; j < graph.Nodes.Length; j++)
                                        for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                            if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                                if (i != j)
                                                    graph.Connect(i, j);
                                matrix[current.Item1, current.Item2] = -1;
                            }
                            else if (current.Item2 + 1 < cleanMatrix.GetLength(1) && (cleanMatrix[current.Item1, current.Item2 + 1] == cleanMatrix[current.Item1, current.Item2] - 1 || cleanMatrix[current.Item1, current.Item2 + 1] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка справа существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item2++; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                                if (matrix[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                    for (int j = 0; j < graph.Nodes.Length; j++)
                                        for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                            if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                                if (i != j)
                                                    graph.Connect(i, j);
                                matrix[current.Item1, current.Item2] = -1;
                            }
                            else if (current.Item1 - 1 >= 0 && (cleanMatrix[current.Item1 - 1, current.Item2] == cleanMatrix[current.Item1, current.Item2] - 1 || cleanMatrix[current.Item1 - 1, current.Item2] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка сверху существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item1--; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                                if (matrix[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                    for (int j = 0; j < graph.Nodes.Length; j++)
                                        for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                            if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                                if (i != j)
                                                    graph.Connect(i, j);
                                matrix[current.Item1, current.Item2] = -1;
                            }
                            else if (current.Item1 + 1 < cleanMatrix.GetLength(0) && (cleanMatrix[current.Item1 + 1, current.Item2] == cleanMatrix[current.Item1, current.Item2] - 1 || cleanMatrix[current.Item1 + 1, current.Item2] == cleanMatrix[startMatrixY, startMatrixX])) // если клетка снизу существует и ее цифра на 1 меньше чем цифра в текущей клетке
                            {
                                current.Item1++; // текущая клетка становится этой клеткой
                                result[i].Add(new Tuple<int, int>(current.Item1, current.Item2)); // добавляем текущую клетку в наш список решений
                                graph[i].Coordinates.Insert(1, new Point(current.Item2 * 20, current.Item1 * 20)); // возможно начальные координаты будут добавляться 2 раза
                                if (matrix[current.Item1, current.Item2] == -1) // если пересекли трассу, то соединяем вершины
                                    for (int j = 0; j < graph.Nodes.Length; j++)
                                        for (int k = 0; k < graph.Nodes[j].Coordinates.Count(); k++)
                                            if (graph.Nodes[j].Coordinates[k].X / 20 == current.Item2 && graph.Nodes[j].Coordinates[k].Y / 20 == current.Item1)
                                                if (i != j)
                                                    graph.Connect(i, j);
                                matrix[current.Item1, current.Item2] = -1;
                            }
                            if (current.Item1 == startMatrixY && current.Item2 == startMatrixX) break; // вечный цикл до тех пор пока не дойдем до начальной клетки
                        }
                        graph.Nodes[i].Coordinates.RemoveAt(0);
                        cleanMatrix[endMatrixY, endMatrixX] = cleanMatrix[startMatrixY, startMatrixX];
                    }
                }
            }
            return matrixTraced;
        }

        private static bool ArrayEquality(int[,] arrA, int[,] arrB)
        {
            if (arrA.GetLength(0) != arrB.GetLength(0)) return false;
            if (arrA.GetLength(1) != arrB.GetLength(1)) return false;

            for (int i = 0; i < arrA.GetLength(0); i++)
            {
                for (int j = 0; j < arrA.GetLength(1); j++)
                {
                    if (arrA[i, j] != arrB[i, j]) return false;
                }
            }

            return true;
        }

    }
}
