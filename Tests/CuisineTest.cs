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

        [Fact]
        public void Test_Find_FindsCuisineInDatabase()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            //Act
            Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

            //Assert
            Assert.Equal(testCuisine, foundCuisine);
        }


        [Fact]
        public void Test_GetRestaurants_RetrievesAllRestaurantsWithCuisine()
        {
            Cuisine testCuisine = new Cuisine("Thai");
            testCuisine.Save();

            Restaurant firstRestaurant = new Restaurant("Bang Bar", "West Seattle", "Medium", testCuisine.GetId());
            firstRestaurant.Save();
            Restaurant secondRestaurant = new Restaurant("Matador", "Seattle", "Low", testCuisine.GetId());
            secondRestaurant.Save();
            Console.WriteLine(secondRestaurant);

            List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
            List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();
            Console.WriteLine(testRestaurantList[0].GetId());
            Console.WriteLine(resultRestaurantList[0].GetId());

            Assert.Equal(testRestaurantList, resultRestaurantList);
        }

        [Fact]
        public void Test_Update_UpdatesCuisineInDatabase()
        {
            //Arrange
            string name = "Mexican";
            Cuisine testCuisine = new Cuisine(name);
            testCuisine.Save();
            string newName = "Italian";

            //Act
            testCuisine.Update(newName);

            string result = testCuisine.GetName();

            //Assert
            Assert.Equal(newName, result);
        }

        public void Dispose()
        {
            Cuisine.DeleteAll();
            Restaurant.DeleteAll();
        }

    }
}
