using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class RestaurantsTest : IDisposable
  {
    public RestaurantsTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bestrestaurants_test;Integrated Security=SSPI;";
    }


    public void Dispose()
    {
      Restaurants.DeleteAll();
    }
  }
}
