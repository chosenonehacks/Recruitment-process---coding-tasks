using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zadania
{
    class Person
    {
        public String FirstName;
        public String LastName;
        public String Email;
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<string> plik1List = ReadCSV(@"C:\Set1_1.csv");
            List<string> plik2List = ReadCSV(@"C:\Set1_2.csv");
            List<string> set_2 = ReadCSV(@"C:\Set2.csv");

            List<string> unmatchedList = GiveUnmatched(plik1List, plik2List);

            plik2List.AddRange(unmatchedList);

            List<string> cominbedReuslt = plik2List;

            List<Person> personLists = GetPersonList(set_2);
            List<Person> combinedResultsPersonLists = GetPersonList(cominbedReuslt);

            List<string> NamesFromCombined = new List<string>();

            foreach (var item in combinedResultsPersonLists)
            {
                NamesFromCombined.Add(item.LastName);
            }

            List<Person> listaNameKtorePasuja = personLists.Where(person => NamesFromCombined.Contains(person.LastName)).ToList();

            Console.ReadLine();
        }

        private static List<Person> GetPersonList(List<string> set_2)
        {
            List<Person> personList = new List<Person>();

            foreach (var item in set_2)
            {
                Person pSet2 = new Person();

                string[] tablica = item.Split(';');
                pSet2.FirstName = tablica[0];
                pSet2.LastName = tablica[1];
                pSet2.Email = tablica[2];

                personList.Add(pSet2);
            }
            return personList;
        }

        private static List<string> GiveUnmatched(List<string> list1, List<string> list2)
        {           
            var temp = new List<string>();
            foreach (var item in list1)
            {
                if (list2.Contains(item) == false)
                {
                    temp.Add(item);
                }
            }
            return temp;
        }

        private static List<string> ReadCSV(string plik)
        {
            List<string> listFromCSV = new List<string>();

            var reader1 = new StreamReader(File.OpenRead(plik));
            

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader1.EndOfStream)
            {
                var line = reader1.ReadLine();

                listA.Add(line);                
            }

            return listA;
        }

    }
}
