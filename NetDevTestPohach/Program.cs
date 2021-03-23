using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text.Json;
namespace NetDevTestPohach
{
    class CD
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }

   


    }

    class Analytics
    {
        public int CdsCount { get; set; }
        public double PricesSum { get; set; }
        public IEnumerable<String> Countries { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }


      
    }

    class Program
    {
        static void Main(string[] args)

        {
            
            List<CD> cds = new List<CD>();
           
            var document = new XmlDocument();
            document.Load("CDs.xml");
            XmlElement  root = document.DocumentElement;

            foreach (XmlElement cdNode in root)
            {
                CD cd = new CD();
               foreach(XmlNode xmlNode in cdNode.ChildNodes)
                {
                    if (xmlNode.Name.Equals("TITLE"))
                    {
                        cd.Title = xmlNode.InnerText;
                 
                    }

                    if (xmlNode.Name.Equals("COUNTRY"))
                    {
                        cd.Country = xmlNode.InnerText;
                    }

                    if (xmlNode.Name.Equals("COMPANY"))
                    {
                        cd.Company = xmlNode.InnerText;
                    }

                    if (xmlNode.Name.Equals("PRICE"))
                    {
                        cd.Price = Double.Parse(xmlNode.InnerText);
                    }

                    if (xmlNode.Name.Equals("YEAR"))
                    {
                        cd.Year = Int32.Parse(xmlNode.InnerText);
                    }

                }

                cds.Add(cd);

            } 
            
            // Amount of cds
            int cdsCount = cds.Count;
         
            //Total Price
            double pricesSum = 0;
           foreach(CD cd in cds)
            {
                pricesSum += cd.Price;
            }

           //Countries
            var countries = cds.OrderBy(n => n.Country).Select(m => m.Country).Distinct();

            int minYear = cds.Min(n => n.Year); 

            int maxYear = cds.Max(n=> n.Year);
           

            Analytics analytics = new Analytics { CdsCount = cdsCount, PricesSum = pricesSum, Countries = countries, MinYear =minYear, MaxYear =maxYear };
            string json = JsonSerializer.Serialize<Analytics>(analytics);
            Console.WriteLine(json);

            Console.ReadLine();
        }
    }
}
