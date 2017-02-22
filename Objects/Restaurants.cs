using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
    public class Restaurant
    {
        private int _id;
        private string _name;
        private string _location;
        private string _price;

        public Restaurant(string Name, string Location, string Price, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _location = Location;
            _price = Price;
        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string newName)
        {
            _name = newName;
        }
        public string GetLocation()
        {
            return _location;
        }
        public void SetLocation(string newLocation)
        {
            _location = newLocation;
        }
        public string GetPrice()
        {
            return _price;
        }
        public void SetPrice(string newPrice)
        {
            _price = newPrice;
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> allRestaurants = new List<Restaurant>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int restaurantId = rdr.GetInt32(0);
                string restaurantName = rdr.GetString(1);
                string restaurantLocation = rdr.GetString(2);
                string restaurantPrice = rdr.GetString(3);
                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantLocation, restaurantPrice, restaurantId);
                allRestaurants.Add(newRestaurant);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allRestaurants;
        }
        public static void DeleteAll()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
          cmd.ExecuteNonQuery();
          conn.Close();
        }
    }
}
