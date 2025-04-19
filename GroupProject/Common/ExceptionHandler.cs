using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    /// <summary>
    /// Class which handles exceptions for top-level methods
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// The filePath of the .txt file where the error log is stored
        /// </summary>
        private string filePath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorFilePath">The file path to the .txt file wher the error log is tored</param>
        public ExceptionHandler(string errorFilePath) { filePath = errorFilePath; }

        /// <summary>
        /// Method which handles exceptions for top-level methods
        /// </summary>
        /// <param name="sClass">The class whose method is processing the error</param>
        /// <param name="sMethod">The method which is calling HandleError</param>
        /// <param name="sMessage">The error message to write to the target file</param>
        public void HandleError(string sClass, string sMethod, string sMessage)
        {
            System.IO.File.AppendAllText(filePath, Environment.NewLine + sClass + " " + sMethod + " " + sMessage);
        }
    }
}