using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zadania
{
    //#Sub task 3.1 Create model for a Person
    struct Person
    {
        public String FirstName;
        public String LastName;
        public String Email;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //TASK #1 Compare Set1_1 with Set1_2. Return not-matched results.
            //Sub task #1.1 Load data needed for tasks into lists
            List<string> Set1_1List = ReadCSV("DataFiles/Set1_1.csv");
            List<string> Set1_2List = ReadCSV("DataFiles/Set1_2.csv");
            List<string> Set2List = ReadCSV("DataFiles/Set2.csv");

            //Sub taks 1.2 Created method that will give us not matched results
            List<string> unmatchedList = GiveUnmatched(Set1_1List, Set1_2List);

            //TASK #2 Merge Set1_1 with Set1_2. Return combined results.
            List<String> combinedResult = new List<String>(Set1_2List);
            combinedResult.AddRange(unmatchedList); 

            //TASK #3 Find any PERSON from Set2 with a NAME that is in the result of #2.            
            //Sub task 3.2 Created method to get person lists and extracted Persons from both files
            List<Person> personLists = GetPersonList(Set2List);
            List<Person> combinedResultsPersonLists = GetPersonList(combinedResult);

            //Sub task 3.3 We are intersted only in NAMES values, so I created method to extract names
            List<string> NamesFromCombinedList = GetJustNames(combinedResultsPersonLists);

            //Sub task 3.4 Match persons from Set2 with a Names from combined results
            List<Person> matchedPersonsList = personLists.Where(person => NamesFromCombinedList.Contains(person.LastName)).ToList();

            //Display results
            int i = 0;
            foreach (var matchedPerson in matchedPersonsList)
            {                
                Console.WriteLine(i++ + ":" + matchedPerson.LastName);               
            }
            
            Console.ReadLine();
        }

        private static List<string> GetJustNames(List<Person> combinedResultsPersonLists)
        {
            List<string> NamesFromCombined = new List<string>();

            foreach (var item in combinedResultsPersonLists)
            {
                NamesFromCombined.Add(item.LastName);
            }
            return NamesFromCombined;
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
            var tempList = new List<string>();
            foreach (var item in list1)
            {
                if (list2.Contains(item) == false)
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }

        private static List<string> ReadCSV(string plik)
        {
            List<string> listFromCSV = new List<string>();

            var reader1 = new StreamReader(File.OpenRead(plik));
            

            List<string> tempList = new List<string>();
            
            while (!reader1.EndOfStream)
            {
                var line = reader1.ReadLine();

                tempList.Add(line);                
            }
            return tempList;
        }

    }
}
