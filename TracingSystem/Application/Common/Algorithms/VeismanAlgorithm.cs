using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracingSystem.Application.Common.Algorithms
{
    class VeismanAlgorithm
    {
        Graph Graph { get; set; }
        #region
        public VeismanAlgorithm(Graph graph)
        {
            Graph = graph;
        }

        private static int[] GetNumberOfTrueElementsInEachRow(bool[,] matrix) // вычисляет массив с количеством true элементов в каждой строке матрицы
        {
            var numberOfTrueElementsInEachRow = new int[matrix.GetLength(0)];
            var counterOfTrueElements = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == true) counterOfTrueElements++;
                }
                numberOfTrueElementsInEachRow[i] = counterOfTrueElements;
                counterOfTrueElements = 0;
            }
            return numberOfTrueElementsInEachRow;
        }
        private static (bool[,], List<Node>) DeleteRowAndColumn((bool[,], List<Node>) matrix, int number) //удаляет строчку и столбез под номером number. В листе удаляет Node под номером number.
        {
            var resultMatrix = new bool[matrix.Item1.GetLength(0) - 1, matrix.Item1.GetLength(1) - 1];
            matrix.Item2.Remove(matrix.Item2[number]);
            var result = (resultMatrix, matrix.Item2);
            var i1 = 0;
            var j1 = 0;
            for (int i = 0; i < result.resultMatrix.GetLength(0); i++)
            {
                if (i >= number)
                    i1 = i + 1;
                else
                    i1 = i;
                for (int j = 0; j < result.resultMatrix.GetLength(1); j++)
                {
                    if (j >= number) j1 = j + 1;
                    else j1 = j;
                    resultMatrix[i, j] = matrix.Item1[i1, j1];
                }
            }
            return result;
        }
        private static bool IsFalseMatrix(bool[,] matrix)//Возвращает false, если есть хотябы 1 true элемент в матрице 
        {
            foreach (var e in matrix)
            {
                if (e == true) return false;
            }
            return true;
        }

        private static bool IsSameList(List<Node> list1, List<Node> list2)
        {
            if (list1.Count != list2.Count) return false;
            list1 = list1.OrderBy(x => x.NodeNumber).ToList();
            list2 = list2.OrderBy(x => x.NodeNumber).ToList();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].NodeNumber != list2[i].NodeNumber) return false;
            }
            return true;
        }
        private static bool IsSimilarList(List<Node> list1, List<Node> list2)
        {
            var smallerListCount = Math.Min(list1.Count, list2.Count);
            var smallerList = new List<Node>();
            var biggerList = new List<Node>();
            if (list1.Count == smallerListCount) { smallerList = list1; biggerList = list2; }
            else { smallerList = list2; biggerList = list1; }

            for (int i = 0; i < smallerListCount; i++)
            {
                var flag = false;
                for (int j = 0; j < biggerList.Count; j++)
                {
                    if (smallerList[i].NodeNumber == biggerList[j].NodeNumber) { flag = true; break; };
                }
                if (!flag) return false;
            }
            return true;
        }

        private static List<List<Node>> Minimization(List<List<List<Node>>> conjunction)
        {
            while (conjunction.Count != 1) // умножаем скобку на скобку
            {
                var iterator = 0;
                var skobka = new List<List<Node>>();
                for (int i = 0; i < conjunction[conjunction.Count() - 2].Count(); i++)
                {
                    for (int j = 0; j < conjunction.Last().Count; j++)
                    {
                        skobka.Add(new List<Node>());
                        for (int k = 0; k < conjunction[conjunction.Count() - 2][i].Count; k++)
                        {
                            skobka[iterator].Add(conjunction[conjunction.Count() - 2][i][k]);
                        }
                        for (int k = 0; k < conjunction.Last()[j].Count; k++)
                        {
                            skobka[iterator].Add(conjunction.Last()[j][k]);
                        }
                        iterator++;
                    }
                }

                for (int i = 0; i < skobka.Count; i++) // удаляем повторяющиеся вершины
                    skobka[i] = skobka[i].Distinct().OrderBy(x => x.NodeNumber).ToList();

                var skobkaWithoutRepetitions = new List<List<Node>>(); //удаляем повторяющиеся элементы в скобке
                var repeatedElements = new List<List<Node>>();
                for (int i = 0; i < skobka.Count; i++)
                {
                    var flag1 = false; // повторяется ли текущая скобка 
                    var flag2 = false;  // содержится ли текущая скобка в повторяющихся (чтобы не добавлять несколько раз)

                    for (int j = 0; j < skobka.Count; j++)
                    {
                        if ((i != j) && IsSameList(skobka[i], skobka[j])) { flag1 = true; break; }
                    }

                    for (int j = 0; j < repeatedElements.Count; j++)
                    {
                        if (IsSameList(skobka[i], repeatedElements[j])) { flag2 = true; break; }
                    }

                    if (repeatedElements.Count == 0 && flag1) { repeatedElements.Add(skobka[i]); flag1 = false; }
                    if (flag1 && !flag2) repeatedElements.Add(skobka[i]);
                }

                for (int i = 0; i < skobka.Count; i++) //добавляем не повторяющиеся элементы
                {
                    var flag = false; //содержится ли элемент в повторяющихся

                    for (int j = 0; j < repeatedElements.Count; j++)
                    {
                        if (IsSameList(skobka[i], repeatedElements[j])) { flag = true; break; }
                    }
                    if (!flag) skobkaWithoutRepetitions.Add(skobka[i]);
                }

                for (int i = 0; i < repeatedElements.Count; i++)//добавляем повторяющиеся элементы
                    skobkaWithoutRepetitions.Add(repeatedElements[i]);

                var minimizedSkobka = new List<List<Node>>();
                var elementsToDelete = new List<List<Node>>();
                for (int i = 0; i < skobkaWithoutRepetitions.Count; i++)
                {
                    var flag1 = false; // содержит ли в себе другие скобки 
                    var flag2 = false;  // (чтобы не добавлять несколько раз)

                    for (int j = 0; j < skobkaWithoutRepetitions.Count; j++)
                    {
                        if ((i != j) && IsSimilarList(skobkaWithoutRepetitions[i], skobkaWithoutRepetitions[j]) && skobkaWithoutRepetitions[i].Count > skobkaWithoutRepetitions[j].Count) { flag1 = true; break; }
                    }

                    for (int j = 0; j < elementsToDelete.Count; j++)
                    {
                        if (IsSameList(skobkaWithoutRepetitions[i], elementsToDelete[j])) { flag2 = true; break; }
                    }

                    if (elementsToDelete.Count == 0 && flag1) { elementsToDelete.Add(skobkaWithoutRepetitions[i]); flag1 = false; }
                    if (flag1 && !flag2) elementsToDelete.Add(skobkaWithoutRepetitions[i]);
                }

                for (int i = 0; i < skobkaWithoutRepetitions.Count; i++) //добавляем элементы которые не надо удалить
                {
                    var flag = false; //содержится ли элемент в тех, которые надо удалить

                    for (int j = 0; j < elementsToDelete.Count; j++)
                    {
                        if (IsSameList(skobkaWithoutRepetitions[i], elementsToDelete[j])) { flag = true; break; }
                    }
                    if (!flag) minimizedSkobka.Add(skobkaWithoutRepetitions[i]);
                }



                conjunction.RemoveAt(conjunction.Count() - 1);
                conjunction.RemoveAt(conjunction.Count() - 1);
                conjunction.Add(minimizedSkobka);
            }
            return conjunction[0];
        }
        #endregion
        public Task<Dictionary<int, Node[]>[]> RunAsync()
        {
            return Task.Run(Run);
        }

        public Dictionary<int, Node[]>[] Run()
        {
            var matrix = (IncidenceMatrix: Graph.AdjacencyMatrix, Nodes: Graph.Nodes.ToList());
            var conjunction = new List<List<List<Node>>>(); //конъюнкция выражений Ci(xiVxa&xb,...,xq) верхний лист это скобка с выражениями, средний лист содержит два листа: первый с xi, второй с остальными x. нижний лист содержит вершины
            var iterator = 0; // нужен для создания скобок с выражениями Ci(xiVxa&xb,...,xq)
            while (!IsFalseMatrix(matrix.IncidenceMatrix))
            {
                var countOfTrueObjectsInARow = GetNumberOfTrueElementsInEachRow(matrix.IncidenceMatrix);//считаем количество true элементов в каждой строке
                var currentNode = (Node: new Node(0), maxOfTrueElements: 0);
                for (int i = 0; i < countOfTrueObjectsInARow.Length; i++) //записываем в currentNode вершину которой соответствует строка с наибольшим кол-вом true элементов
                {
                    if (countOfTrueObjectsInARow[i] > currentNode.maxOfTrueElements)
                    {
                        currentNode.maxOfTrueElements = countOfTrueObjectsInARow[i];
                        currentNode.Node = matrix.Nodes[i];
                    }
                }

                conjunction.Add(new List<List<Node>>());
                conjunction[iterator].Add(new List<Node>());
                conjunction[iterator].Add(new List<Node>());
                conjunction[iterator][0].Add(currentNode.Node);  //создаем конъюнкцию выражений

                var numberOfTheCurrentNode = 0;
                for (int i = 0; i < matrix.Nodes.Count; i++) // находим порядковый номер текущей вершины в списке оставшихся вершин
                    if (currentNode.Node.NodeNumber == matrix.Nodes[i].NodeNumber) numberOfTheCurrentNode = i;
                for (int i = 0; i < matrix.IncidenceMatrix.GetLength(0); i++) //записываем в конъюнкцию вершины которые должны стоять после V
                {
                    if (matrix.IncidenceMatrix[numberOfTheCurrentNode, i] == true)
                        conjunction[iterator][1].Add(matrix.Nodes[i]);
                }
                matrix = DeleteRowAndColumn(matrix, numberOfTheCurrentNode); //удаляем строку и столбец текущей вершины и удаляем ее из списка вершин
                iterator++;
            }
            //если конъюнкция пустая значит ребер нет и нужно присвоить каждой вершине один цвет
            if (conjunction.Count == 0)
            {
                var result1 = new Dictionary<int, Node[]>[1];
                result1[0] = new Dictionary<int, Node[]>();
                result1[0].Add(0, Graph.Nodes.ToArray());
                return result1;
            }

            var minimizedConjunction = Minimization(conjunction);

            var minimalVVM = new List<List<Node>>();
            iterator = 0;

            foreach (var k in minimizedConjunction)
            {
                var nodesExistance = new bool[Graph.Length];
                foreach (var x in k)
                {
                    nodesExistance[x.NodeNumber] = true;
                }

                minimalVVM.Add(new List<Node>());

                for (int i = 0; i < nodesExistance.Length; i++)
                {
                    if (!nodesExistance[i])
                    {
                        var node = Graph.Nodes.Where(x => x.NodeNumber == i).Select(x => x).First();
                        minimalVVM[iterator].Add(node);
                    }
                }
                iterator++;
            }

            var tConjunction = new List<List<List<Node>>>();
            iterator = 0;
            var testNodes = new Node[minimalVVM.Count];
            for (int i = 0; i < minimalVVM.Count; i++)
            {
                testNodes[i] = new Node(i);
            }

            foreach (var node in Graph.Nodes)
            {
                tConjunction.Add(new List<List<Node>>());
                for (int i = 0; i < minimalVVM.Count; i++)
                {
                    foreach (var e in minimalVVM[i])
                    {
                        if (node.NodeNumber == e.NodeNumber)
                        {
                            var currentNode = new List<Node>();
                            currentNode.Add(testNodes[i]);
                            tConjunction[iterator].Add(new List<Node>(currentNode));
                        }
                    }
                }
                iterator++;
            }

            var answer = Minimization(tConjunction);

            var minimalPredicate = int.MaxValue;

            for (int i = 0; i < answer.Count; i++)
            {
                if (answer[i].Count < minimalPredicate) minimalPredicate = answer[i].Count;
            }

            for (int i = 0; i < answer.Count; i++)
            {
                if (answer[i].Count != minimalPredicate) answer[i] = null;
            }

            answer.RemoveAll(x => x == null);

            var result = new Dictionary<int, Node[]>[answer.Count];
            for (int i = 0; i < answer.Count; i++)
            {
                result[i] = new Dictionary<int, Node[]>();
                for (int j = 0; j < answer[i].Count; j++)
                {
                    result[i].Add(j, minimalVVM[answer[i][j].NodeNumber].ToArray());
                }
            }
            return result;
        }
    }
}
