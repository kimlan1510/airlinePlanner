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


    public void Dispose()
    {
      Flights.DeleteAll();
      Airlines.DeleteAll();
    }

  }
}
