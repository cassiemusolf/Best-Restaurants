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
        private int _cuisineId;

        public Restaurant(string Name, string Location, string Price, int CuisineId, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _location = Location;
            _price = Price;
            _cuisineId = CuisineId;
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
                bool cuisineEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
                return (idEquality && nameEquality && locationEquality && priceEquality && cuisineEquality);
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
        public int GetCuisineId()
        {
            return _cuisineId;
        }
        public void SetCuisineId(int newCuisineId)
        {
            _cuisineId = newCuisineId;
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
                int restaurantCuisineId = rdr.GetInt32(4);
                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantLocation, restaurantPrice, restaurantCuisineId, restaurantId);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, location, price, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantLocation, @RestaurantPrice, @RestaurantCuisineId);", conn);

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

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
            cuisineIdParameter.Value = this.GetCuisineId();
            cmd.Parameters.Add(cuisineIdParameter);

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
            int foundRestaurantCuisineId = 0;

            while(rdr.Read())
            {
                foundRestaurantId = rdr.GetInt32(0);
                foundRestaurantName = rdr.GetString(1);
                foundRestaurantLocation = rdr.GetString(2);
                foundRestaurantPrice = rdr.GetString(3);
                foundRestaurantCuisineId = rdr.GetInt32(4);
            }
            Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantLocation, foundRestaurantPrice, foundRestaurantCuisineId, foundRestaurantId);

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

        public void Update(string newName)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @NewName OUTPUT INSERTED.name WHERE id = @RestaurantId;", conn);

          SqlParameter newNameParameter = new SqlParameter();
          newNameParameter.ParameterName = "@NewName";
          newNameParameter.Value = newName;
          cmd.Parameters.Add(newNameParameter);

          SqlParameter restaurantIdParameter = new SqlParameter();
          restaurantIdParameter.ParameterName = "@RestaurantId";
          restaurantIdParameter.Value = this.GetId();
          cmd.Parameters.Add(restaurantIdParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            this._name = rdr.GetString(0);
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

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId; DELETE FROM restaurants WHERE id = @RestaurantId;", conn);

            SqlParameter restaurantIdParameter = new SqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = this.GetId();

            cmd.Parameters.Add(restaurantIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
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
