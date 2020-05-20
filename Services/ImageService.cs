using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace UltraZoom.Services
{
	public static class ImageService
	{
		public static Image ToImage(this byte[] bytes)
		{
			if (bytes == null) return null;
			var ms = new MemoryStream(bytes, 0, bytes.Length);
			ms.Write(bytes, 0, bytes.Length);
			return Image.FromStream(ms, true);
		}

		public static byte[] ToByteArray(this Image img)
		{
			using (var memoryStream = new MemoryStream())
			{
				img.Save(memoryStream, ImageFormat.Png);
				return memoryStream.ToArray();
			}
		}
	}
}
