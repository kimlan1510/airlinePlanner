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



    public void Dispose()
    {
      Flights.DeleteAll();
    }

  }
}
