using System.Drawing;
using System;
using System.Drawing.Drawing2D;

namespace OOPDraw
{
    public partial class OOPDraw : Form
    {

        public OOPDraw()
        {
            InitializeComponent();
            DoubleBuffered = true;
            LineWidth.SelectedItem = "Medium";
            Colour.SelectedItem = "Green";
            Shape.SelectedItem = "Line";
            Action.SelectedItem = "Draw";
        }
        private void Form1_Load(object sender, EventArgs e) { }

        Pen currentPen = new Pen(Color.Black);
        bool dragging = false;
        Point startOfDrag = Point.Empty;
        Point lastMousePosition = Point.Empty;
        List<Shape> shapes = new List<Shape>();
        Rectangle selectionBox;

        private List<Shape> GetSelectedShapes()
        {
            return shapes.Where(s => s.Selected).ToList();
        }

        private void MoveSelectedShapes(MouseEventArgs e)
        {
            foreach (Shape s in GetSelectedShapes())
            {
                s.MoveBy(e.X - lastMousePosition.X, e.Y - lastMousePosition.Y);
            }
        }


        private void SelectShapes()
        {
            DeselectAll();
            foreach (Shape s in shapes)
            {
                if (selectionBox.FullySurrounds(s))
                {
                    s.Select();
                }
            }
        }


        private void DeselectAll()
        {
            foreach (Shape s in shapes)
            {
                s.Deselect();
            }
        }


        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            foreach (Shape shape in shapes)
            {
                shape.Draw(gr);
                Console.WriteLine();
            }

            if (selectionBox != null)
            {
                selectionBox.Draw(gr);
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startOfDrag = lastMousePosition = e.Location;
            switch (Action.Text)
            {
                case "Draw":
                    AddShape(e);
                    break;
                case "Select":
                    Pen p = new Pen(Color.Black, 1.0F);
                    selectionBox = new Rectangle(p, startOfDrag.X, startOfDrag.Y);
                    break;
            }

        }

        private void AddShape(MouseEventArgs e)
        {


            switch (Shape.Text)
            {
                case "Line":
                    shapes.Add(new Line(currentPen, e.X, e.Y));
                    break;
                case "Rectangle":
                    shapes.Add(new Rectangle(currentPen, e.X, e.Y));
                    break;
                case "Ellipse":
                    shapes.Add(new Ellipse(currentPen, e.X, e.Y));
                    break;
                case "Circle":
                    shapes.Add(new Circle(currentPen, e.X, e.Y));
                    break;
            }

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                
                switch (Action.Text)
                {
                    case "Move":
                        MoveSelectedShapes(e);
                        break;
                    case "Draw":
                        Shape shape = shapes.Last();
                        shape.GrowTo(e.X, e.Y);
                        break;
                        
                    case "Select":
                        selectionBox.GrowTo(e.X, e.Y);
                        SelectShapes();
                        break;
                }
                lastMousePosition = e.Location;
                Refresh();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            lastMousePosition = Point.Empty;
            selectionBox = null;
            Refresh();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            float width = currentPen.Width;
            switch (LineWidth.Text)
            {
                case "Thin":
                    width = 2.0F;
                    break;
                case "Medium":
                    width = 4.0F;
                    break;
                case "Thick":
                    width = 8.0F;
                    break;
            }
            currentPen = new Pen(currentPen.Color, width);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = currentPen.Color;
            switch (Colour.Text)
            {
                case "Red":
                    color = Color.Red;
                    break;
                case "Blue":
                    color = Color.Blue;
                    break;
                case "Green":
                    color = Color.Green;
                    break;
            }
            currentPen = new Pen(color, currentPen.Width);
        }




        //Ignore

        private void OOPDraw_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }





    public static class DrawingFucntions
    {
        public static void DrawClosedArc(Graphics g, Shape shape)
        {
            (int x, int y, int w, int h) = shape.EnclosingRectangle();
            if (w > 0 && h > 0)
            {
                g.DrawArc(shape.Pen, x, y, w, h, 0F, 360F);
            }
        }
    }



    public abstract class Shape
    {

        public Pen Pen { get; protected set; }
        public int X1 { get; protected set; }
        public int Y1 { get; protected set; }
        public int X2 { get; protected set; }
        public int Y2 { get; protected set; }
        public bool Selected { get; private set; }


