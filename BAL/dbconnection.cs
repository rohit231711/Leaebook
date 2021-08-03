using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for dbconnection
/// </summary>
public class dbconnection
{
    string connection;
    SqlConnection con = new SqlConnection();
 //   private readonly object configurationmanager;

    public dbconnection()
    {
        connection = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        con.ConnectionString = connection;
    }

    public void executedml(string str)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand(str,con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable filltable(string str)
    {
        SqlDataAdapter adp = new SqlDataAdapter(str,con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    public DataSet fillset(string str)
    {
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataSet dt = new DataSet();
        adp.Fill(dt);
        return dt;
    }


    public void fillgrid(string str,GridView grd)
    {
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        grd.DataSource = dt;
        grd.DataBind();
    }

}