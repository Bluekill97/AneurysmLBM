using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;

namespace Utilities {
    public class ErrorLog {
        static char winFolderDelimiter = '\\';
        static string[] winForbiddenFileNameChars = new string[] {
            "\\",
            "/",
            ":",
            "*",
            "?",
            "\"",
            "<",
            ">",
            "|"
        };

        // descending order of time units for sorting, milliseconds for most unique filenames
        static string dateTimeFormat = "yyyy.MM.dd-HH.mm.ss.fffffff";

        public static void LogError(Exception e) {
            string fileName = "Error " + DateTime.Now.ToString(dateTimeFormat);
            string data = e.ToString();

            WriteToFile(fileName, data);
        }

        public static void LogTaskError(List<ITerminalTask> tasks) {
            string fileName = "TaskError " + DateTime.Now.ToString(dateTimeFormat);
            string data = "";

            foreach (ITerminalTask task in tasks) {
                data += task.GetID() + " " + task.GetName() + ": \r\n";
                data += task.GetOutput();
                data += "\r\n";
            }

            WriteToFile(fileName, data);
        }

        private static void WriteToFile(string fileName, string data) {
            foreach (string s in winForbiddenFileNameChars)
                fileName.Replace(s, "-");

            string projectPath = Directory.GetCurrentDirectory();
            string logFolderPath = projectPath + winFolderDelimiter + "Log" + winFolderDelimiter;

            if (!Directory.Exists(logFolderPath))
                Directory.CreateDirectory(logFolderPath);

            File.WriteAllText(logFolderPath + fileName + ".txt", data);
        }
    }
}