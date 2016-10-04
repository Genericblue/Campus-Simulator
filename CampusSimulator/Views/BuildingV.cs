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
    public partial class BuildingV : Form
    {
        BuildingVM bvm;
        public BuildingV(Campus.Building newBuilding)
        {
            InitializeComponent();
            bvm = new BuildingVM(newBuilding);
            this.Text = newBuilding.name;
            pictureBox1.BackgroundImage = Image.FromFile(newBuilding.campusName + "/" + newBuilding.name + "1.jpg");
            Bitmap bitmap = new Bitmap(
                pictureBox1.Size.Width, pictureBox1.Size.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            pictureBox1.Image = bitmap;
            pictureBox1.Invalidate();
            label1.Text = "Building: " + newBuilding.name;
            label2.Text = "Hours: " + newBuilding.hours;
            label3.Text = "";
        }

        private void BuildingV_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs;
            int x = mouseEventArgs.X;
            int y = mouseEventArgs.Y;
            Campus.Building.Room room = bvm.getRoom(x, y);
            if (room != null)
            {
                label1.Text = "Room: " + room.name;
                label2.Text = "Person: " + room.person;
                label3.Text = "Office Hours:\n" + room.hours;
            }

        }
    }
}
