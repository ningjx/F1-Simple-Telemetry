using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace F1Tools
{
    public static class Helper
    {
        public static string GetIP()
        {
            string hostName = Dns.GetHostName();//本机名   
            var addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (var ip in addressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.MapToIPv4().ToString();
                }
            }
            return string.Empty;
        }

        public static string GetLocalIP()
        {
            string result = RunApp("route", "print", true);
            Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
            if (m.Success)
            {
                return m.Groups[2].Value;
            }
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                    c.Connect("www.baidu.com", 80);
                    string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                    c.Close();
                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static string RunApp(string filename, string arguments, bool recordLog)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    Thread.Sleep(100);

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                    string txt = sr.ReadToEnd();
                    sr.Close();
                    if (recordLog)
                        Trace.WriteLine(txt);
                    return txt;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static bool CheckUpdate()
        {
            try
            {
                var client = new HttpClient();
                using (var request = new HttpRequestMessage(HttpMethod.Get, "https://gitee.com/n-i-n-g/F1-2020-Telemetering-Tools/raw/master/WpfApp1/Properties/AssemblyInfo.cs"))
                {
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var dataString = HttpUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
                        if (!string.IsNullOrEmpty(dataString))
                        {
                            var re = new Regex(@"(?<=AssemblyVersion\("")\d\.\d\.\d\.\d(?<!\""\))");
                            var version = re.Match(dataString).Value;
                            if (!string.IsNullOrEmpty(version))
                            {
                                var items = version.Split('.');
                                var curVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                                var newVer = new Version(Convert.ToInt32(items[0]), Convert.ToInt32(items[1]), Convert.ToInt32(items[2]), Convert.ToInt32(items[3]));
                                return newVer > curVer;
                            }
                        }
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public static ulong GetCpuID(int idx)
        {
            ulong cpuid = 0;
            if (idx < 0 || idx >= System.Environment.ProcessorCount)
            {
                idx = 0;
            }
            cpuid |= 1UL << idx;
            return cpuid;
        }
    }
}
