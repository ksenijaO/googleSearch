using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace GoogleSearch
{
    //Struktūra
    public struct File_struct
    {
        public string url;
        public string query;
        public int pos;
        public int day;
        public int month;
        public int year;

        //Faila struktūra
        public File_struct(string u, string q, int p, int d, int m, int y)
        {
            this.url = u;
            this.query = q;
            this.pos = p;
            this.day = d;
            this.month = m;
            this.year = y;
        }

    }
    //Struktūrs

    //Darbs ar data.dat failu
    public class file_work
    {
        public const string file = "data/data.dat";

        protected int day;
        protected int month;
        protected int year;
        protected string url;
        protected string query;
        protected int pos;

        //Datums
        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
            }
        }
        public int Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        //Datums


        //Url
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }
        //Url

        //Vaicājums
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
            }
        }
        //Vaicājums


        //Pozīcija
        public int Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }
        //Pozīcija


        //Darba metodes ar datu glābtuvi

        public void date_set()
        {
            Day = DateTime.Now.Day;
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
        }
        //Faila data.dat esksistēšanas pārbaude
        public bool file_check()
        {
            if (File.Exists(file))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Faila data.dat esksistēšanas pārbaude

        //Struktūras eksistēsanas pārbaude 
        public bool IsWrited(File_struct f)
        {
            try
            {
                List<File_struct> check_data = new List<File_struct>();

                check_data = read();

                for (int i = 0; i < check_data.Count; i++)
                {
                    if (String.Compare(check_data[i].query, f.query) == 0 && String.Compare(check_data[i].url, f.url) == 0)
                    {
                        if (check_data[i].day == f.day && check_data[i].month == f.month && check_data[i].year == f.year)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Struktūras eksistēsanas pārbaude 


        //Pārbaude par ieraksta eksistēšanu failā 
        public bool isExist_record()
        {
            try
            {
                List<File_struct> check_data = new List<File_struct>();

                check_data = read();

                for (int i = 0; i < check_data.Count; i++)
                {
                    if (String.Compare(check_data[i].query, Query) == 0 && String.Compare(check_data[i].url, Url) == 0)
                    {
                        if (check_data[i].day == Day && check_data[i].month == Month && check_data[i].year == Year)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Pārbaude par ieraksta eksistēšanu failā 

        //Struktūras ieraksta vedošana data.dat failā
        public bool write()
        {
            try
            {
                File_struct f = new File_struct(Url, Query, Pos, Day, Month, Year);

                if (!IsWrited(f))
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.Append)))
                    {
                        writer.Write(f.url);
                        writer.Write(f.query);
                        writer.Write(f.pos);
                        writer.Write(f.day);
                        writer.Write(f.month);
                        writer.Write(f.year);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Struktūras ieraksta vedošana data.dat failā

        //Struktūras nolasīšana no data.dat faila
        public List<File_struct> read()
        {

            List<File_struct> list = new List<File_struct>();

            using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
            {

                while (reader.PeekChar() > -1)
                {

                    string url_1 = reader.ReadString();
                    string query_1 = reader.ReadString();
                    int pos_1 = reader.ReadInt32();
                    int day_1 = reader.ReadInt32();
                    int month_1 = reader.ReadInt32();
                    int year_1 = reader.ReadInt32();

                    list.Add(new File_struct(url_1, query_1, pos_1, day_1, month_1, year_1));

                }

            }

            return list;

        }
        //Struktūras nolasīšana no data.dat faila


    }
    //Klase ar data.dat faila lietošanu

    //Klase ar darbu "Google" HTML
    public class web_site
    {
        protected string url;

        protected string[] url_arr;

        protected int position;

        protected string func;

        protected string query;

        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
            }
        }

        public string Func
        {
            get
            {
                return func;
            }
            set
            {
                func = value;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string[] Url_arr
        {
            get
            {
                return url_arr;
            }
            set
            {
                url_arr = value;
            }
        }

        //URL pozīcija masīvā 
        public bool position_from_array(string[] url_arr, string site_url)
        {
            try
            {
                for (int i = 0; i < url_arr.Length; i++)
                {
                    if (url_arr[i].Contains(site_url))
                    {
                        Position = i + 1;
                        return true;
                    }
                }
                Position = 999;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //URL pozīcija masīvā 


        //Lieko tegu izdzēšana
        public string string_url_replace(string new_html)
        {
            try
            {
                new_html = new_html.Replace("<b>", "");
                new_html = new_html.Replace("</b>", "");
                return new_html;
            }
            catch (Exception)
            {
                return new_html;
            }
        }
        //Lieko tegu izdzēšana

        //Ieraksts žurnālrakstā par vaicājumu
        public bool file_write(string[] html)
        {
            try
            {
                string file_name = "Logs/Logs_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt";
                StreamWriter SW = new StreamWriter(file_name, true);
                SW.Write("\r\n");
                SW.WriteLine("================================================");
                SW.WriteLine(DateTime.Now.ToString());
                SW.Write("\r\n");
                SW.WriteLine(" Vaicājums: " + Query);
                SW.WriteLine(" URL meklēšana: " + Url);
                SW.WriteLine(" Pozīcija: " + Position);
                SW.WriteLine(" Funkcija: " + Func);
                for (int i = 0; i < html.Length; i++)
                {
                    if (i == Position - 1)
                    {
                        SW.Write("\r\n" + "*** " + html[i] + " ***");
                    }
                    else
                    {
                        SW.Write("\r\n" + html[i]);
                    }
                }
                SW.Write("\r\n");
                SW.WriteLine("================================================");
                SW.Write("\r\n");
                SW.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Ieraksts žurnālrakstā par vaicājumu

        //HTML apstāde URL masīvā
        public bool html_parse_to_url_array(string site)
        {
            try
            {
                List<string> url_list = new List<string>();

                //for

                string html = site;



                while (html.Contains("<div class=\"jfp3ef\"><a href=\"/url?q=") != false)
                {

                    string cache_html = html;
                    string new_html = cache_html.Remove(0, cache_html.IndexOf("<div class=\"jfp3ef\"><a href=\"/url?q="));
                    new_html = new_html.Replace("<div class=\"jfp3ef\"><a href=\"/url?q=", "");

                    new_html = new_html.Remove(new_html.IndexOf("&amp;sa="));

                    //new_html = string_url_replace(new_html);


                    if (new_html.Contains("www") || new_html.Contains("https://") || new_html.Contains("http://"))
                    {
                        url_list.Add(new_html);
                    }

                    html = html.Remove(0, html.IndexOf("<div class=\"jfp3ef\"><a href=\"/url?q=") + 41);

                }

                //for

                string[] url_array = url_list.ToArray();

                int url_array_length = url_array.Length;

                Url_arr = url_array;

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //HTML apstāde URL masīvā

        //Rezultātu lapas iegūšāna pēc meklētājvārda
        public string get_string_html()
        {
            try
            {
                WebRequest request = WebRequest.Create("https://www.google.com/search?source=hp&q=" + Query + "&hl=en");
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();
                }
                return html;
            }
            catch (Exception)
            {
                MessageBox.Show("Izņēmuma izsaukšana!", "Kļūda!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "0";
            }
        }
        //Rezultātu lapas iegūšāna pēc meklētājvārda


    }
    //Klase ar darbu "Google" HTML
}
