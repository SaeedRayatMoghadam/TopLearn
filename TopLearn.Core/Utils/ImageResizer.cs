using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TopLearn.Core.Utils;

public class ImageResizer
{

    public static void resize(Stream image, string output_Image_Path, int new_Width)
    {
        const long quality = 50L;

        Bitmap source_Bitmap = new Bitmap(image);
        
        double dblWidth_origial = source_Bitmap.Width;
        
        double dblHeigth_origial = source_Bitmap.Height;

        double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

        int new_Height = (int)(new_Width * relation_heigth_width);
        
        //< create Empty Drawarea >
        var new_DrawArea = new Bitmap(new_Width, new_Height);
        //</ create Empty Drawarea >
        
        using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))

        {

            //< setup >

            graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

            graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

            //</ setup >



            //< draw into placeholder >

            //*imports the image into the drawarea

            graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_Width, new_Height);

            //</ draw into placeholder >



            //--< Output as .Jpg >--

            using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))

            {

                //< setup jpg >

                var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                var encoderParameters = new EncoderParameters(1);

                encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                //</ setup jpg >



                //< save Bitmap as Jpg >

                var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                new_DrawArea.Save(output, codec, encoderParameters);

                //resized_Bitmap.Dispose ();

                output.Close();

                //</ save Bitmap as Jpg >

            }

            //--</ Output as .Jpg >--

            graphic_of_DrawArea.Dispose();

        }

        source_Bitmap.Dispose();

        //---------------</ Image_resize() >---------------

    }
}