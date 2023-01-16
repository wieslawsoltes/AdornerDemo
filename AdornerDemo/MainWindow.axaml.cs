using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace AdornerDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    Vector _startPoint;
    double _angleRadians;
    double _angle;

    private void RotateThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        _startPoint = GetPosition(RotateThumb);
        Console.WriteLine($"DragStarted {_startPoint}");
    }

    private void RotateThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        Move(RotateThumb, e.Vector);

        //var position = GetPosition(RotateThumb);
        var position = RotateThumb.Bounds.Center;
        //var delta = position - _startPoint;
        var delta = position - Rect.Bounds.Center;
        _angleRadians = (Math.PI / 2.0) +  Math.Atan2(delta.Y, delta.X);
        _angle = _angleRadians * (180 / Math.PI);
        Console.WriteLine($"DragDelta {position} {_angleRadians} {_angle}");

        Rect.RenderTransform = new MatrixTransform(Matrix.CreateRotation(_angleRadians));
    }

    private void RotateThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        Console.WriteLine($"DragCompleted {e.Vector}");
    }

    private void Move(Control control, Vector delta)
    {
        var (left, top) = GetPosition(control);
        left += delta.X;
        top += delta.Y;
        Canvas.SetLeft(control, left);
        Canvas.SetTop(control, top);
    }

    private static Vector GetPosition(Control control)
    {
        var left = Canvas.GetLeft(control);
        var top = Canvas.GetTop(control);
        return new Vector(left, top);
    }
}
