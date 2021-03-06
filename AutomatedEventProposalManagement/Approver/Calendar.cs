﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatedEventProposalManagement.Approver
{
    public partial class Calendar : Form
    {
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "20O72YtkHWmSZDIZ2kF01hmg2T3iSetxJO58CuIu",
            BasePath = "https://event-proposal.firebaseio.com/"
        };
        public Calendar()
        {
            InitializeComponent();

        }

      


        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
           
            string date = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            FirebaseResponse pen = client.Get("SAO/Proposal/");
            Dictionary<string, pending> pending = pen.ResultAs<Dictionary<string, pending>>();
            foreach (var find in pending)
            {
                string isDate = find.Value.date_of_event;
                if (isDate.Equals(monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd")))
                {

                    calendarDataGrid.Rows.Add( find.Value.beneficiaries, find.Value.committee_in_charge, find.Value.date,
                                find.Value.date_of_event, find.Value.description, find.Value.id,  find.Value.name_of_project,
                                find.Value.nature_of_project, find.Value.noted_by_adviser, find.Value.noted_by_org_president,  find.Value.org_name, find.Value.org_type,
                                find.Value.prepared_by,find.Value.status, find.Value.time_from, find.Value.time_to, find.Value.venue);

                }
               

            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            
            try
            {
                client = new FireSharp.FirebaseClient(config);
                if (client != null)
                {

                    this.CenterToScreen();
                    this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                    this.WindowState = FormWindowState.Normal;


                }
            }
            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
            ColorDateTaken();

           
           

        }

        public void ColorDateTaken()
        {

            FirebaseResponse response1 = client.Get("SAO/Proposal/");

            Dictionary<string, pending> pending = response1.ResultAs<Dictionary<string, pending>>();
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (var pussy in pending)
            {
                DateTime myDate = Convert.ToDateTime(pussy.Value.date_of_event);

                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                var nowwe = new DateTime(year,month,day);
                
                var totalmonths = (nowwe.Year - myDate.Year) * 12 + nowwe.Month - myDate.Month;
                totalmonths += nowwe.Day < myDate.Day ? -1 : 0;
                var days = nowwe.Subtract(myDate.AddMonths(totalmonths)).Days;

                string f = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
                DateTime hehe = DateTime.Parse(f);
                monthCalendar1.AddBoldedDate(hehe);
                monthCalendar1.UpdateBoldedDates();

                if ( monthCalendar1.BoldedDates.Contains(hehe))
                {
                    this.monthCalendar1.CalendarDimensions = new System.Drawing.Size(2,1);
                    this.monthCalendar1.ShowTodayCircle = true;
                    this.monthCalendar1.ShowWeekNumbers = true;
                    this.monthCalendar1.ShowToday = true;
                }
                















            }


        }


        private void calendarGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
