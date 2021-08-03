using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BAL;
public partial class Admin_AddVedio : System.Web.UI.Page
{
    VedioBAL objVedio = new VedioBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int a = 0;
        string subPath = "~/Video";        // your code goes here
        try
        {
            objVedio.VideoName = txtVideoName.Text.Trim();
            objVedio.IsActive = true;
            if (fpUpload.HasFile)
            {

                bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));

                if (!IsExists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                string Extention = Path.GetExtension(fpUpload.FileName);
               
                string fileName = Path.GetFileNameWithoutExtension(fpUpload.FileName);
                string fileExtension = Path.GetExtension(fpUpload.FileName);
                
               
                objVedio.VideoPath = RenameFile(fileName,objVedio.GetMaxVideoID().ToString())+fileExtension;
       
           
                a = objVedio.InsertVideo();
                fpUpload.PostedFile.SaveAs(Server.MapPath(subPath + "/" + RenameFile(fileName,a.ToString())+fileExtension));
            }
            if (a > 0)
            {
                Response.Redirect("ManageVideo.aspx");
                // Global.Alert(this, "Category added successfully.");
                clear();
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
    }
    private void clear()
    {
        txtVideoName.Text = string.Empty;
    }
    private string RenameFile(String oldFilename, String newFilename)
    {
        FileInfo file = new FileInfo(oldFilename);

        if (file.Exists)
        {
            File.Move(oldFilename, newFilename);
        }
        return newFilename;
    }

}