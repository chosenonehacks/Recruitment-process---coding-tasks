using Zadania;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Zadania.Tests
{
    [TestClass()]
    public class UnitTests
    {


        [TestMethod()]
        public void ReadCSVTestAnyTest()
        {
            //Arrange                        
            List<string> testList = new List<string>();

            //Act
            testList = Program.ReadCSV("DataFiles/Set1_1.csv");

            //Assert
            Assert.AreEqual(true, testList.Any());
        }

        [TestMethod()]
        public void GiveUnmatchedCount()
        {
            //Arrange               
            List<string> Set1_1List = Program.ReadCSV("DataFiles/Set1_1.csv");
            List<string> Set1_2List = Program.ReadCSV("DataFiles/Set1_2.csv");


            //Act
            List<string> unmatchedList = Program.GiveUnmatched(Set1_1List, Set1_2List);

            //Assert
            Assert.AreEqual(9, unmatchedList.Count());
        }

        [TestMethod()]
        public void CountCombinedResults()
        {
            //Arrange               
            List<string> Set1_1List = Program.ReadCSV("DataFiles/Set1_1.csv");
            List<string> Set1_2List = Program.ReadCSV("DataFiles/Set1_2.csv");
            List<string> unmatchedList = Program.GiveUnmatched(Set1_1List, Set1_2List);


            //Act
            List<String> combinedResult = new List<String>(Set1_2List);
            combinedResult.AddRange(unmatchedList);

            //Assert
            Assert.AreEqual(110, combinedResult.Count());
        }

        [TestMethod()]
        public void GetJustNames()
        {
            //Arrange               
            List<string> Set1_1List = Program.ReadCSV("DataFiles/Set1_1.csv");
            List<string> Set1_2List = Program.ReadCSV("DataFiles/Set1_2.csv");
            List<string> Set2List = Program.ReadCSV("DataFiles/Set2.csv");
            List<string> unmatchedList = Program.GiveUnmatched(Set1_1List, Set1_2List);
            List<string> combinedResult = new List<String>(Set1_2List);
            combinedResult.AddRange(unmatchedList);

            List<Person> personLists = Program.GetPersonList(Set2List);
            List<Person> combinedResultsPersonLists = Program.GetPersonList(combinedResult);

            //Act
            List<string> NamesFromCombinedList = Program.GetJustNames(combinedResultsPersonLists);

            //Assert
            Assert.AreEqual("LastName", NamesFromCombinedList.FirstOrDefault());
        }

        [TestMethod()]
        public void GetPersonList()
        {
            //Arrange               
            List<string> Set1_1List = Program.ReadCSV("DataFiles/Set1_1.csv");
            List<string> Set1_2List = Program.ReadCSV("DataFiles/Set1_2.csv");
            List<string> Set2List = Program.ReadCSV("DataFiles/Set2.csv");
            List<string> unmatchedList = Program.GiveUnmatched(Set1_1List, Set1_2List);
            List<string> combinedResult = new List<String>(Set1_2List);
            combinedResult.AddRange(unmatchedList);
            

            //Act
            List<Person> personLists = Program.GetPersonList(Set2List);

            //Assert
            Assert.IsInstanceOfType(personLists.FirstOrDefault(), typeof(Person));            
        }

        [TestMethod()]
        public void CheckIfMatchedProperly()
        {
            //Arrange               
            List<string> Set1_1List = Program.ReadCSV("DataFiles/Set1_1.csv");
            List<string> Set1_2List = Program.ReadCSV("DataFiles/Set1_2.csv");
            List<string> Set2List = Program.ReadCSV("DataFiles/Set2.csv");
            List<string> unmatchedList = Program.GiveUnmatched(Set1_1List, Set1_2List);
            List<string> combinedResult = new List<String>(Set1_2List);
            combinedResult.AddRange(unmatchedList);

            List<Person> personLists = Program.GetPersonList(Set2List);
            List<Person> combinedResultsPersonLists = Program.GetPersonList(combinedResult);
            List<string> NamesFromCombinedList = Program.GetJustNames(combinedResultsPersonLists);

            //Act
            List<Person> matchedPersonsList = personLists.Where(person => NamesFromCombinedList.Contains(person.LastName)).ToList();

            //Assert
            Assert.IsTrue(matchedPersonsList.Count() == 17);
        }
    }
}
