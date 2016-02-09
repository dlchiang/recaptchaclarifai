using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
namespace reCAPTCHA_Breaker
{
    class Program
    {

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
       static int cX = 0;
        static int cY = 0;
        static public Bitmap Copy(Bitmap src, Rectangle rect)
        {
            Bitmap b = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(b);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel);

            g.Dispose();

            return b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Write the reCAPTCHA prompt");
            String tag = Console.ReadLine();
            File.WriteAllText("tag", tag);
            Console.WriteLine("Press CTRL + SHIFT on the top left of the reCAPTCHA box");
            Console.WriteLine("Waiting...");
            Console.WriteLine("Solving Started! Please wait...");
            while(true)
            {
            if (GetAsyncKeyState(Keys.LControlKey) != 0)
            {
                if(GetAsyncKeyState(Keys.LShiftKey)!=0)
                {
                    SetPos();
                    Solve();
                    break;
                }
            }
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();
            }

        }
        static void SetPos()
        {
            cX = Cursor.Position.X +15;
            cY = Cursor.Position.Y +45;
        }
        static void Solve()
        {
            Bitmap entireScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(entireScreen);
            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                             Screen.PrimaryScreen.Bounds.Y,
                             0, 0,
                             entireScreen.Size,
                             CopyPixelOperation.SourceCopy);

            string positions = "";
            Bitmap TL = Copy(entireScreen, new Rectangle(cX, cY, 130, 130));
            Bitmap TM = Copy(entireScreen, new Rectangle(cX + 130, cY, 130, 130));
            Bitmap TR = Copy(entireScreen, new Rectangle(cX + 260, cY, 130, 130));

            Bitmap ML = Copy(entireScreen, new Rectangle(cX, cY + 130, 130, 130));
            Bitmap MM = Copy(entireScreen, new Rectangle(cX + 130, cY + 130, 130, 130));
            Bitmap MR = Copy(entireScreen, new Rectangle(cX + 260, cY + 130, 130, 130));

            Bitmap LL = Copy(entireScreen, new Rectangle(cX, cY + 260, 130, 130));
            Bitmap LM = Copy(entireScreen, new Rectangle(cX + 130, cY + 260, 130, 130));
            Bitmap LR = Copy(entireScreen, new Rectangle(cX + 260, cY + 260, 130, 130));

            int clickX = cX + 65 - 10;
            int clickY = cY + 65 - 40;
            positions += (clickX) + "," + (clickY) + "|";
            positions += (clickX + 105) + "," + (clickY) + "|";
            positions += (clickX + 210) + "," + (clickY) + "|";

            positions += (clickX) + "," + (clickY + 105) + "|";
            positions += (clickX + 105) + "," + (clickY + 105) + "|";
            positions += (clickX + 210) + "," + (clickY + 105) + "|";

            positions += (clickX) + "," + (clickY + 210) + "|";
            positions += (clickX + 105) + "," + (clickY + 210) + "|";
            positions += (clickX + 210) + "," + (clickY + 210) + "|";

            File.WriteAllText("pos", positions);


            TL.Save("TL.jpg");
            TM.Save("TM.jpg");
            TR.Save("TR.jpg");

            ML.Save("ML.jpg");
            MM.Save("MM.jpg");
            MR.Save("MR.jpg");

            LL.Save("LL.jpg");
            LM.Save("LM.jpg");
            LR.Save("LR.jpg");
            g.Dispose();
            entireScreen.Dispose();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Python27\python.exe";
            start.Arguments = string.Format("script.py");

            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            start.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = Process.Start(start);
            while (p.HasExited == false)
            {
                Application.DoEvents();
            }
            using (StreamReader r = p.StandardOutput)
            {
                string result = r.ReadToEnd();
                Console.WriteLine("Solved! Results:");
                Console.WriteLine(result);
                Console.ReadLine();
            }
          
        }
    }
}
