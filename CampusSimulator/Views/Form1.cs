using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CampusSimulator.ViewModels;
using CampusSimulator.Views;
using CampusSimulator.Models;

namespace CampusSimulator
{
    public partial class Form1 : Form
    {
        SearchVM svm;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            svm = new SearchVM();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //use searchVm to get campus
            //pass to a new form
            Campus c = svm.getCampus(textBox1.Text);
            if (c != null)
            {
                CampusMapV newWindow = new CampusMapV(c);
                newWindow.Show();
            }
            //close this form
        }
    }
}
