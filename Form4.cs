using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GoogleSearch
{
    public partial class Form4 : Form
    {
        public Form1 new_form_1;

        public SitesList site = new SitesList();

        public class Site
        {
            public string QUERY { get; set; }
            public string URL { get; set; }
            public int POS { get; set; }
            public string DATA { get; set; }

            public Site(string q,string u,int p,string d)
            {
                QUERY = q; URL = u;POS = p;DATA = d;
            }
            
        }

        public class SitesList : ArrayList, ITypedList
        {
            public PropertyDescriptorCollection
            GetItemProperties(PropertyDescriptor[] listAccessors)
            {
                return TypeDescriptor.GetProperties(typeof(Site));
            }
            public string GetListName(PropertyDescriptor[] listAccessors)
            {
                return "SiteList";
            }
        }

        public Form4(List<File_struct> data, string url, string query, Form1 new_form)
        {
            InitializeComponent();

            try
            {

                new_form_1 = new_form;

                new_form_1.Hide();

                label1.Text = "Vaicājums: " + query + ", URL: " + url;

                for (int i = 0; i < data.Count; i++)
                {
                    if (String.Compare(data[i].query, query) == 0 && String.Compare(data[i].url, url) == 0)
                    {
                        site.Add(new Site(data[i].query, data[i].url, data[i].pos, data[i].day.ToString() + "/" + data[i].month.ToString() + "/" + data[i].year.ToString()));
                    }
                }
            }
            catch (Exception e)
            {
                 MessageBox.Show("Вызвано исключение! \n" + e,"Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            

    }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = site;
            dataGridView1.Columns[0].HeaderText = "Vaicājums";
            dataGridView1.Columns[1].HeaderText = "URL";
            dataGridView1.Columns[2].HeaderText = "Pozcīcija";
            dataGridView1.Columns[3].HeaderText = "Datums";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.new_form_1.Show();
        }
        public void formclose_2(object sender, EventArgs e)
        {

            this.new_form_1.Show();
        }
    }
}
