using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CampusSimulator.Models;
using CampusSimulator.ViewModels;

namespace CampusSimulator.Views
{
    public partial class CampusMapV : Form
    {
        Graphics g = null;
        CampusVM cvm;
        bool second = false;
        Campus.Point firstPoint = new Campus.Point();
        public CampusMapV(Campus newCampus)
        {
            InitializeComponent();
            this.Text = newCampus.name;
            cvm = new CampusVM(newCampus);
            update(null);
        }
            
        private void CampusMapV_Load(object sender, EventArgs e)
        {
        
        }

        private void update(List<Campus.Edge> path)
        {
            Campus campus = cvm.campus;
            pictureBox1.BackgroundImage = Image.FromFile(campus.name + "/campusmap.png");
            Bitmap bitmap = new Bitmap(
                pictureBox1.Size.Width, pictureBox1.Size.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            pictureBox1.Image = bitmap;
            g = Graphics.FromImage(pictureBox1.Image);
            if (path != null)
            {
                foreach (Campus.Edge edge in path)
                {
                    Pen p = new Pen(Color.Blue);
                    p.Width = 10;
                    g.DrawLine(p, new Point(edge.p1.x, edge.p1.y), new Point(edge.p2.x, edge.p2.y));
                    
                }
            }
            if (false)
            {
                for(int i = 0; i < campus.points.Count; i++)
                {
                    g.DrawString(i.ToString(), this.Font, Brushes.Black, campus.points[i].x, campus.points[i].y);
                }
                foreach (Campus.Edge edge in campus.edges)
                {
                    Pen p = new Pen(Color.Black);
                    p.Width = 5;
                    g.DrawLine(p, new Point(edge.p1.x, edge.p1.y), new Point(edge.p2.x, edge.p2.y));
                }
            }
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            List<Campus.Edge> path = new List<Campus.Edge>();
            var mouseEventArgs = e as MouseEventArgs;
            int x = mouseEventArgs.X;
            int y = mouseEventArgs.Y;
            Campus.Building b = cvm.getBuilding(x, y);
            if (b != null)
            {
                BuildingV newWindow = new BuildingV(b);
                newWindow.Show();
            }
            else
            {
                if (second == false)
                {
                    cvm.resetPoints();
                    firstPoint = cvm.addPoint(x, y);
                    second = true;
                }
                else
                {
                    second = false;
                    path = cvm.shortestPath(firstPoint, cvm.addPoint(x, y));
                }
                update(path);
            }
        }
    }
}
