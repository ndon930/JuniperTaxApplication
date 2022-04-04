namespace BusinessObjects.Log
{
    /// <summary>
    /// Class to handle logging of messages to file. 
    /// </summary>
    public interface ILog
    {
        #region Properties
        /// <summary>
        /// The path of the log
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// The Name of the file for the log
        /// </summary>
        string Name { get; set; }
        #endregion

        #region methods
        
        #endregion

        #region functions
        /// <summary>
        /// Log to the current file. 
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogInfo(string message);

        /// <summary>
        /// Log Exception to the Error file. 
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogException(string methodName, string message);
        #endregion
    }
}