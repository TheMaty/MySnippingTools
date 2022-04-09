using SharpAvi;
using SharpAvi.Codecs;
using SharpAvi.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySnippingTool
{
    class Operations
    {
        #region MFC
        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest,

        int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);[DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(int hObject);


        [DllImport("gdi32.dll")]
        static extern int CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hdc, int hgdiobj);

        [DllImport("User32.dll")]
        public static extern int GetDesktopWindow();

        [DllImport("User32.dll")]
        public static extern int GetWindowDC(int hWnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(int hWnd, int hDC);

        #endregion 

        public static void CaptureScreen(string snippingToolFilePath, ref Image imgClipboard, ref bool escapeFired)
        {
            if (!snippingToolFilePath.Contains("ScreenSketch.exe")) // earlier than windows 11
            {
                using (Process snippingToolProcess = new Process())
                {
                    // Get the process start information of snippingTool.
                    ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(snippingToolFilePath, "/clip");

                    // Assign 'StartInfo' of notepad to 'StartInfo' of 'Process' object.
                    snippingToolProcess.StartInfo = myProcessStartInfo;

                    // Create a SnippingTool.
                    snippingToolProcess.Start();


                    //save clipboard to a file in temporary folder
                    //Process can not capture exit message of the snipping tool so "while" loop saves us 
                    while (true)
                    {
                        if (escapeFired)
                            return;

                        if (Clipboard.ContainsImage())
                        {

                            imgClipboard = Clipboard.GetImage();

                            //Close it
                            snippingToolProcess.Close();

                            return;
                        }
                    }

                }
            }
            else //windows 11
            {                  
                using (Process.Start("ms-screenclip:"))
                {
                    //save clipboard to a file in temporary folder
                    //Process can not capture exit message of the snipping tool so "while" loop saves us 
                    while (true)
                    {
                        if (escapeFired)
                            return;

                        if (Clipboard.ContainsImage())
                        {
                            imgClipboard = Clipboard.GetImage();
                            return;
                        }
                    }

                }
            }

            
        }

        public static Image CaptureScreen(int x, int y, int wid, int hei)
        {
            //create DC for the entire virtual screen
            int hdcSrc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            int hdcDest = CreateCompatibleDC(hdcSrc);
            int hBitmap = CreateCompatibleBitmap(hdcSrc, wid, hei);
            SelectObject(hdcDest, hBitmap);

            // set the destination area White - a little complicated
            Bitmap bmp = new Bitmap(wid, hei);
            Image ii = (Image)bmp;
            Graphics gf = Graphics.FromImage(ii);
            IntPtr hdc = gf.GetHdc();
            //use whiteness flag to make destination screen white
            BitBlt(hdcDest, 0, 0, wid, hei, (int)hdc, 0, 0, 0x00FF0062);
            gf.Dispose();
            ii.Dispose();
            bmp.Dispose();

            //Now copy the areas from each screen on the destination hbitmap
            Screen[] screendata = Screen.AllScreens;
            int X, X1, Y, Y1;
            for (int i = 0; i < screendata.Length; i++)
            {
                if (screendata[i].Bounds.X > (x + wid) || (screendata[i].Bounds.X +
                   screendata[i].Bounds.Width) < x || screendata[i].Bounds.Y > (y + hei) ||
                   (screendata[i].Bounds.Y + screendata[i].Bounds.Height) < y)
                {// no common area
                }
                else
                {
                    // something  common
                    if (x < screendata[i].Bounds.X) X = screendata[i].Bounds.X; else X = x;
                    if ((x + wid) > (screendata[i].Bounds.X + screendata[i].Bounds.Width))
                        X1 = screendata[i].Bounds.X + screendata[i].Bounds.Width;
                    else X1 = x + wid;
                    if (y < screendata[i].Bounds.Y) Y = screendata[i].Bounds.Y; else Y = y;
                    if ((y + hei) > (screendata[i].Bounds.Y + screendata[i].Bounds.Height))
                        Y1 = screendata[i].Bounds.Y + screendata[i].Bounds.Height;
                    else Y1 = y + hei;
                    // Main API that does memory data transfer
                    BitBlt(hdcDest, X - x, Y - y, X1 - X, Y1 - Y, hdcSrc, X, Y,
                             0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT
                }
            }

            // send image to clipboard
            Image img = Image.FromHbitmap(new IntPtr(hBitmap));
            DeleteDC(hdcSrc);
            DeleteDC(hdcDest);
            DeleteObject(hBitmap);

            return img;

        }
    }
    public class RecorderParams
    {
        public RecorderParams(string filename, int FrameRate, FourCC Encoder, int Quality, Point SelectedAreaLocation, Size SelectedAreaSize)
        {
            FileName = filename;
            FramesPerSecond = FrameRate;
            Codec = Encoder;
            this.Quality = Quality;

            Height = SelectedAreaSize.Height;
            Width = SelectedAreaSize.Width;

            X = SelectedAreaLocation.X;
            Y = SelectedAreaLocation.Y;
        }

        string FileName;
        public int FramesPerSecond, Quality;
        FourCC Codec;

        public int Height { get; private set; }
        public int Width { get; private set; }


        public int X { get; private set; }
        public int Y { get; private set; }

        public AviWriter CreateAviWriter()
        {
            return new AviWriter(FileName)
            {
                FramesPerSecond = FramesPerSecond,
                EmitIndex1 = true,
            };
        }

        public IAviVideoStream CreateVideoStream(AviWriter writer)
        {
            // Select encoder type based on FOURCC of codec
            if (Codec == KnownFourCCs.Codecs.Uncompressed)
                return writer.AddUncompressedVideoStream(Width, Height);
            else if (Codec == KnownFourCCs.Codecs.MotionJpeg)
                return writer.AddMotionJpegVideoStream(Width, Height, Quality);
            else
            {
                return writer.AddMpeg4VideoStream(Width, Height, (double)writer.FramesPerSecond,
                    // It seems that all tested MPEG-4 VfW codecs ignore the quality affecting parameters passed through VfW API
                    // They only respect the settings from their own configuration dialogs, and Mpeg4VideoEncoder currently has no support for this
                    quality: Quality,
                    codec: Codec,
                    // Most of VfW codecs expect single-threaded use, so we wrap this encoder to special wrapper
                    // Thus all calls to the encoder (including its instantiation) will be invoked on a single thread although encoding (and writing) is performed asynchronously
                    forceSingleThreadedAccess: true);
            }
        }
    }

    public class Recorder : IDisposable
    {
        #region Fields
        AviWriter writer;
        RecorderParams Params;
        IAviVideoStream videoStream;
        Thread screenThread;
        ManualResetEvent stopThread = new ManualResetEvent(false);
        #endregion

        public Recorder(RecorderParams Params, string NameOfTheStream)
        {
            try
            {
                this.Params = Params;

                // Create AVI writer and specify FPS
                writer = Params.CreateAviWriter();

                // Create video stream
                videoStream = Params.CreateVideoStream(writer);
                // Set only name. Other properties were when creating stream, 
                // either explicitly by arguments or implicitly by the encoder used
                videoStream.Name = NameOfTheStream;

                screenThread = new Thread(RecordScreen)
                {
                    Name = typeof(Recorder).Name + ".RecordScreen",
                    IsBackground = true
                };

                screenThread.Start();

            }
            catch (Exception exp)
            {
                throw exp;
            }
            
        }

        public void Dispose()
        {
            stopThread.Set();
            screenThread.Join();

            // Close writer: the remaining data is written to a file and file is closed
            writer.Close();

            stopThread.Dispose();
        }

        void RecordScreen()
        {            
            var frameInterval = TimeSpan.FromSeconds(1 / (double)writer.FramesPerSecond);
            var buffer = new byte[Params.Width * Params.Height * 4];
            Task videoWriteTask = null;
            var timeTillNextFrame = TimeSpan.Zero;

            while (!stopThread.WaitOne(timeTillNextFrame))
            {
                var timestamp = DateTime.Now;

                try
                {
                    Screenshot(buffer);
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                // Wait for the previous frame is written
                videoWriteTask?.Wait();

                // Start asynchronous (encoding and) writing of the new frame
                videoWriteTask = videoStream.WriteFrameAsync(true, buffer, 0, buffer.Length);

                timeTillNextFrame = timestamp + frameInterval - DateTime.Now;
                if (timeTillNextFrame < TimeSpan.Zero)
                    timeTillNextFrame = TimeSpan.Zero;
            }

            // Wait for the last frame is written
            videoWriteTask?.Wait();
        }

        public void Screenshot(byte[] Buffer)
        {
            try
            {
                using (var BMP = new Bitmap(Params.Width, Params.Height))
                {
                    using (var g = Graphics.FromImage(BMP))
                    {
                        //g.CopyFromScreen(Point.Empty, Point.Empty, new Size(Params.Width, Params.Height), CopyPixelOperation.SourceCopy);
                        g.CopyFromScreen(Params.X, Params.Y, 0, 0, new Size(Params.Width, Params.Height), CopyPixelOperation.SourceCopy);

                        g.Flush();

                        var bits = BMP.LockBits(new Rectangle(0, 0, Params.Width, Params.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                        Marshal.Copy(bits.Scan0, Buffer, 0, Buffer.Length);
                        BMP.UnlockBits(bits);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
