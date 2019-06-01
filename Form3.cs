using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GoogleSearch
{
    public partial class Form3 : Form
    {
        public Form1 new_form_1;

        public Form3(List<File_struct> data,string url,string query,Form1 new_form)
        {
            InitializeComponent();
            try
            {

                new_form_1 = new_form;

                new_form_1.Hide();

                label1.Text = "Grafika apskatīšana pēc vaicājuma - " + query;
                label4.Text = "URL: " + url;
                label2.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();


                DataTable dt = new DataTable();
                dt.Columns.Add("Datums", typeof(byte));
                dt.Columns.Add("Pozīcija", typeof(byte));

                for (int i = 0; i < data.Count; i++)
                {
                    if (String.Compare(data[i].query, query) == 0 && String.Compare(data[i].url, url) == 0 && data[i].month == DateTime.Now.Month)
                    {

                        if (data[i].pos == 999)
                        {
                            dt.Rows.Add(data[i].day, 11);
                        }
                        else
                        {
                            dt.Rows.Add(data[i].day, data[i].pos);
                        }

                    }
                }
                chart1.DataSource = dt;
                chart1.Series["Series1"].Name = url;
                chart1.Series[url].XValueMember = "Datums";
                chart1.Series[url].YValueMembers = "Pozīcija";
                chart1.ChartAreas[0].AxisX.Title = "Datums";
                chart1.ChartAreas[0].AxisY.Title = "Pozīcija";
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 31;
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisY.Interval = 1;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 11;
            }
            catch(Exception e)
            {
                MessageBox.Show("Izņēmuma izsaukšana! \n" + e, "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }


        public void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.new_form_1.Show();
        }

        public void formclose(object sender, EventArgs e)
        {

            this.new_form_1.Show();
        }
    }
}
