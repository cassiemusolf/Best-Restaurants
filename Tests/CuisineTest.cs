using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
    public class CuisineTest : IDisposable
    {
        public CuisineTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bestrestaurants_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CuisineEmptyAtFirst()
        {
            //Arrange, Act
            int result = Cuisine.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameName()
        {
            //Arrange, Act
            Cuisine firstCuisine = new Cuisine("Mexican");
            Cuisine secondCuisine = new Cuisine("Mexican");

            //Assert
            Assert.Equal(firstCuisine, secondCuisine);
        }

        [Fact]
        public void Test_Save_SavesCuisineToDatabase()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("French");
            testCuisine.Save();

            //Act
            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{testCuisine};

            //Assert
            Assert.Equal(testList, result);
        }

        public void Dispose()
        {
            Cuisine.DeleteAll();
        }

    }
}
