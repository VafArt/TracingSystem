using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace TracingSystem.Application.Common.Algorithms
{
    public enum ObjectiveFunction
    {
        MinimalDistance,
        MinimalLayerCount,
    }

    public enum PcbMatrixPart
    {
        FreePlace = 0, // свободное место на плате
        TracePart = -1, // часть трассы
        ElementPart = -2 // часть элемента
            // <= -3 - начало и конец трассы
            // > 0 - волна
    }

    public enum TracePriority
    {
        Vertical,
        Horizontal,
    }

    public class Tracing
    {
        //if (startMatrixY - 1 >= 0 && ((matrix[startMatrixY - 1, startMatrixX] > -2) || (matrix[startMatrixY - 1, startMatrixX] == matrix[startMatrixY, startMatrixX]))) // если клетка сверху от начальной клетки существует и равна 0 или значению в начальной клетке

        private class Position
        {
            public int Row { get; set; }

            public int Column { get; set; }

            public Position(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

        private class Connection
        {
            public Position? From { get; set; }

            public Position? To { get; set; }

            public Connection(Position? from, Position? to)
            {
                From = from;
                To = to;
            }
        }

        private static bool CheckIfPositionExists(int[,] matrix, int row, int column)
        {
            if (row >= matrix.GetLength(0) || row < 0) return false;

            if (column >= matrix.GetLength(1) || column < 0) return false;

            return true;
        }

        private static bool CheckIfCorrectPosition(int[,] matrix, int row, int column, int neighborRow, int neighborColumn)
        {
            if (matrix == null) return false;

            if(!CheckIfPositionExists(matrix, row, column)) return false;

            if (!CheckIfPositionExists(matrix, neighborRow, neighborColumn)) return false;

            if (!(row == neighborRow && (Math.Abs(column - neighborColumn) == 1) || (column == neighborColumn && (Math.Abs(row - neighborRow) == 1)))) return false; // если это не соседние клетки, не считая диагональных

            if (matrix[neighborRow, neighborColumn] > 0) return false;

            if (matrix[row, column] == 1 && matrix[neighborRow, neighborColumn] < -2) return false; //чтобы не изменить начальную позицию

            return true;
        }

        private static readonly Func<int[,], int, int, int, int, int, bool> MinimalDistanceComparer = (matrix, row, column, neighborRow, neighborColumn, startValue) =>
        {
            if (!CheckIfCorrectPosition(matrix, row, column, neighborRow, neighborColumn)) return false;

            if (matrix[neighborRow, neighborColumn] == (int)PcbMatrixPart.ElementPart) return false;
            return true;
        };

        private static readonly Func<int[,], int, int, int, int, int, bool> MinimalLayerCountComparer = (matrix, row, column, neighborRow, neighborColumn, startValue) =>
        {
            if (!CheckIfCorrectPosition(matrix, row, column, neighborRow, neighborColumn)) return false;

            if (matrix[neighborRow, neighborColumn] == (int)PcbMatrixPart.ElementPart) return false;

            if (matrix[neighborRow, neighborColumn] == (int)PcbMatrixPart.TracePart) return false;

            if (matrix[neighborRow, neighborColumn] <= -3 && matrix[neighborRow, neighborColumn] != startValue) return false; // начало или конец трассы

            return true;
        };

        public ObjectiveFunction ObjectiveFunction { get; set; }

        public TracePriority Priority { get; set; }

        public Tracing(ObjectiveFunction objectiveFunction, TracePriority priority)
        {
            ObjectiveFunction = objectiveFunction;
            Priority = priority;
        }

        private List<Connection> GetConnections(int[,] matrix)
        {
            //сформировать координаты трасс
            var dict = new Dictionary<int, Connection>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] < -2)
                    {
                        if (!dict.TryAdd(matrix[i, j], new Connection(new Position(i, j), null)))
                            dict[matrix[i, j]].To = new Position(i, j);
                    }
                }
            }
            return dict.Values.ToList();
        }

        public async Task<IEnumerable<Trace>> RunAsync(int[,] matrix)
        {
            var connections = GetConnections(matrix);
            var result = new Trace[connections.Count()];

            //чтобы не менять матрицу в ней только трассы и неиспользуемые пады
            var mainMatrix = (int[,])matrix.Clone();

            // отсортировать в порядке увеличения расстояния между падами
            var orderedConnections = connections.OrderBy(connection => Math.Sqrt(Math.Pow(connection.To.Row - connection.From.Row, 2) + Math.Pow(connection.To.Column - connection.From.Column, 2))).ToList();

            if(ObjectiveFunction == ObjectiveFunction.MinimalLayerCount)
            {
                for (int i = 0; i < orderedConnections.Count; i++)
                {
                    var comparer = MinimalLayerCountComparer;
                    var clone = (int[,])mainMatrix.Clone();
                    try
                    {
                        StartWave
                        (
                        orderedConnections[i].From.Row,
                        orderedConnections[i].From.Column,
                        orderedConnections[i].To.Row,
                        orderedConnections[i].To.Column,
                        clone,
                        comparer);
                        var trace = GetTrace
                            (
                            orderedConnections[i].From.Row,
                            orderedConnections[i].From.Column,
                            orderedConnections[i].To.Row,
                            orderedConnections[i].To.Column,
                            clone
                            );
                        mainMatrix = clone;
                        result[i] = trace;
                    }
                    catch
                    {
                        //почистить матрицу, после неудачной трассировки чтобы остались только пады
                        clone = (int[,])matrix.Clone();
                        comparer = MinimalDistanceComparer;
                        StartWave
                            (
                            orderedConnections[i].From.Row,
                            orderedConnections[i].From.Column,
                            orderedConnections[i].To.Row,
                            orderedConnections[i].To.Column,
                            clone,
                            comparer);
                        var trace = GetTrace
                            (
                            orderedConnections[i].From.Row,
                            orderedConnections[i].From.Column,
                            orderedConnections[i].To.Row,
                            orderedConnections[i].To.Column,
                            clone
                            );
                        result[i] = trace;
                        //переносим эту трассу в основную матрицу
                        for (int k = 0; k < clone.GetLength(0); k++)
                        {
                            for (int j = 0; j < clone.GetLength(1); j++)
                            {
                                if (clone[k, j] == -1)
                                    mainMatrix[k, j] = -1;
                            }
                        }
                    }
                }
            }
            if(ObjectiveFunction == ObjectiveFunction.MinimalDistance)
            {
                var comparer = MinimalDistanceComparer;
                var startWaveTasks = new List<Task>();
                var matrixes = new List<int[,]>();

                for(int i = 0; i < orderedConnections.Count; i++)
                {
                    matrixes.Add((int[,])mainMatrix.Clone());
                    int j = i;
                    startWaveTasks.Add(new Task(() => StartWave
                    (
                        orderedConnections[j].From.Row,
                        orderedConnections[j].From.Column,
                        orderedConnections[j].To.Row,
                        orderedConnections[j].To.Column,
                        matrixes[j],
                        comparer)
                    ));
                }

                foreach (var task in startWaveTasks)
                    task.Start();
                await Task.WhenAll(startWaveTasks);

                var getTraceTasks = new List<Task<Trace>>();
                for (int i = 0; i < orderedConnections.Count; i++)
                {
                    int j = i;
                    getTraceTasks.Add(new Task<Trace>(() => GetTrace
                    (
                        orderedConnections[j].From.Row,
                        orderedConnections[j].From.Column,
                        orderedConnections[j].To.Row,
                        orderedConnections[j].To.Column,
                        matrixes[j])
                    ));
                }
                foreach (var task in getTraceTasks)
                    task.Start();
                result = await Task.WhenAll(getTraceTasks);
            }
            return result;
        }

        private void VerticalPriorityCheckCase(int[,] matrixWithWave, Position currentPosition, int currentWave)
        {

            if (CheckIfPositionExists(matrixWithWave, currentPosition.Row + 1, currentPosition.Column) && matrixWithWave[currentPosition.Row + 1, currentPosition.Column] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row + 1, currentPosition.Column] = -1;
                currentPosition.Row++;
            }
            else if (CheckIfPositionExists(matrixWithWave, currentPosition.Row - 1, currentPosition.Column) && matrixWithWave[currentPosition.Row - 1, currentPosition.Column] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row - 1, currentPosition.Column] = -1;
                currentPosition.Row--;
            }
            else if (CheckIfPositionExists(matrixWithWave, currentPosition.Row, currentPosition.Column + 1) && matrixWithWave[currentPosition.Row, currentPosition.Column + 1] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row, currentPosition.Column + 1] = -1;
                currentPosition.Column++;
            }
            else if (CheckIfPositionExists(matrixWithWave, currentPosition.Row, currentPosition.Column - 1) && matrixWithWave[currentPosition.Row, currentPosition.Column - 1] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row, currentPosition.Column - 1] = -1;
                currentPosition.Column--;
            }
        }

        private void HorizontalPriorityCheckCase(int[,] matrixWithWave, Position currentPosition, int currentWave)
        {
            if (CheckIfPositionExists(matrixWithWave, currentPosition.Row, currentPosition.Column + 1) && matrixWithWave[currentPosition.Row, currentPosition.Column + 1] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row, currentPosition.Column + 1] = -1;
                currentPosition.Column++;
            }
            else if (CheckIfPositionExists(matrixWithWave, currentPosition.Row, currentPosition.Column - 1) && matrixWithWave[currentPosition.Row, currentPosition.Column - 1] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row, currentPosition.Column - 1] = -1;
                currentPosition.Column--;
            }
            else if (CheckIfPositionExists(matrixWithWave, currentPosition.Row + 1, currentPosition.Column) && matrixWithWave[currentPosition.Row + 1, currentPosition.Column] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row + 1, currentPosition.Column] = -1;
                currentPosition.Row++;
            }
            else if (CheckIfPositionExists(matrixWithWave, currentPosition.Row - 1, currentPosition.Column) && matrixWithWave[currentPosition.Row - 1, currentPosition.Column] == currentWave - 1)
            {
                matrixWithWave[currentPosition.Row - 1, currentPosition.Column] = -1;
                currentPosition.Row--;
            }
        }

        //очищает все волны, оставляет трассу, элементы и конец/начало трасс
        //начало и конец текущуй трассы меняет на -1
        private Trace GetTrace(int rowStart, int columnStart, int rowEnd, int columnEnd, int[,] matrixWithWave)
        {
            matrixWithWave[rowStart, columnStart] = 0;

            var trace = new Trace();
            trace.DirectionChangingCoords = new List<TracePoint>();
            var currentWave = matrixWithWave[rowEnd, columnEnd];
            var currentPosition = new Position(rowEnd, columnEnd);
            var previousPosition = new Position(rowEnd, columnEnd);

            var rowChanging = false;
            var columnChanging = false;
            var rowChanged = true;
            var columnChanged = true;
            // пока значение в начале не изменится, -1 - обозначает трассу
            while (matrixWithWave[rowStart, columnStart] != -1)
            {
                if (Priority == TracePriority.Vertical)
                {
                    VerticalPriorityCheckCase(matrixWithWave, currentPosition, currentWave);
                }
                if (Priority == TracePriority.Horizontal)
                {
                    HorizontalPriorityCheckCase(matrixWithWave, currentPosition, currentWave);
                }
                currentWave--;

                if (currentPosition.Row != previousPosition.Row && currentPosition.Column == previousPosition.Column)
                {
                    rowChanged = true;
                    columnChanged = false;
                }

                if (currentPosition.Row == previousPosition.Row && currentPosition.Column != previousPosition.Column)
                {
                    rowChanged = false;
                    columnChanged = true;
                }

                if (rowChanging != rowChanged || columnChanging != columnChanged)
                {
                    trace.DirectionChangingCoords.Add(new TracePoint(previousPosition.Column, previousPosition.Row));
                    rowChanging = rowChanged;
                    columnChanging = columnChanged;
                }
                previousPosition = new Position(currentPosition.Row, currentPosition.Column);
            }
            trace.DirectionChangingCoords.Add(new TracePoint(columnStart, rowStart));


            for (int i = 0; i < matrixWithWave.GetLength(0); i++)
            {
                for (int j = 0; j < matrixWithWave.GetLength(1); j++)
                    if (matrixWithWave[i, j] > 0)
                        matrixWithWave[i, j] = 0;
            }
            matrixWithWave[rowEnd, columnEnd] = -1;

            return trace;
        }
        private Trace GetTraceMinimalLayerCountVersion(int rowStart, int columnStart, int rowEnd, int columnEnd, int[,] matrixWithWave)
        {
            matrixWithWave[rowStart, columnStart] = 0;

            //клонируем матрицу и все трассы меняем на свободное пространство
            var clonedMatrix = new int[matrixWithWave.GetLength(0), matrixWithWave.GetLength(1)];
            for (int i = 0; i < clonedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < clonedMatrix.GetLength(1); j++)
                {
                    clonedMatrix[i,j] = clonedMatrix[i,j];
                    if (clonedMatrix[i, j] == -1)
                        clonedMatrix[i, j] = 0;
                }
            }


            var trace = new Trace();
            trace.DirectionChangingCoords = new List<TracePoint>();
            var currentWave = clonedMatrix[rowEnd, columnEnd];
            var currentPosition = new Position(rowEnd, columnEnd);
            var previousPosition = new Position(rowEnd, columnEnd);

            var rowChanging = false;
            var columnChanging = false;
            var rowChanged = true;
            var columnChanged = true;
            // пока значение в начале не изменится, -1 - обозначает трассу
            while (clonedMatrix[rowStart, columnStart] != -1)
            {
                if(Priority == TracePriority.Vertical)
                {
                    VerticalPriorityCheckCase(clonedMatrix, currentPosition, currentWave);
                }
                if(Priority == TracePriority.Horizontal)
                {
                    HorizontalPriorityCheckCase(clonedMatrix, currentPosition, currentWave);
                }
                currentWave--;

                if(currentPosition.Row != previousPosition.Row && currentPosition.Column == previousPosition.Column)
                {
                    rowChanged = true;
                    columnChanged = false;
                }

                if (currentPosition.Row == previousPosition.Row && currentPosition.Column != previousPosition.Column)
                {
                    rowChanged = false;
                    columnChanged = true;
                }

                if(rowChanging != rowChanged || columnChanging != columnChanged)
                {
                    trace.DirectionChangingCoords.Add(new TracePoint(previousPosition.Column, previousPosition.Row));
                    rowChanging = rowChanged;
                    columnChanging = columnChanged;
                }
                previousPosition = new Position(currentPosition.Row, currentPosition.Column);
            }
            trace.DirectionChangingCoords.Add(new TracePoint(columnStart, rowStart));


            for (int i = 0; i < clonedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < clonedMatrix.GetLength(1); j++)
                    if (clonedMatrix[i, j] > 0)
                        clonedMatrix[i, j] = 0;
            }
            clonedMatrix[rowEnd, columnEnd] = -1;

            //перенести трассу из клонированной матрицы в основную (лень делать по нормальному поэтому так)
            for (int i = 0; i < clonedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < clonedMatrix.GetLength(1); j++)
                {
                    if (clonedMatrix[i, j] == -1)
                        matrixWithWave[i, j] = -1;
                }
            }

            return trace;
        }

        //не меняет значение в клетке начала, меняет значение в клетке конца
        private void StartWave(int rowStart, int columnStart, int rowEnd, int columnEnd, int[,] matrix, Func<int[,], int, int, int, int, int, bool> comparer)
        {

            var numbersPositions = new Dictionary<int, List<Position>>();
            if (comparer(matrix, rowStart, columnStart, rowStart - 1, columnStart, matrix[rowStart, columnStart])) // если сверху можно провести трассу
            {
                matrix[rowStart - 1, columnStart] = 1;
                numbersPositions.TryAdd(1, new List<Position>());
                numbersPositions[1].Add(new Position(rowStart - 1, columnStart));
            }

            if (comparer(matrix, rowStart, columnStart, rowStart + 1, columnStart, matrix[rowStart, columnStart])) // если снизу можно провести трассу
            {
                matrix[rowStart + 1, columnStart] = 1;
                numbersPositions.TryAdd(1, new List<Position>());
                numbersPositions[1].Add(new Position(rowStart + 1, columnStart));
            }

            if (comparer(matrix, rowStart, columnStart, rowStart, columnStart - 1, matrix[rowStart, columnStart])) // если слева можно провести трассу
            {
                matrix[rowStart, columnStart - 1] = 1;
                numbersPositions.TryAdd(1, new List<Position>());
                numbersPositions[1].Add(new Position(rowStart, columnStart - 1));
            }

            if (comparer(matrix, rowStart, columnStart, rowStart, columnStart + 1, matrix[rowStart, columnStart])) // если справа можно провести трассу
            {
                matrix[rowStart, columnStart + 1] = 1;
                numbersPositions.TryAdd(1, new List<Position>());
                numbersPositions[1].Add(new Position(rowStart, columnStart + 1));
            }

            if (!numbersPositions.ContainsKey(1)) throw new Exception("Нет доступа к контактной площадке!");

            int currentWave = 2;
            while (matrix[rowEnd, columnEnd] == matrix[rowStart, columnStart])
            {
                foreach(var position in numbersPositions[currentWave - 1])
                {
                    if (comparer(matrix, position.Row, position.Column, position.Row - 1, position.Column, matrix[rowStart, columnStart])) // если сверху можно провести трассу
                    {
                        matrix[position.Row - 1, position.Column] = currentWave;
                        numbersPositions.TryAdd(currentWave, new List<Position>());
                        numbersPositions[currentWave].Add(new Position(position.Row - 1, position.Column));
                    }

                    if (comparer(matrix, position.Row, position.Column, position.Row + 1, position.Column, matrix[rowStart, columnStart])) // если снизу можно провести трассу
                    {
                        matrix[position.Row + 1, position.Column] = currentWave;
                        numbersPositions.TryAdd(currentWave, new List<Position>());
                        numbersPositions[currentWave].Add(new Position(position.Row + 1, position.Column));
                    }

                    if (comparer(matrix, position.Row, position.Column, position.Row, position.Column - 1, matrix[rowStart, columnStart])) // если слева можно провести трассу
                    {
                        matrix[position.Row, position.Column - 1] = currentWave;
                        numbersPositions.TryAdd(currentWave, new List<Position>());
                        numbersPositions[currentWave].Add(new Position(position.Row, position.Column - 1));
                    }

                    if (comparer(matrix, position.Row, position.Column, position.Row, position.Column + 1, matrix[rowStart, columnStart])) // если справа можно провести трассу
                    {
                        matrix[position.Row, position.Column + 1] = currentWave;
                        numbersPositions.TryAdd(currentWave, new List<Position>());
                        numbersPositions[currentWave].Add(new Position(position.Row, position.Column + 1));
                    }
                }
                if (!numbersPositions.ContainsKey(currentWave)) throw new Exception($"Невозможно провести трассу! {currentWave}");
                currentWave++;
            }
        }
    }
}
