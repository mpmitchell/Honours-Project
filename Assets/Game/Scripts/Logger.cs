using System.IO;

public class Logger {

  const string fileName = "Log";

  static StreamWriter file;

  public static void OpenFile() {
    int fileNumber = 0;
    for (; File.Exists(fileName + fileNumber + ".txt"); fileNumber++);
    file = File.CreateText(fileName + fileNumber + ".txt");
  }

  public static void Log(string text) {
    if (file == null) {
      OpenFile();
    }
    file.WriteLine(System.DateTime.Now + "\t" + text);
    file.Flush();
  }
}
