using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  public class Flights
  {
    private int _id;
    private string _flying_from;
    private string _flying_to;
    private string _depart;
    private string _arrival;
    private string _status;

    public Flights(string flying_from, string flying_to, string depart, string arrival, string status, int id = 0)
    {
      _flying_from = flying_from;
      _flying_to = flying_to;
      _depart = depart;
      _arrival = arrival;
      _status = status;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetFlyingFrom()
    {
      return _flying_from;
    }
    public string GetFlyingTo()
    {
      return _flying_to;
    }
    public string GetDepart()
    {
      return _depart;
    }
    public string GetArrivalDate()
    {
      return _arrival;
    }
    public string GetStatus()
    {
      return _status;
    }

    public override bool Equals(System.Object otherFlights)
    {
      if(!(otherFlights is Flights))
      {
        return false;
      }
      else
      {
        Flights newFlights = (Flights) otherFlights;
        bool idEquality = (this.GetId() == newFlights.GetId());
        bool flyingFromEquality = (this.GetFlyingFrom() == newFlights.GetFlyingFrom());
        bool flyingToEquality = (this.GetFlyingTo() == newFlights.GetFlyingTo());
        bool departEquality = (this.GetDepart() == newFlights.GetDepart());
        bool arriveDateEquality = (this.GetArrivalDate() == newFlights.GetArrivalDate());
        bool statusEquality = (this.GetStatus() == newFlights.GetStatus());
        return (idEquality && flyingFromEquality && flyingToEquality && departEquality && arriveDateEquality && statusEquality);
      }
    }

    public static List<Flights> GetAll()
    {
      List<Flights> AllFlights = new List<Flights>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string flying_from = rdr.GetString(1);
        string flying_to = rdr.GetString(2);
        string depart = rdr.GetString(3);
        string arrival = rdr.GetString(4);
        string status = rdr.GetString(5);
        Flights newFlight = new Flights(flying_from, flying_to, depart, arrival, status, id);
        AllFlights.Add(newFlight);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllFlights;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (flying_from, flying_to, depart, arrival, status) OUTPUT INSERTED.id VALUES (@flying_from, @flying_to, @depart, @arrival, @status);", conn);

      SqlParameter fromPara = new SqlParameter("@flying_from", this.GetFlyingFrom());
      SqlParameter toPara = new SqlParameter("@flying_to", this.GetFlyingTo());
      SqlParameter departPara = new SqlParameter("@depart", this.GetDepart());
      SqlParameter arrivePara = new SqlParameter("@arrival", this.GetArrivalDate());
      SqlParameter StatusPara = new SqlParameter("@status", this.GetStatus());

      cmd.Parameters.Add(fromPara);
      cmd.Parameters.Add(toPara);
      cmd.Parameters.Add(departPara);
      cmd.Parameters.Add(arrivePara);
      cmd.Parameters.Add(StatusPara);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Flights Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string flying_from = null;
      string flying_to = null;
      string depart = null;
      string arrival = null;
      string status = null;

      while(rdr.Read())
      {
       foundId = rdr.GetInt32(0);
       flying_from = rdr.GetString(1);
       flying_to = rdr.GetString(2);
       depart = rdr.GetString(3);
       arrival = rdr.GetString(4);
       status = rdr.GetString(5);
      }
      Flights foundFlights = new Flights(flying_from, flying_to, depart, arrival, status, foundId);
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }
      return foundFlights;
    }

    public void Update(string flying_from, string flying_to, string depart, string arrival, string status)
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE flights SET (flying_from, flying_to, depart, arrival, status) = (@flying_from, @flying_to, @depart, @arrival, @status) WHERE id = @Id;", conn);

     SqlParameter flyingFromPara = new SqlParameter("@flying_from", flying_from);
     SqlParameter flyingToPara = new SqlParameter("@flying_to", flying_to);
     SqlParameter departPara = new SqlParameter("@depart", depart);
     SqlParameter arrivalPara = new SqlParameter("@arrival", arrival);
     SqlParameter statusPata = new SqlParameter("@status", status);
     SqlParameter idPara = new SqlParameter("@Id", this.GetId());

     cmd.Parameters.Add(flyingFromPara);
     cmd.Parameters.Add(flyingToPara);
     cmd.Parameters.Add(departPara);
     cmd.Parameters.Add(arrivalPara);
     cmd.Parameters.Add(statusPata);
     cmd.Parameters.Add(idPara);

     this._flying_from = flying_from;
     this._flying_to = flying_to;
     this._depart = depart;
     this._arrival = arrival;
     this._status = status;
     cmd.ExecuteNonQuery();
     conn.Close();
    }


    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
    }
  }
}
