using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace DailyCheckList
{
    public class XMLDataHandler
    {
        public void ExtractItems(List<CheckActivity> Items)
        {
            Items.Clear();
            if (!File.Exists("CheckActivityList.xml")) return;
            XmlDocument doc = new XmlDocument();
            doc.Load("CheckActivityList.xml");
            XmlNode root = doc.FirstChild;
            if (root != null)
                root = root.NextSibling;//First child is <?xml?>
            if (root.HasChildNodes)
            {               
                XmlNode date = root.FirstChild;
                var attr = date.Attributes;
                int y = Convert.ToInt32(attr.GetNamedItem("Year").Value);
                int m = Convert.ToInt32(attr.GetNamedItem("Month").Value);
                int d = Convert.ToInt32(attr.GetNamedItem("Day").Value);
                DateTime today = DateTime.Today;
                bool sameDay = y == today.Year && d == today.Day && m == today.Month;

                XmlNode Activties = date.NextSibling;
                if(Activties!=null&& Activties.HasChildNodes)
                {
                    var Activity = Activties.FirstChild;
                    attr = Activity.Attributes;
                    string Name = attr.GetNamedItem("Name").Value;
                    bool cheked = sameDay && Convert.ToBoolean(attr.GetNamedItem("Checked").Value);
                    if (cheked)
                        Name = "[✓] " + Name;
                    else
                        Name = "[ ] " + Name;
                    Items.Add(new CheckActivity(Name, cheked));

                    while (null!=Activity.NextSibling)
                    {
                        Activity = Activity.NextSibling;
                        attr = Activity.Attributes;
                         Name = attr.GetNamedItem("Name").Value;
                         cheked = sameDay && Convert.ToBoolean(attr.GetNamedItem("Checked").Value);
                        if (cheked)
                            Name = "[✓] " + Name;
                        else
                            Name = "[ ] " + Name;
                        Items.Add(new CheckActivity(Name, cheked));
                    }
                }
            }
           
        }

        public void DumpItems(List<CheckActivity> Items)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter("CheckActivityList.xml",null);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            xmlWriter.WriteStartElement("Date");
            xmlWriter.WriteAttributeString("Year",DateTime.Today.Year.ToString());
            xmlWriter.WriteAttributeString("Month", DateTime.Today.Month.ToString());
            xmlWriter.WriteAttributeString("Day", DateTime.Today.Day.ToString());
            xmlWriter.WriteEndElement();//Date

            xmlWriter.WriteStartElement("Activities");

            foreach (CheckActivity act in Items)
            {
                xmlWriter.WriteStartElement("Activity");
                String name = act.Name;
                if (name.StartsWith("["))
                    name = name.Substring(4);
                xmlWriter.WriteAttributeString("Name", name);
                xmlWriter.WriteAttributeString("Checked", act.IsChecked.ToString());
                xmlWriter.WriteEndElement();//Activity
            }

            xmlWriter.WriteEndElement();//Activities

            xmlWriter.WriteEndElement();//Root
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}
