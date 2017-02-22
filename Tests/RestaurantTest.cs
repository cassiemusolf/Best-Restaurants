using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bestrestaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
        //Arrange, Act
        int result = Restaurant.GetAll().Count;

        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Seattle Chan", "Seattle", "High", 1);
      Restaurant secondRestaurant = new Restaurant("Seattle Chan", "Seattle", "High", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
        //Arrange
        Restaurant testRestaurant = new Restaurant("Laredo's", "Seattle", "Medium", 1);
        testRestaurant.Save();

        //Act
        List<Restaurant> result = Restaurant.GetAll();
        List<Restaurant> testList = new List<Restaurant>{testRestaurant};

        //Assert
        Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
        //Arrange
        Restaurant testRestaurant = new Restaurant("KFC", "Federal Way", "Low", 1);
        testRestaurant.Save();

        //Act
        Restaurant savedRestaurant = Restaurant.GetAll()[0];

        int testId = testRestaurant.GetId();
        int result = savedRestaurant.GetId();

        //Assert
        Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
        //Arrange
        Restaurant testRestaurant = new Restaurant("Bang Bar", "West Seattle", "Medium", 1);
        testRestaurant.Save();

        //Act
        Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

        //Assert
        Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
