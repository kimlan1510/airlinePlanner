using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace AirlinePlanner
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];
      Get["/flights"] = _ => {
        List<Flights> AllFlights = Flights.GetAll();
        return View["flights.cshtml", AllFlights];
      };
      Get["/airlines"] = _ => {
        List<Airlines> AllAirlines = Airlines.GetAll();
        return View["airlines.cshtml", AllAirlines];
      };
      //Create a new flight
      Get["/flights/new"] = _ => {
        return View["flight_form.cshtml"];
      };
      Post["/flights/new"] = _ => {
        Flights newflight = new Flights (Request.Form["flying-from"], Request.Form["flying-to"], Request.Form["depart"], Request.Form["arrive"], Request.Form["status"]);
        newflight.Save();
        return View["success.cshtml"];
      };

      //Create a new airline
      Get["/airlines/new"] = _ => {
        return View["airlines_form.cshtml"];
      };
      Post["/airlines/new"] = _ => {
        Airlines newAirline = new Airlines(Request.Form["name"]);
        newAirline.Save();
        return View["success.cshtml"];
      };

      Get["flights/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Flights SelectedFlights = Flights.Find(parameters.id);
        List<Airlines> FlightsAirlines = SelectedFlights.GetAirlines();
        List<Airlines> AllAirlines = Airlines.GetAll();
        model.Add("flight", SelectedFlights);
        model.Add("flightAirline", FlightsAirlines);
        model.Add("allAirlines", AllAirlines);
        return View["flight.cshtml", model];
      };

      Post["flight/add_airline"] = _ => {
        Airlines airline = Airlines.Find(Request.Form["airline-id"]);
        Flights flight = Flights.Find(Request.Form["flight-id"]);
        flight.AddAirlines(airline);
        return View["success.cshtml"];
      };

      Post["airline/add_flight"] = _ => {
        Airlines airline = Airlines.Find(Request.Form["airline-id"]);
        Flights flight = Flights.Find(Request.Form["flight-id"]);
        airline.AddFlights(flight);
        return View["success.cshtml"];
      };
    }
  }
}
