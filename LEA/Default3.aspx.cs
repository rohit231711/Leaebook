using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SwfDotNet.IO;
using SwfDotNet.IO.Tags;
using SwfDotNet.IO.Tags.Types;
using System.Drawing;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string fileName = @"E:\Nirav\Project\LeaeBook10022015\LEA\Book\21\Original10443_1.jpg";
            System.Diagnostics.Process objProcess = new System.Diagnostics.Process();
            objProcess.StartInfo.UseShellExecute = false;
            objProcess.StartInfo.CreateNoWindow = true;
            objProcess.StartInfo.RedirectStandardError = true;
            objProcess.StartInfo.RedirectStandardOutput = true;

            objProcess.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~");
            objProcess.StartInfo.FileName = HttpContext.Current.Server.MapPath("~/jpeg2swf/jpeg2swf.exe");
            //objProcess.StartInfo.Arguments = "-F " + " " + "C:\\WINDOWS\\Fonts\\" + " " + "-s linksopennewwindow=0" + " " + "-T9 -s insertstop" + " -f " + " " + fileName + " " + " -o " + " " + Server.MapPath("~/Book/21/123") + "/" + "1jpg.swf" + " " + "-j 100";
            objProcess.StartInfo.Arguments = "-f C:\\WINDOWS\\Fonts\\ -X 384 -Y 240 -r 0.25 -o " + Server.MapPath("~/Book/21/123") + "/" + "1jpg.swf " + fileName + " ";

            //Start the conversion process.
            objProcess.Start();
            objProcess.WaitForExit();
            objProcess.Close();
            objProcess.Dispose();
            //  insertFileRecord();

            //System.Diagnostics.Process objSysProcess = new System.Diagnostics.Process();
            //objSysProcess.StartInfo.UseShellExecute = false;
            //objSysProcess.StartInfo.CreateNoWindow = true;
            //objSysProcess.StartInfo.RedirectStandardError = true;
            //objSysProcess.StartInfo.RedirectStandardOutput = true;

            //objSysProcess.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~");
            //objSysProcess.StartInfo.FileName = HttpContext.Current.Server.MapPath("~/jpeg2swf/png2swf.exe");
            //objSysProcess.StartInfo.Arguments = "-F " + " " + "C:\\WINDOWS\\Fonts\\" + " " + "-s linksopennewwindow=0" + " " + "-T9 -s insertstop" + " -f " + " " + fileName + " " + " -o " + " " + Server.MapPath("~/Book/21/123") + "/" + "1png.swf" + " " + "-j 100";

            ////Start the conversion process.
            //objSysProcess.Start();
            //objSysProcess.WaitForExit();
            //objSysProcess.Close();
            //objSysProcess.Dispose();
            ////  insertFileRecord();
        }
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        for (int i = 2; i <= 84; i++)
        {
            string fileName = @"E:\Nirav\Project\LeaeBook10022015\LEA\Book\21\Original10442_" + i + ".jpg";

            //Image img = Image.FromFile(@"E:\Nirav\Project\LeaeBook10022015\LEA\Book\21\Original10442_1.jpg");
            Image img = Image.FromFile(fileName);
            int posX = 0;
            int posY = 0;
            int imgWidth = img.Width;
            int imgHeight = img.Height;
            //Create a new Swf instance
            Swf swf = new Swf();
            //Set size in inch unit (1 pixel = 20 inches)
            swf.Size = new Rect(0, 0, (posX + imgWidth) * 20, (posY + imgHeight) * 20);
            swf.Version = 7;  //Version 7 (for compression, must be > 5)
            swf.Header.Signature = "CWS";  //Set the signature to compress the swf

            //Set the background color tag as white
            swf.Tags.Add(new SetBackgroundColorTag(255, 255, 255));
            //Set the jpeg tag
            ushort jpegId = swf.GetNewDefineId();
            //Load the jped directly from an image
            //In fact, this line will load the jpeg data in the file as 
            //a library element only (not to display the jpeg)
            swf.Tags.Add(DefineBitsJpeg2Tag.FromImage(jpegId, img));
            //Now we will define the picture's shape tag
            //to define all the transformations on the picture 
            //(as rotation, color effects, etc..) 
            DefineShapeTag shapeTag = new DefineShapeTag();
            shapeTag.CharacterId = swf.GetNewDefineId();
            shapeTag.Rect = new Rect(posX * 20 - 1, posY * 20 - 1,
                 (posX + imgWidth) * 20 - 1, (posY + imgHeight) * 20 - 1);
            FillStyleCollection fillStyles = new FillStyleCollection();
            fillStyles.Add(new BitmapFill(FillStyleType.ClippedBitmapFill,
                  ushort.MaxValue, new Matrix(0, 0, 20, 20)));
            fillStyles.Add(new BitmapFill(FillStyleType.ClippedBitmapFill,
                           jpegId, new Matrix(posX * 20 - 1, posY * 20 - 1,
                           (20.0 * imgWidth) / img.Width,
                           (20.0 * imgHeight) / img.Height)));
            LineStyleCollection lineStyles = new LineStyleCollection();
            ShapeRecordCollection shapes = new ShapeRecordCollection();
            shapes.Add(new StyleChangeRecord(posX * 20 - 1, posY * 20 - 1, 2));
            shapes.Add(new StraightEdgeRecord(imgWidth * 20, 0));
            shapes.Add(new StraightEdgeRecord(0, imgHeight * 20));
            shapes.Add(new StraightEdgeRecord(-imgWidth * 20, 0));
            shapes.Add(new StraightEdgeRecord(0, -imgHeight * 20));
            shapes.Add(new EndShapeRecord());
            shapeTag.ShapeWithStyle =
               new ShapeWithStyle(fillStyles, lineStyles, shapes);
            swf.Tags.Add(shapeTag);

            //Place the picture to the screen with depth=1
            swf.Tags.Add(new PlaceObject2Tag(shapeTag.CharacterId, 1, 0, 0));
            //Add a single frame
            swf.Tags.Add(new ShowFrameTag());
            swf.Tags.Add(new EndTag());

            //Write the swf to a file
            SwfWriter writer = new SwfWriter(@"E:\Nirav\Project\LeaeBook10022015\LEA\Book\21\10442\" + i + ".swf");
            writer.Write(swf);
            writer.Close();

            img.Dispose();
        }
    }
}