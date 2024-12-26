using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Common.Helpers
{
    public static class Logger
    {
        static readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static readonly string pathRoot = Path.Combine(desktopPath,"FlowerShop");
        private static readonly object lockLog = new object();
        public static void LogMsg(string msg)
        {
            lock(lockLog){
                try
                {
                    // Lấy ngày hiện tại dưới dạng "yyyy-MM-dd"
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                    // Tạo đường dẫn thư mục cho ngày hiện tại
                    string dailyFolder = Path.Combine(pathRoot, currentDate);

                    // Kiểm tra và tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(dailyFolder))
                    {
                        Directory.CreateDirectory(dailyFolder);
                    }
                    string path = Path.Combine(dailyFolder, "log.txt");
                    using (FileStream fStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fStream))
                        {
                            sw.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
                        }
                    }
                }
                catch { }
            }
        }
    }
}
