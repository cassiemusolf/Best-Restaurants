using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BestRestaurants
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Cuisine> allCuisines = Cuisine.GetAll();
                return View["index.cshtml", allCuisines];
            };
            Get["/restaurants"] = _ => {
                List<Restaurant> AllRestaurants = Restaurant.GetAll();
                return View["restaurants.cshtml", AllRestaurants];
            };
            Get["/cuisines"] = _ => {
                List<Cuisine> AllCuisines = Cuisine.GetAll();
                return View["cuisines.cshtml", AllCuisines];
            };
            Get["/cuisines/new"] = _ => {
                return View["cuisines_form.cshtml"];
            };
            Post["/cuisines/new"] = _ => {
                Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
                newCuisine.Save();
                return View["success.cshtml"];
            };
            Get["/restaurants/new"] = _ => {
                List<Cuisine> AllCuisines = Cuisine.GetAll();
                return View["restaurants_form.cshtml", AllCuisines];
            };
            Post["/restaurants/new"] = _ => {
                Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-location"], Request.Form["restaurant-price"], Request.Form["cuisine-id"]);
                newRestaurant.Save();
                return View["success.cshtml"];
            };
            Post["/restaurants/delete"] = _ => {
                Restaurant.DeleteAll();
                return View["success.cshtml"];
            };
            Post["/cuisines/delete"] = _ => {
                Cuisine.DeleteAll();
                return View["success.cshtml"];
            };
            Get["/cuisines/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                var SelectedCuisine = Cuisine.Find(parameters.id);
                var CuisineRestaurants = SelectedCuisine.GetRestaurants();
                model.Add("cuisine", SelectedCuisine);
                model.Add("restaurants", CuisineRestaurants);
                return View["cuisine.cshtml", model];
            };
            Get["cuisine/edit/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                return View["cuisine_edit.cshtml", SelectedCuisine];
            };
            Get["/restaurants/{id}"] = parameters => {
              Restaurant restaurant = Restaurant.Find(parameters.id);
              return View["restaurant.cshtml", restaurant];
            };
            Patch["cuisine/edit/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                SelectedCuisine.Update(Request.Form["cuisine-name"]);
                return View["success.cshtml"];
            };
            Get["cuisine/delete/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                return View["cuisine_delete.cshtml", SelectedCuisine];
            };
            Delete["cuisine/delete/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                SelectedCuisine.Delete();
                return View["success.cshtml"];
            };
            Get["/restaurant/edit/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                return View["restaurant_edit.cshtml", SelectedRestaurant];
            };
            Patch["/restaurant/edit/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                SelectedRestaurant.Update(Request.Form["restaurant-name"]);
                return View["success.cshtml"];
            };
            Get["restaurant/delete/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                return View["restaurant_delete.cshtml", SelectedRestaurant];
            };
            Delete["restaurant/delete/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                SelectedRestaurant.Delete();
                return View["success.cshtml"];
            };
        }
    }
}
