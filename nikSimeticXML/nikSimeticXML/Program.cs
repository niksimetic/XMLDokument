using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace nikSimeticXML
{
    class Program
    {
        public class Klijent_class
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string EmailAdresa { get; set; }
        }

        public class Tester
        {
            private static XDocument CreateCustomerListXml()
            {
                List<Klijent_class> lista_klijenata = CreateCustomerList();

                var dokumentXmlKlijenti = new XDocument(new XElement("lista_klijenata",
                    from Klijent_class in lista_klijenata
                    select new XElement("Klijent",
                        new XAttribute("Ime", Klijent_class.Ime),
                        new XAttribute("Prezime", Klijent_class.Prezime),
                        new XElement("EmailAdresa", Klijent_class.EmailAdresa)
                        )));
                return dokumentXmlKlijenti;
            }

            private static List<Klijent_class> CreateCustomerList()
            {
                List<Klijent_class> lista_klijenata = new List<Klijent_class>
            {
                new Klijent_class
                {
                    Ime = "Ivan",
                    Prezime = "Horvatin",
                    EmailAdresa = "ivan.horvatin@email.com"
                },
                new Klijent_class
                {
                    Ime = "Marko",
                    Prezime = "Ivić",
                    EmailAdresa = "marko.ivic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Josip",
                    Prezime = "Bralić",
                    EmailAdresa = "josip.bralic@email.com"
                },
                new Klijent_class
                {
                    Ime = "John",
                    Prezime = "Horvatin",
                    EmailAdresa = "john.horvatin@email.com"
                },
                new Klijent_class
                {
                    Ime = "Mladen",
                    Prezime = "Marković",
                    EmailAdresa = "marko.markovic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Josip",
                    Prezime = "Kukuljan",
                    EmailAdresa = "josip.kukuljan@email.com"
                },
                new Klijent_class
                {
                    Ime = "Ivan",
                    Prezime = "Crnković",
                    EmailAdresa = "ivan.crnkovic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Joško",
                    Prezime = "Karlautić",
                    EmailAdresa = "josko.karlautic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Anatolij",
                    Prezime = "Uremović",
                    EmailAdresa = "anatolij.uremovic@email.com"
                },
            };
                return lista_klijenata;
            }

            static void Main()
            {
                if(!Directory.Exists(@"C:\Documents\xmlDocument"))
                {
                    Directory.CreateDirectory(@"C:\Documents\xmlDocument");

                }
                XDocument dokumentXmlKlijenti = CreateCustomerListXml();

                Console.WriteLine(dokumentXmlKlijenti.ToString());

                // prvo pretraživanje

                Console.WriteLine("\n\nPretraga za jednim elementom (Ime = Anatolij)...");

                var query =
                    from Klijent_class in
                        dokumentXmlKlijenti.Element("lista_klijenata").Elements("Klijent")
                    where Klijent_class.Attribute("Ime").Value == "Anatolij"
                    select Klijent_class;
                XElement oneCustomer = query.SingleOrDefault();

                if (oneCustomer != null)
                {
                    Console.WriteLine(oneCustomer);
                    oneCustomer.Save("C:/Documents/xmlDokument/pretrazivanje1.xml");
                }
                else
                {
                    Console.WriteLine("Ne postoji takav zapis.");
                }

                // drugo pretraživanje

                Console.WriteLine("\nPretraga elemenata potomaka(Ime = Marko)...");

                query =
                    from Klijent_class in
                        dokumentXmlKlijenti.Descendants("Klijent")
                    where Klijent_class.Attribute("Ime").Value == "Marko"
                    select Klijent_class;
                oneCustomer = query.SingleOrDefault();
                if (oneCustomer != null)
                {
                    Console.WriteLine(oneCustomer);
                    oneCustomer.Save("C:/Documents/xmlDokument/pretrazivanje2.xml");
                }
                else
                {
                    Console.WriteLine("Ne postoji takav zapis.");
                }

                // treće pretraživanje

                Console.WriteLine("\nPretraga korištenjem vrijednosti elemenata(EmailAdresa = marko.markovic@mail.com)...");
                query =
                    from EmailAdresa in
                        dokumentXmlKlijenti.Descendants("EmailAdresa")
                    where EmailAdresa.Value == "marko.markovic@email.com"
                    select EmailAdresa;

                XElement oneEmail = query.SingleOrDefault();
                if (oneEmail != null)
                {
                    Console.WriteLine(oneEmail);
                    oneEmail.Save("C:/Documents/xmlDokkument/pretrazivanje3.xml");
                }
                else
                {
                    Console.WriteLine("Ne postoji takav zapis.");
                }

                // četvrto pretraživanje 

                Console.WriteLine("\nPretraga korištenjem vrijednosti elemenata potomaka(EmailAdresa = marko.markovic@mail.com)...");
                query =
                    from Klijent_class in
                        dokumentXmlKlijenti.Descendants("Klijent")
                    where Klijent_class.Element("EmailAdresa").Value == "marko.markovic@email.com"
                    select Klijent_class;

                oneCustomer = query.SingleOrDefault();
                if (oneEmail != null)
                {
                    Console.WriteLine(oneCustomer);
                    oneCustomer.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/xmlDokument/pretrazivanje4.xml");
                }
                else
                {
                    Console.WriteLine("Ne postoji takav zapis.");
                }

                Console.Read();
            }
        } // kraj class
    } // kraj namespace
}