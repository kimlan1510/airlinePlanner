using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AirlinePlanner
{
  public class Airlines
  {
    private int _id;
    private string _name;

    public Airlines (string name, int id = 0)
    {
      _name = name;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(System.Object otherAirlines)
    {
     if(!(otherAirlines is Airlines))
     {
       return false;
     }
     else
      {
       Airlines newAirlines = (Airlines) otherAirlines;
       bool idEquality = (this.GetId() == newAirlines.GetId());
       bool nameEquality = (this.GetName() == newAirlines.GetName());
       return (idEquality && nameEquality);
      }
    }

    public static List<Airlines> GetAll()
    {
      List<Airlines> AllAirlines = new List<Airlines>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM airline_services;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int airlineId = rdr.GetInt32(0);
        string airlineName = rdr.GetString(1);
        Airlines newAirlines = new Airlines(airlineName, airlineId);
        AllAirlines.Add(newAirlines);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllAirlines;
    }

    public void Save()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO airline_services (name) OUTPUT INSERTED.id VALUES (@name);", conn);

     SqlParameter namePara = new SqlParameter("@name", this.GetName());

     cmd.Parameters.Add(namePara);
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

    public static Airlines Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM airline_services WHERE id = @id;", conn);
      SqlParameter airlineIdPara = new SqlParameter("@id", id.ToString());
      cmd.Parameters.Add(airlineIdPara);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundid = 0;
      string foundAirlineName = null;

      while(rdr.Read())
      {
        foundid = rdr.GetInt32(0);
        foundAirlineName = rdr.GetString(1);
      }
      Airlines foundAirlines = new Airlines(foundAirlineName, foundid);
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }
     return foundAirlines;
    }




    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM airline_services;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
