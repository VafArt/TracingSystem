using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracingSystem.Application.Services;
using TracingSystem.Domain;

namespace TracingSystem
{
    //класс формы для настройки расслоения
    public partial class BundleSettingsForm : Form
    {
        private readonly List<Trace> _traces;
        private readonly int _traceWidth;
        private bool _isSelectMode = false;
        private Graphics g;
        private MainForm _mainForm;

        public BundleSettingsForm(List<Trace> traces, int traceWidth, MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _traces = traces;
            _traceWidth = traceWidth;
            g = mainForm.workSpace.CreateGraphics();
        }

        private List<int> SelectedTraces { get; set; } = new List<int>();

        public List<List<int>> TracesToBeInOneLayer { get; set; } = new List<List<int>>();

        public List<List<int>> TracesToBeInDifferentLayers { get; set; } = new List<List<int>>();

        private Color _colorForSelecting;

        //обработчик кнопки для выбора трасс на одном слое
        private void selectTracesButton1_Click(object sender, EventArgs e)
        {
            _isSelectMode = !_isSelectMode;
            if (_isSelectMode)
            {
                _mainForm.workSpace.MouseClick += SelectTrace;
                var rnd = new Random();//для рандомного цвета
                _colorForSelecting = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            }
            if (!_isSelectMode)
            {
                _mainForm.workSpace.MouseClick -= SelectTrace;
                var traces = new List<int>(SelectedTraces);
                TracesToBeInOneLayer.Add(traces);
                SelectedTraces.Clear();
            }
        }

        //обработчик события нажатия на трассу
        private void SelectTrace(object? sender, MouseEventArgs e)
        {
            var point = e.Location;
            var workSpace = sender as PictureBox;

            using (var penForSelect = new Pen(_colorForSelecting, _traceWidth))
            using (var penForUnselect = new Pen(Color.Red, _traceWidth))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int i = 0; i < _traces.Count; i++)
                {
                    var tracePoints = _traces[i]?.DirectionChangingCoords?.Select(coord => coord.GetPointF).ToList();
                    if (IsPointInsidePolyline(point, tracePoints, _traceWidth))
                    {
                        //если трасса уже есть в списке удалить ее из списка и убрать выделение
                        if (SelectedTraces.Contains(i))
                        {
                            SelectedTraces.Remove(i);
                            for (int j = 0; j < tracePoints.Count - 1; j++)
                            {
                                g.DrawLine(penForUnselect, tracePoints[j], tracePoints[j + 1]);
                            }
                            break;
                        }
                        //добавляем трассу
                        SelectedTraces.Add(i);
                        //подсвечиваем выбранную трассу
                        for (int j = 0; j < tracePoints.Count - 1; j++)
                        {
                            g.DrawLine(penForSelect, tracePoints[j], tracePoints[j + 1]);
                        }
                        break;
                    }
                }
            }
        }

        //проверяет находится ли точка внутри трассы
        private bool IsPointInsidePolyline(PointF point, List<PointF> polylinePoints, float lineWidth)
        {
            for (int i = 0; i < polylinePoints.Count - 1; i++)
            {
                if (IsPointInsideLine(point, polylinePoints[i], polylinePoints[i + 1], lineWidth))
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsPointInsideLine(PointF point, PointF lineStart, PointF lineEnd, float lineWidth)
        {
            // Вычисляем расстояние от точки до линии
            float distance = Math.Abs((lineEnd.Y - lineStart.Y) * point.X - (lineEnd.X - lineStart.X) * point.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X) /
                             (float)Math.Sqrt(Math.Pow(lineEnd.Y - lineStart.Y, 2) + Math.Pow(lineEnd.X - lineStart.X, 2));

            // Проверяем, находится ли точка в пределах линии
            if (distance <= lineWidth / 2 &&
                Math.Min(lineStart.X, lineEnd.X) <= point.X && point.X <= Math.Max(lineStart.X, lineEnd.X) &&
                Math.Min(lineStart.Y, lineEnd.Y) <= point.Y && point.Y <= Math.Max(lineStart.Y, lineEnd.Y))
            {
                return true;
            }

            return false;
        }

        //обработчик события нажатия на кнопку отмены выделения трасс в отдельный слой
        private void cancelTraces1_Click(object sender, EventArgs e)
        {
            //очистить список и закрасить все трассы красным
            TracesToBeInOneLayer.Clear();

            using (var pen = new Pen(Color.Red, _traceWidth))
            {
                for (int i = 0; i < _traces.Count; i++)
                {
                    var tracePoints = _traces[i]?.DirectionChangingCoords?.Select(coord => coord.GetPointF).ToList();
                    for (int j = 0; j < tracePoints.Count - 1; j++)
                    {
                        g.DrawLine(pen, tracePoints[j], tracePoints[j + 1]);
                    }
                }
            }
        }

        //обработчик события нажатия на кнопку добавления трасс, которые должны быть на рахных слоях
        private void selectTracesButton2_Click(object sender, EventArgs e)
        {
            _isSelectMode = !_isSelectMode;
            if (_isSelectMode)
            {
                _mainForm.workSpace.MouseClick += SelectTrace;
                var rnd = new Random();//для рандомного цвета
                _colorForSelecting = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            }
            if (!_isSelectMode)
            {
                _mainForm.workSpace.MouseClick -= SelectTrace;
                var traces = new List<int>(SelectedTraces);
                TracesToBeInDifferentLayers.Add(traces);
                SelectedTraces.Clear();
            }
        }

        //обработчик события нажатия на кнопку отмены выделения трасс в разные слои
        private void cancelTraces2_Click(object sender, EventArgs e)
        {
            //очистить список и закрасить все трассы красным
            TracesToBeInDifferentLayers.Clear();

            using (var pen = new Pen(Color.Red, _traceWidth))
            {
                for (int i = 0; i < _traces.Count; i++)
                {
                    var tracePoints = _traces[i]?.DirectionChangingCoords?.Select(coord => coord.GetPointF).ToList();
                    for (int j = 0; j < tracePoints.Count - 1; j++)
                    {
                        g.DrawLine(pen, tracePoints[j], tracePoints[j + 1]);
                    }

                }
            }
        }

        //кнопка закрывающая форму
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
