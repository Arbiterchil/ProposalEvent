﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatedEventProposalManagement
{
    public partial class CustomNotif : Form
    {
        public CustomNotif()
        {
            InitializeComponent();
        }

        public enum enAc{

            wait,
            start,
            close

           }

        private CustomNotif.enAc action;
        private int x, y;
        public void shoWAlert(string namep, string prepby, string venue, string status)
        {

            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 100; i++)
            {
                fname = "alert" + i.ToString();
                CustomNotif cus = (CustomNotif)Application.OpenForms[fname];
                
                if (cus == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i;
                    this.Location = new Point(this.x,this.y);
                    break;
                }
            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            

            this.label1.Text = namep;
            this.label2.Text = prepby;
            this.label3.Text = venue;
            this.label5.Text = status;
            
            this.Show();
            this.action = enAc.start;
            this.timer1.Interval = 1;
            timer1.Start();

        }

     

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case enAc.wait:
                    timer1.Interval = 5000;
                    action = enAc.close;

                    break;

                case enAc.start:

                    timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                        this.BringToFront();
                        this.Activate();
                        this.Focus();
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = enAc.wait;
                        }
                    }

                    break;

                case enAc.close:

                    timer1.Interval = 1;
                    this.Opacity -= 0.1;
                    this.Left -= 3;

                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                    }

                    break;
                    
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = enAc.close;
        }

        private void CustomNotif_Load(object sender, EventArgs e)
        {
       
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}