        public Shape(Pen p, int x1, int y1, int x2, int y2)
        {
            Pen = new Pen(p.Color, p.Width);
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Shape(Pen p, int x1, int y1) : this(p, x1, y1, x1, y1)
        {

        }

        public virtual void GrowTo(int x2, int y2)
        {
            X2 = x2;
            Y2 = y2;
        }

        public abstract void Draw(Graphics g);

        public (int, int, int, int) EnclosingRectangle()
        {
            int x = Math.Min(X1, X2);
            int y = Math.Min(Y1, Y2);
            int w = Math.Max(X1, X2) - x;
            int h = Math.Max(Y1, Y2) - y;
            return (x, y, w, h);
        }

        public virtual void MoveBy(int xDelta, int yDelta)
        {
            X1 += xDelta;
            Y1 += yDelta;
            X2 += xDelta;
            Y2 += yDelta;
        }

        public void Select()
        {
            Selected = true;
            Pen.DashStyle = DashStyle.Dash;
        }

        public void Deselect()
        {
            Selected = false;
            Pen.DashStyle = DashStyle.Solid;
        }

    }

    public class CompositeShape : Shape
    {
        private List<Shape> Components { get; set; }
        public CompositeShape(List<Shape> components)
        : base(new Pen(Color.Black, 1.0F), 0, 0, 0, 0)
        {
            Pen.DashStyle = DashStyle.Dash;
            Components = components;
            CalculateEnclosingRectangle();
        }
        private void CalculateEnclosingRectangle()
        {
            X1 = Components.Min(m => Math.Min(m.X1, m.X2));
            Y1 = Components.Min(m => Math.Min(m.Y1, m.Y2));
            X2 = Components.Max(m => Math.Max(m.X1, m.X2));
            Y2 = Components.Max(m => Math.Max(m.Y1, m.Y2));
        }

        public override void Draw(Graphics g)
        {
            foreach (Shape m in Components)
            {
                m.Draw(g);
            }
            if (Selected) g.DrawRectangle(Pen, X1, Y1, X2 - X1, Y2 - Y1);
        }
        public override void MoveBy(int xDelta, int yDelta)
        {
            foreach (Shape m in Components)
            {
                m.MoveBy(xDelta, yDelta);
            }
            X1 += xDelta;
            Y1 += yDelta;
            X2 += xDelta;
            Y2 += yDelta;
        }


    }






    public class Line : Shape
    {

        public Line(Pen p, int x1, int y1, int x2, int y2) : base(p, x1, y1, x2, y2)
        {
        }

        public Line(Pen p, int x1, int y1) : base(p, x1, y1)
        {

        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pen, X1, Y1, X2, Y2);
        }


    }

    public class Rectangle : Shape
    {

        public Rectangle(Pen p, int x1, int y1, int x2, int y2) : base(p, x1, y1, x2, y2)
        {
        }
        public Rectangle(Pen p, int x1, int y1) : base(p, x1, y1)
        {
        }

        public override void Draw(Graphics g)
        {
            (int x, int y, int w, int h) = EnclosingRectangle();
            g.DrawRectangle(Pen, x, y, w, h);
        }

        public bool FullySurrounds(Shape s)
        {
            (int x, int y, int w, int h) = this.EnclosingRectangle();
            (int xs, int ys, int ws, int hs) = s.EnclosingRectangle();
            return x < xs && y < ys && x + w > xs + ws && y + h > ys + hs;
        }


    }


    public class Ellipse : Shape
    {
        public Ellipse(Pen p, int x1, int y1) : base(p, x1, y1)
        {
        }

        public Ellipse(Pen p, int x1, int y1, int x2, int y2) : base(p, x1, y1, x2, y2)
        {
        }

        public override void Draw(Graphics g)
        {
            DrawingFucntions.DrawClosedArc(g, this);

        }
    }


    public class Circle : Shape
    {
        public Circle(Pen p, int x1, int y1) : base(p, x1, y1)
        {
        }

        public Circle(Pen p, int x1, int y1, int x2, int y2) : base(p, x1, y1, x2, y2)
        {
            GrowTo(x2, y2);
        }

        public override void Draw(Graphics g)
        {
            DrawingFucntions.DrawClosedArc(g, this);

        }

        public override void GrowTo(int x2, int y2)
        {
            int diameter = Math.Max(x2 - X1, y2 - Y1);

            X2 = X1 + diameter;
            Y2 = Y1 + diameter;
        }

    }

}