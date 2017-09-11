using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace rainbow.Web.Core
{
    /// <summary>
    /// 验证码生成类
    /// </summary>
    public class verify_code : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int codeW = 120;
            int codeH = 22;
            int fontSize = 16;
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.DarkBlue };
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random r = new Random();
            for (var i = 0; i < 6; i++)
            {
                chkCode += character[r.Next(character.Length)];
            }
            context.Session["code"] = chkCode;
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画躁线
            for (int i = 0; i < 1; i++)
            {
                int x1 = r.Next(codeW);
                int y1 = r.Next(codeH);
                int x2 = r.Next(codeW);
                int y2 = r.Next(codeH);
                Color c = color[r.Next(color.Length)];
                g.DrawLine(new Pen(c), x1, y1, x2, y2);
            }
            //画躁点
            for (var i = 0; i < 100; i++)
            {
                int x = r.Next(bmp.Width);
                int y = r.Next(bmp.Height);
                Color clr = color[r.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }
            //画验证码字符串
            for (var i = 0; i < chkCode.Length; i++)
            {
                string fn = font[r.Next(font.Length)];
                Font f = new Font(fn, fontSize);
                Color c = color[r.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), f, new SolidBrush(c), (float)i * 18 + 2, (float)0);
            }
            //清除该页缓存
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Png";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}