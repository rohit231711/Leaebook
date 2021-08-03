using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
public partial class ThumbTest : System.Web.UI.Page
{
    BookIssueBAL ObjBookIssue = new BookIssueBAL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        string subPath = "~/Book/" + Convert.ToInt32(13);
        Response.Write(Server.MapPath(subPath));

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string subPath = "~/Book/" + Convert.ToInt32(13);
        Response.Write(subPath);
        bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
        if (!IsExists)
        {
            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
        }
        ObjBookIssue.ID = Convert.ToInt32(txtBookID.Text);
        dt = ObjBookIssue.GetImageMagazinIssueList();
        for (int cntRow = 0; cntRow < dt.Rows.Count; cntRow++)
        {
            byte[] imgarray = imageToByteArray(System.Drawing.Image.FromFile(Server.MapPath(subPath) + "\\Original" + 34 + "_" + (cntRow + 1).ToString() + ".jpg"));
            GenerateThumbnails(327, 250, imgarray, subPath + "/" + "th_" + 34 + "_" + (cntRow + 1).ToString() + ".bmp");
        }
    }
    private void GenerateThumbnails(int height, int width, byte[] sourcePath, string targetPath)
    {
        ImageConverter imageConverter = new System.Drawing.ImageConverter();
        System.Drawing.Image image = imageConverter.ConvertFrom(sourcePath) as System.Drawing.Image;

        {
            var newWidth = (int)(width);
            var newHeight = (int)(height);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighSpeed;
            thumbGraph.SmoothingMode = SmoothingMode.None;
         
            thumbGraph.InterpolationMode = InterpolationMode.Low;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(Server.MapPath(targetPath));
        }
    }
    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return ms.ToArray();
    }

}