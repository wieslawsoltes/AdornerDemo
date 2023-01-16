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

    double _angleRadians;
    double _angle;

    private void RotateThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        //Console.WriteLine($"DragStarted");
    }

    private double Distance(Vector point1, Vector point2)
    {
       return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
    }
    
    private void RotateThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        //var distance = Distance(RotateThumb.Bounds.TopLeft, Rect.Bounds.Center);
        //var distance = Distance(GetPosition(RotateThumb), GetPosition(Rect));
        var distance = 100;
        //Console.WriteLine($"DragDelta {distance}");
        
        //MoveDelta(RotateThumb, e.Vector);

        Vector point1 = Rect.Bounds.Center;
        Vector point2 = RotateThumb.Bounds.TopLeft + e.Vector;

        var delta = point2 - Rect.Bounds.Center;
        _angleRadians = (Math.PI / 2.0) +  Math.Atan2(delta.Y, delta.X);
        _angle = _angleRadians * (180 / Math.PI);

        var angle = Math.Atan2(delta.Y, delta.X);
        double newX2 = point1.X + distance * Math.Cos(angle);
        double newY2 = point1.Y + distance * Math.Sin(angle);

        //RotateThumb.RenderTransform = new MatrixTransform(Matrix.CreateRotation(_angleRadians));
        MoveTo(RotateThumb, new Vector(newX2, newY2));

        //Console.WriteLine($"DragDelta {point2} {_angleRadians} {_angle}");

        Rect.RenderTransform = new MatrixTransform(Matrix.CreateRotation(_angleRadians));
    }

    private void RotateThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        //Console.WriteLine($"DragCompleted");
    }

    private void MoveTo(Control control, Vector delta)
    {
        var (left, top) = delta;
        Canvas.SetLeft(control, left);
        Canvas.SetTop(control, top);
    }
    
    private void MoveDelta(Control control, Vector delta)
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
