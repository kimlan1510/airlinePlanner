using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  [Collection("AirlinePlanner")]
  public class AirlinesTest : IDisposable
  {
    public AirlinesTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=airline_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_AirlinesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Airlines.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveAirlinesToDatabase()
    {
      //Arrange
      Airlines testAirlines = new Airlines("eva");
      testAirlines.Save();

      //Act
      List<Airlines> result = Airlines.GetAll();
      List<Airlines> testList = new List<Airlines>{testAirlines};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsAirlinesInDatabase()
    {
      //Arrange
      Airlines testAirlines = new Airlines("eva");
      testAirlines.Save();
      //Act
      Airlines foundAirlines = Airlines.Find(testAirlines.GetId());
      //Assert
      Assert.Equal(testAirlines, foundAirlines);
    }

    [Fact]
    public void Delete_DeletesAirlinesAssociationsFromDatabase_AirlineList()
    {
      //Arrange
      Flights testFlights = new Flights("portland", "New York", "today", "tomorrow", "on time");
      testFlights.Save();

      Airlines testAirlines = new Airlines("eva");
      testAirlines.Save();

      //Act
      testAirlines.AddFlights(testFlights);
      testAirlines.Delete();

      List<Airlines> resultFlightsCategories = testFlights.GetAirlines();
      List<Airlines> testFlightsCategories = new List<Airlines> {};

      //Assert
      Assert.Equal(testFlightsCategories, resultFlightsCategories);
    }

    [Fact]
    public void Test_AddFlights_AddsFlightsToAirlines()
    {
     //Arrange
     Airlines testAirlines = new Airlines("eva");
     testAirlines.Save();

     Flights testFlights = new Flights("portland", "New York", "today", "tomorrow", "on time");
     testFlights.Save();

     Flights testFlights2 = new Flights("Seattle", "New York", "today", "tomorrow", "on time");
     testFlights2.Save();

     //Act
     testAirlines.AddFlights(testFlights);
     testAirlines.AddFlights(testFlights2);

     List<Flights> result = testAirlines.GetFlights();
     List<Flights> testList = new List<Flights>{testFlights, testFlights2};

     //Assert
     Assert.Equal(testList, result);
    }

    [Fact]
    public void GetFlights_ReturnsAllAirlinesFlights_TaskList()
    {
     //Arrange
     Airlines testAirlines = new Airlines("eva");
     testAirlines.Save();

     Flights testFlights1 = new Flights("portland", "New York", "today", "tomorrow", "on time");
     testFlights1.Save();

     Flights testFlights2 = new Flights("Seattle", "New York", "today", "tomorrow", "on time");
     testFlights2.Save();

     //Act
     testAirlines.AddFlights(testFlights1);
     List<Flights> savedFlights = testAirlines.GetFlights();
     List<Flights> testList = new List<Flights> {testFlights1};

     //Assert
     Assert.Equal(testList, savedFlights);
    }


    public void Dispose()
    {
      Flights.DeleteAll();
      Airlines.DeleteAll();
    }

  }
}
