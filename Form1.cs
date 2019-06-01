using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;



namespace GoogleSearch
{

    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
            query.Text = "Delfi";
            search_text.Text = "https://www.delfi.lv/";

        }

        public List<File_struct> def_func()
        {

                        file_work workfile = new file_work();

                        List<File_struct> data = new List<File_struct>();

                        return workfile.read();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (search_text.Text.Length > 0 && query.Text.Length > 0)
                {

                    web_site web = new web_site();

                    web.Url = search_text.Text;

                    web.Query = query.Text;

                    load.Visible = true;

                    bool web_load = false;

                    web.Func = "Pozīcijas apskatīšana";

                    string html = web.get_string_html();

                    if (web.html_parse_to_url_array(html))
                    {
                        if (web.position_from_array(web.Url_arr, web.Url))
                        {
                            label4.Visible = true;
                            if (web.Position != 999)
                            {
                                label4.Text = "Tekoša URL pozīcija \"" + web.Url + "\" pēc vaicājuma \"" + web.Query + "\" - " + web.Position.ToString();
                            }
                            else
                            {
                                label4.Text = "Tekoša URL pozīcija \"" + web.Url + "\" pēc vaicājuma \"" + web.Query + "\" - NaN";
                            }
                            button5.Visible = true;
                        }

                        if (web.file_write(web.Url_arr))
                        {
                            load.Visible = false;

                            web_load = true;
                        }
                    }

                    if (web_load)
                    {

                        file_work workfile = new file_work();

                        workfile.date_set();

                        workfile.Url = web.Url;

                        workfile.Query = web.Query;

                        workfile.Pos = web.Position;

                        if (!workfile.isExist_record())
                        {

                            workfile.write();
                            MessageBox.Show("Ieraksts par pozīciju ir veiksmīgi izveidots!\nInformācija:\n\nVaicājums: " + workfile.Query + "\nURL: " + workfile.Url + "\nPozīcija: " + workfile.Pos, "Izpildīts!", MessageBoxButtons.OK, MessageBoxIcon.None);

                        }
                        else
                        {
                            MessageBox.Show("Šodien ieraksts par pozīciju jau eksistē!\n\nURL: " + workfile.Url + " \nVaicājums: " + workfile.Query + "\nPozīcija: " + workfile.Pos, "Uzmanību!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }



                    }




                }
                else
                {
                    MessageBox.Show("Ir nepieciešams ievadīt datus!", "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception err)
            {
                MessageBox.Show("Izņēmuma izsaukšana! \n" + err, "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public bool isExist(List<File_struct> send_data)
        {
            for (int i = 0; i < send_data.Count; i++)
            {
                if (String.Compare(send_data[i].url, search_text.Text) == 0 && String.Compare(send_data[i].query, query.Text) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Graphic
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (search_text.Text.Length > 0 && query.Text.Length > 0)
                {
                    List<File_struct> send_data = new List<File_struct>();

                    send_data = def_func();

                    if (isExist(send_data))
                    {

                        Form graphic = new Form3(send_data, search_text.Text, query.Text, this);

                        graphic.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Nav iespējams attēlot grafiku pēc datiem, kuri neeksistē!", "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                }
                else
                {
                    MessageBox.Show("Ir nepieciešams ievadīt datus!", "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception err)
            {
                MessageBox.Show("Izņēmuma izsaukšana! \n" + err, "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //table
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (search_text.Text.Length > 0 && query.Text.Length > 0)
                {
                    List<File_struct> send_data = new List<File_struct>();

                    send_data = def_func();

                    if (isExist(send_data))
                    {

                        Form table = new Form4(send_data, search_text.Text, query.Text, this);

                        table.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Nav iespējams attēlot tabulu pēc datiem, kuri neeksistē!", "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                }
                else
                {
                    MessageBox.Show("Ir nepieciešams ievadīt datus!", "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception err)
            {
                MessageBox.Show("Izņēmuma izsaukšana! \n" + err, "Kļūda!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    
        //faq
        private void button4_Click(object sender, EventArgs e)
        {
            Form faq = new Form2();
            faq.ShowDialog();
        }
        //hide
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            label4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Vai vēlaties iziet no lietojumprogrammas?", "Izeja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (exit == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }


}
