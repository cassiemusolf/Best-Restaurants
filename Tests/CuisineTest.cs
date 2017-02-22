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

    public void Dispose()
    {
      Cuisine.DeleteAll();
    }

  }
}
