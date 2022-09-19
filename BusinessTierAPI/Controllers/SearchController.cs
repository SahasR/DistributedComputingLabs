using APIClasses;
using System.Drawing;
using System.Net;
using System.ServiceModel;
using System.Web.Http;


namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/search")]

    public class SearchController : ApiController
    {
        
        [HttpPost]
        public void Post()
        {
             
        }

        public Bitmap Draw()
        {
            Bitmap bitmap = new Bitmap(640, 480);

            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    bitmap.SetPixel(x, y, Color.BlueViolet);
                }
            }

            bitmap.Save("m.bmp");
            return bitmap;
        }
    }
}
