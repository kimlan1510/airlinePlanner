using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  [Collection("AirlinePlanner")]
  public class FlightsTest : IDisposable
  {
    public FlightsTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=airline_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     //Arrange, Act
     int result = Flights.GetAll().Count;

     //Assert
     Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
     //Arrange
     Flights testFlights = new Flights("portland", "New York", "today", "tomorrow", "on time");

     //Act
     testFlights.Save();
     List<Flights> result = Flights.GetAll();
     List<Flights> testList = new List<Flights>{testFlights};

     //Assert
     Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindFlightsInDatabase()
    {
      //Arrange
      Flights testFlights = new Flights("portland", "New York", "today", "tomorrow", "on time");
      testFlights.Save();

      //Act
      Flights foundFlights = Flights.Find(testFlights.GetId());

      //Assert
      Assert.Equal(testFlights, foundFlights);
    }

    [Fact]
    public void Test_Update_UpdatesFlightsInDatabase()
    {
      //Arrange
      Flights testFlight = new Flights("Seattle", "New York", "today", "tomorrow", "on time");
      testFlight.Save();
      string new_flying_from = "Portland";
      //Act
      testFlight.Update("Portland", "New York", "today", "tomorrow", "on time");
      string result =testFlight.GetFlyingFrom();

      //Assert
      Assert.Equal(new_flying_from, result);
    }

    [Fact]
    public void AddAirlines_AddsAirlinesToFlights_AirlinesList()
    {
      //Arrange
      Flights testFlights = new Flights("Seattle", "New York", "today", "tomorrow", "on time");
      testFlights.Save();

      Airlines testAirlines = new Airlines("eva");
      testAirlines.Save();

      //Act
      testFlights.AddAirlines(testAirlines);

      List<Airlines> result = testFlights.GetAirlines();
      List<Airlines> testList = new List<Airlines>{testAirlines};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetAirlines_ReturnsAllFlightsAirlines_AirlineList()
    {
      //Arrange
      Flights testFlights = new Flights("Seattle", "New York", "today", "tomorrow", "on time");
      testFlights.Save();

      Airlines testAirlines1 = new Airlines("eva");
      testAirlines1.Save();

      Airlines testAirlines2 = new Airlines("delta");
      testAirlines2.Save();

      //Act
      testFlights.AddAirlines(testAirlines1);
      List<Airlines> result = testFlights.GetAirlines();
      List<Airlines> testList = new List<Airlines> {testAirlines1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Delete_DeletesFlightsAssociationsFromDatabase_FlightsList()
    {
      //Arrange
      Airlines testAirlines = new Airlines("eva");
      testAirlines.Save();

      Flights testFlights = new Flights("Seattle", "New York", "today", "tomorrow", "on time");
      testFlights.Save();

      //Act
      testFlights.AddAirlines(testAirlines);
      testFlights.Delete();

      List<Flights> resultAirlinesFlights = testAirlines.GetFlights();
      List<Flights> testAirlinesFlights = new List<Flights> {};

      //Assert
      Assert.Equal(testAirlinesFlights, resultAirlinesFlights);
    }





    public void Dispose()
    {
      Flights.DeleteAll();
    }

  }
}
