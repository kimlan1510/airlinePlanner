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
    public void Delete_DeletesAirlinesFromDatabase_AirlinesList()
    {
      //Arrange
      string name1 = "eva";
      Airlines testAirlines1 = new Airlines(name1);
      testAirlines1.Save();

      string name2 = "delta";
      Airlines testAirlines2 = new Airlines(name2);
      testAirlines2.Save();

      //Act
      testAirlines1.Delete();
      List<Airlines> resultAirlines = Airlines.GetAll();
      List<Airlines> testAirlinesList = new List<Airlines> {testAirlines2};

      //Assert
      Assert.Equal(testAirlinesList, resultAirlines);
    }


    public void Dispose()
    {
      Flights.DeleteAll();
      Airlines.DeleteAll();
    }

  }
}
