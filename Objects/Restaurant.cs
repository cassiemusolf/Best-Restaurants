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

        public override bool Equals(System.Object otherRestaurant)
        {
          if (!(otherRestaurant is Restaurant))
          {
            return false;
          }
          else
          {
            Restaurant newRestaurant = (Restaurant) otherRestaurant;
            bool idEquality = (this.GetId() == newRestaurant.GetId());
            bool nameEquality = (this.GetName() == newRestaurant.GetName());
            bool locationEquality = (this.GetLocation() == newRestaurant.GetLocation());
            bool priceEquality = (this.GetPrice() == newRestaurant.GetPrice());
            return (idEquality && nameEquality && locationEquality && priceEquality);
          }
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, location, price) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantLocation, @RestaurantPrice);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@RestaurantName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlParameter locationParameter = new SqlParameter();
            locationParameter.ParameterName = "@RestaurantLocation";
            locationParameter.Value = this.GetLocation();
            cmd.Parameters.Add(locationParameter);

            SqlParameter priceParameter = new SqlParameter();
            priceParameter.ParameterName = "@RestaurantPrice";
            priceParameter.Value = this.GetPrice();
            cmd.Parameters.Add(priceParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Restaurant Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);

            SqlParameter restaurantIdParameter = new SqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = id.ToString();
            cmd.Parameters.Add(restaurantIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundRestaurantId = 0;
            string foundRestaurantName = null;
            string foundRestaurantLocation = null;
            string foundRestaurantPrice = null;

            while(rdr.Read())
            {
                foundRestaurantId = rdr.GetInt32(0);
                foundRestaurantName = rdr.GetString(1);
                foundRestaurantLocation = rdr.GetString(2);
                foundRestaurantPrice = rdr.GetString(3);
            }
            Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantLocation, foundRestaurantPrice, foundRestaurantId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return foundRestaurant;
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
