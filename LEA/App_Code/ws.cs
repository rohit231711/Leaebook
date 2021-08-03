using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using BAL;
using System.Data;
/// <summary>
/// Summary description for ws
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ws : System.Web.Services.WebService
{

    BookBAL ObjBooks = new BookBAL();
    JavaScriptSerializer js = new JavaScriptSerializer();
    DataTable dt = new DataTable();

    public ws()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]

    public string GetBooks(string search)
     {
        ObjBooks.SearchText = search;
        ObjBooks.IsPublish = 1;

        DataSet ds = new DataSet();
        dt = ObjBooks.BookIssueList();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
       
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return js.Serialize(rows);

    }
    [WebMethod]

    public void InsertTag(string search)
    {
       

            ObjBooks.InsertSearchTags(search);
     

    }

}
