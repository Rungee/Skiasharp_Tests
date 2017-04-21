/*
 * Created by SharpDevelop.
 * User: Runge
 * Date: 4/21/2017
 * Time: 12:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

using SkiaSharp;

namespace Skiasharp_Tests
{
	class Program
	{
		public static void Main(string[] args)
		{
			using (SKBitmap bmpCanvas = new SKBitmap(500,700))
			{
				using (SKCanvas canvas = new SKCanvas(bmpCanvas))
				{
          SKPaint paint = new SKPaint();
					SKBitmap bmpSrc = SKBitmap.Decode("../../images/relief.png");
					SKImage imgSrc = SKImage.FromBitmap(bmpSrc);

          SKRect dstRect1 = new SKRect(-259.9664F, -260.4489F, -259.9664F + 1221.18762F, -260.4489F + 1020.23273F);
          SKRect dstRect2 = new SKRect(0, 0, bmpSrc.Width, bmpSrc.Height);

          SKColor clrBackground = new SKColor(158,180,214);
				
					paint.FilterQuality = SKFilterQuality.High;
					canvas.Clear(clrBackground);
					canvas.DrawImage(imgSrc, dstRect1, paint);
					canvas.Flush();
					
					SaveResult("output_fq_high_bug.png", bmpCanvas);
					
					canvas.Clear(clrBackground);
					canvas.DrawImage(imgSrc, dstRect2, paint);
					canvas.Flush();
					
					SaveResult("output_fq_high_ok.png", bmpCanvas);
					
					canvas.Clear(clrBackground); 
					paint.FilterQuality = SKFilterQuality.Medium;
					canvas.DrawImage(imgSrc, dstRect1, paint);
					canvas.Flush();
					
					SaveResult("output_fq_medium.png", bmpCanvas);
					
					imgSrc.Dispose();
					bmpSrc.Dispose();
					paint.Dispose();
				}
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static void SaveResult(String path, SKBitmap bitmap)
		{
			using (var image = SKImage.FromBitmap(bitmap))
				using (var data = image.Encode(SKEncodedImageFormat.Png, 100)) {
				using (var stream = File.OpenWrite(path)) {
					data.SaveTo(stream);
				}
			}
		}
	}
}