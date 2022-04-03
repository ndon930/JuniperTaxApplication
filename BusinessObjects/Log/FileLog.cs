namespace BusinessObjects.Log
{
    public class FileLog : ILog
    {
        #region properties
        public string Path { get; set; }
        public string Name { get; set; }
        #endregion

        public FileLog(string path, string filename)
        {
            Path = path;
            Name = filename;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { };
        }


        /// <summary>
        /// <inheritdoc/>
        /// Will create folder for logs based on date
        /// </summary>
        /// <param name="message">Message to Log</param>
        public void LogInfo(string message)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                string pathDateTime = Path + "/" + currentDateTime.ToString("yyyy-MM-dd");
                DirectoryInfo dir = new DirectoryInfo(pathDateTime);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                using StreamWriter sw = new StreamWriter(dir.FullName + "/"+ Name + ".log");
                sw.WriteLine(currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": "+ message);
                sw.Close();
            }
            catch (Exception) { };
        }
    }


}