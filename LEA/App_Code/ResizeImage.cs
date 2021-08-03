using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

/// <summary>
/// Summary description for Global
/// </summary>
public class ResizeImage
{
    public ResizeImage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void ReSizeImage(string SourceImage, string TargetImage)
    {
        try
        {
            var image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(SourceImage));
            var scaled = ScaleDownToKb(image, 250, 80);
            SaveJpeg(scaled, HttpContext.Current.Server.MapPath(TargetImage), 80);
        }
        catch
        {

        }
    }
    #region ResizeMyImage


    public static System.Drawing.Image ScaleDownToKb(System.Drawing.Image img, long targetKilobytes, long quality)
    {
        //DateTime start = DateTime.Now;
        //DateTime end;

        float h, w;
        float halfFactor = 100; // halves itself each iteration
        float testPerc = 100;
        var direction = -1;
        long lastSize = 0;
        var iteration = 0;
        var origH = img.Height;
        var origW = img.Width;

        // if already below target, just return the image
        var size = GetImageFileSizeBytes(img, 250000, quality);
        if (size < targetKilobytes * 1024)
        {
            //end = DateTime.Now;
            //Console.WriteLine("================ DONE.  ITERATIONS: " + iteration + " " + end.Subtract(start));
            return img;
        }

        while (true)
        {
            iteration++;

            halfFactor /= 2;
            testPerc += halfFactor * direction;

            h = origH * testPerc / 100;
            w = origW * testPerc / 100;

            var test = ScaleImage(img, (int)w, (int)h);
            size = GetImageFileSizeBytes(test, 50000, quality);

            var byteTarg = targetKilobytes * 1024;
            //Console.WriteLine(iteration + ": " + halfFactor + "% (" + testPerc + ") " + size + " " + byteTarg);

            if ((Math.Abs(byteTarg - size) / (double)byteTarg) < .1 || size == lastSize || iteration > 15 /* safety measure */)
            {
                return test;
            }

            if (size > targetKilobytes * 1024)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            lastSize = size;
        }
    }

    public static long GetImageFileSizeBytes(System.Drawing.Image image, int estimatedSize, long quality)
    {
        long jpegByteSize;
        using (var ms = new MemoryStream(estimatedSize))
        {
            SaveJpeg(image, ms, quality);
            jpegByteSize = ms.Length;
        }
        return jpegByteSize;
    }

    public static void SaveJpeg(System.Drawing.Image image, MemoryStream ms, long quality)
    {
        ((Bitmap)image).Save(ms, FindEncoder(ImageFormat.Jpeg), GetEncoderParams(quality));
    }

    public static void SaveJpeg(System.Drawing.Image image, string filename, long quality)
    {
        ((Bitmap)image).Save(filename, FindEncoder(ImageFormat.Jpeg), GetEncoderParams(quality));
    }

    public static ImageCodecInfo FindEncoder(ImageFormat format)
    {

        if (format == null)
            throw new ArgumentNullException("format");

        foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
        {
            if (codec.FormatID.Equals(format.Guid))
            {
                return codec;
            }
        }

        return null;
    }

    public static EncoderParameters GetEncoderParams(long quality)
    {
        System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
        //Encoder encoder = new Encoder(ImageFormat.Jpeg.Guid);
        EncoderParameters eparams = new EncoderParameters(1);
        EncoderParameter eparam = new EncoderParameter(encoder, quality);
        eparams.Param[0] = eparam;
        return eparams;
    }

    //Scale an image to a given width and height.
    public static System.Drawing.Image ScaleImage(System.Drawing.Image img, int outW, int outH)
    {
        Bitmap outImg = new Bitmap(outW, outH, img.PixelFormat);
        outImg.SetResolution(img.HorizontalResolution, img.VerticalResolution);
        Graphics graphics = Graphics.FromImage(outImg);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(img, new Rectangle(0, 0, outW, outH), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
        graphics.Dispose();
        return outImg;
    }

    #endregion
}