using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ExcelWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;

            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;

        }

        bool isMouseDown = false;
        Point StartPoint;
        Point MovePoint;
        Rectangle rectangle;


        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(canvas);

            var leftOffset = point.X % 10;
            leftOffset = leftOffset < 5 ? -leftOffset : 10 - leftOffset;
            var topOffset = point.Y % 10;
            topOffset = topOffset < 5 ? -topOffset : 10 - topOffset;

            StartPoint = new Point(point.X + leftOffset, point.Y + topOffset);

            rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.LightBlue,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 0,
                Height = 0,
                Opacity = 0.3
            };

            Canvas.SetLeft(rectangle, StartPoint.X);
            Canvas.SetTop(rectangle, StartPoint.Y);
            canvas.Children.Add(rectangle);

            isMouseDown = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;

            var point = e.GetPosition(canvas);

            var leftOffset = point.X % 10;
            leftOffset = leftOffset < 5 ? -leftOffset : 10 - leftOffset;
            var topOffset = point.Y % 10;
            topOffset = topOffset < 5 ? -topOffset : 10 - topOffset;

            MovePoint = new Point(point.X + leftOffset, point.Y + topOffset);
            rectangle.Width = Math.Abs(MovePoint.X - StartPoint.X);
            rectangle.Height = Math.Abs(MovePoint.Y - StartPoint.Y);

            if (MovePoint.X - StartPoint.X <= 0)
            {
                Canvas.SetLeft(rectangle, MovePoint.X);
            }
            if (MovePoint.Y - StartPoint.Y <= 0)
            {
                Canvas.SetTop(rectangle, MovePoint.Y);
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            rectangle.Opacity = 1;
        }

    }
}
