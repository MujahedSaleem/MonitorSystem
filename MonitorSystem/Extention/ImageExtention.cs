
using System.Drawing;

namespace MonitorSystem.Extention
{
    public static class ImageExtention
    {
        public static Image ResizeImage(this Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize,size);
        }
    }
}
