using UnityEngine;
using System.IO;

//Writes the string fed into the class to a text file
public static class writeToLog
{
    static string path = Application.persistentDataPath; //The path to the logFile
    static StreamWriter writer; //A new streamwriter (allows the code to write to the file)

    public static void makeLogFile(string scene)
    {
        System.DateTime dt = System.DateTime.Now;
        string dateTime = dt.ToString("yyyy-MM-dd\\THH-mm-ss\\Z");
        //File.CreateText("log_" + randomInt);
        writer = new StreamWriter(path + "/log_"+scene+"_" + dateTime, true);
        Debug.Log(path + "log_" +scene +"_"+ dateTime);
    }

    //writes a new log message to the log
    public static void WriteString(string logMessage)
    {
        writer.WriteLine(logMessage);
    }

    //writes the final log message to the log and then closes the streamWriter so that the file can't be edited anymore
    public static void closeLog(string logMessage)
    {
        //Debug.Log(Application.persistentDataPath);
        writer.WriteLine(logMessage);
        writer.Close();
    }

}
