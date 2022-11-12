using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace StaticExtensions {

  //[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

  public static class DllExt {

    #region Common Locations 
    /// <summary> General Location to put program data  </summary>
    /// <remarks> C:\ProgramData\MMCommons </remarks>
    public static string MMCommonsFolder() { return "C:\\ProgramData\\MMCommons"; }
    public static string AppExeFolder(){ return MMConLocation() + "\\"; }

    /// <summary> Common location for apps to store program data based on Application.CommonAppDataPath </summary>
    /// <returns> Path to MMCommons folder </returns>
    public static string MMConLocation() {
      string sCommon = Application.CommonAppDataPath;
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\'));
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\'));
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\') + 1);
      return sCommon + "MMCommons";
    }

    public static Boolean HasConsole() {
      try { return Console.WindowHeight > 0; }
      catch { return false;}
    }
    #endregion

    #region object exts
    public static bool IsNull(this object obj){ return (obj == null) || Convert.IsDBNull(obj); }
    public static bool AsBool(this object obj){ return !obj.IsNull() && Convert.ToBoolean(obj); }
    public static string AsString(this object obj){ return Convert.ToString(obj); }
    public static int AsInt(this object obj){
      return int.TryParse(obj.AsString(), out int r) ? r : throw new Exception("failed convert to int " + obj.AsString());
    }
    public static long AsLong(this object obj) {
      return long.TryParse(obj.AsString(), out long r) ? r : throw new Exception("failed convert to long "+ obj.AsString());
    }

    public static DateTime AsDateTime(this object obj) { return Convert.ToDateTime(obj); }
    public static Double AsDouble(this object obj) { return Convert.ToDouble(obj); }
    public static Decimal AsDecimal(this object obj) { return Convert.ToDecimal(obj); }
    public static string AsJson(this object obj) { return JsonConvert.SerializeObject(obj); }
    #endregion

    #region Strings
    public static int ParseCount(this string content, string delims){
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public static string ParseString(this string content, string delims, int take){
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return take >= split.Length ? "" : split[take];
    }
    public static string ParseFirst(this string content, string delims) {
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
    }
    public static string ParseLast(this string content, string delims) {
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return split[split.Length-1];
    }
    public static string ParseReverse(this string content, string delims, string concatString) {
      string result = "";
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      for (int i = split.Length - 1; i >= 0; i--) {
        result += (result == "" ? split[i] : delims + split[i]);
      }
      return result;
    }

    public static string AddQuotes(this string content) { return "\""+content+ "\"";  }
    public static string AddQuote(this string content) { return "'" + content + "'";  }

    public static string RemoveChar(this string content, char CToRemove) { 
      string r = content;
      while (r.IndexOf(CToRemove)>=0) { 
        r =  r.Remove(r.IndexOf(CToRemove), 1);  
      }
      return r;
    }
    public static decimal AsDecimal(this string obj) {      
      return (decimal.TryParse(obj, out decimal r) ? r : throw new Exception("Failed to parse "+obj));      
    }
    public static string LTrim(this string content, char c) { 
      string r = content;
      while((r.Length > 0)&&(r[0]==c)) { 
        r = r.Remove(0,1); 
      }
      return r;
    }
    #endregion 

    #region Date to string 
    public static string AsStrDate(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", x);
    }
    public static string AsStrDateTime12H(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd hh:mm:ss.FFF tt}", x);
    }
    public static string AsStrDateTime24H(this DateTime x) {
      return String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm:ss.FFF}", x);
    }
    public static string AsStrTime(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:h:mm:ss tt}", x);
    }
    public static string AsStrDayHHMM(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy hh:mm}", x);
    }
    public static string AsStrDay(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", x);
    }
    #endregion

    #region Double

    public static int AsInt(this double x){
      return Convert.ToInt32(x);
    }

    public static int AsIntT(this double x){
      return Convert.ToInt32(x.AsStr2P().ParseString(".", 0));
    }

    public static string AsStr2P(this double x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
    }
    public static string AsStr2P(this double x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    public static string AsStr4P(this double x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
    }
    public static string AsStr4P(this double x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    public static string AsStr8P(this double x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
    }

    public static string AsStr8P(this double x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
        
    public static double AsPointsVertical(this double dIn){ return (dIn * 72); }

    public static double AsPointsHorizontal(this double dIn){ return (dIn * 9.72); }

    #endregion

    #region Decimal 
    public static float AsFloat(this decimal x) { 
      return Convert.ToSingle(x);
    }

    public static int AsInt(this decimal x){      
      return Convert.ToInt32(x);
    }

    public static int AsIntT(this decimal x){
      return  Convert.ToInt32(x.AsStr2P().ParseString(".", 0));
    }

    public static string AsStr2P(this decimal x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
    }
    public static string AsStr2P(this decimal x, int toHowManyPlaces, char paddingChar = ' '){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    public static string AsStr4P(this decimal x){      
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
    }
    public static string AsStr4P(this decimal x, int toHowManyPlaces, char paddingChar = ' ') {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(toHowManyPlaces, paddingChar);
      return y;
    }
    public static string AsStr8P(this decimal x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
    }

    public static string AsStr8P(this decimal x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(toHowManyPlaces, paddingChar);
    }

    public static double AsDouble(this decimal x){
      return Convert.ToDouble(x);
    }

    #endregion

    #region cryptish masks 
    /// <summary> Base 64 encodes string variant uses ? as fillers instead of = for inifiles. </summary>
    public static string AsBase64Encoded(this string Text){
      byte[] encBuff = System.Text.Encoding.UTF8.GetBytes(Text);
      return Convert.ToBase64String(encBuff).Replace('=', '?');
    }
    /// <summary> Base 64 decodes string variant uses ? as fillers instead of = for inifiles. </summary>
    public static string AsBase64Decoded(this string Text){
      byte[] decbuff = Convert.FromBase64String(Text.Replace('?', '='));
      string s = System.Text.Encoding.UTF8.GetString(decbuff);
      return s;
    }
    
    public static string AsHexStr(this byte[] byteArray) {
      string outString = "";
      foreach (Byte b in byteArray)
        outString += b.ToString("X2");
      return outString;
    }

    public static byte[] AsByteArray(this string hexString) {
      byte[] returnBytes = new byte[hexString.Length / 2];
      for (int i = 0; i < returnBytes.Length; i++)
        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      return returnBytes;
    }

    public static string AsMD5HashFile(string filePath) {
      using (var md5 = MD5.Create())
      using (var stream = File.OpenRead(filePath))
        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
    }

    #endregion

    #region exceptions
    public static string AsWalkExcTreePath(this Exception e) {
      string sThisExcStr = "e.Msg:" + e.Message ;
      if (e.InnerException != null) {
        sThisExcStr = sThisExcStr + Environment.NewLine + "e.Inn:" + e.InnerException.AsWalkExcTreePath();
      }
      return sThisExcStr;
    }
   
    public static Exception WriteToAppLog(this Exception e, string messageString) {
      try {
        string ToFile = AppExeFolder() + "MMExtLog" + DateTime.Now.AsStrDate().Trim() + ".log";
        (DateTime.Now.AsStrDateTime12H() + ":" + messageString + ":" + e.AsWalkExcTreePath()).WriteToTextFileLine(ToFile);
      } catch { }
      return e;
    }
    public static void WriteAppLog(this string messageString){
      try {
        string ToFile = AppExeFolder() + "MMExtLog" + DateTime.Now.AsStrDate().Trim() + ".log";
        (DateTime.Now.AsStrDateTime12H() + ":" + messageString).WriteToTextFileLine(ToFile);
      } catch { }
    }

    public static string WriteToTextFile(this string stringToWrite, string logFileName){
      try {
        using (StreamWriter w = File.AppendText(logFileName)) { w.Write(stringToWrite); }
      } catch (Exception ee) {
        throw ee.WriteToLogException("");
      }
      return stringToWrite;
    }
    public static string WriteToTextFileLine(this string stringToWrites, string logFileName){
      try {
        using (StreamWriter w = File.AppendText(logFileName)) { w.WriteLine(stringToWrites); }
      } catch (Exception ee) {
        throw ee.WriteToLogException("");
      }
      return stringToWrites;
    }
    public static Exception WriteToLogException(this Exception e, string logFileNamePart){
      try {
        string ToFile = AppExeFolder() + logFileNamePart + DateTime.Now.AsStrDate().Trim() + ".log";
        e.AsWalkExcTreePath().WriteToTextFileLine(ToFile);
      } catch { }
      return e;
    }

    #endregion

    #region Images
    public static Regex r = new Regex(":");
    /// <summary>
    /// Retrieves the datetime without loading the whole image
    /// </summary>
    /// <param name="path"></param>
    /// <returns>DateTime</returns>
    public static DateTime GetDateTakenFromImage(this string path) {
      using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      using (Image myImage = Image.FromStream(fs, false, false)) {
        PropertyItem propItem = myImage.GetPropertyItem(36867);
        string dateTaken = r.Replace(System.Text.Encoding.UTF8.GetString(propItem.Value), "-", 2);
        return DateTime.Parse(dateTaken);
      }
    }
    /// <summary>
    /// GetColors by Matt Meents, creates const foreach ARGB and then sum out the colors...
    /// </summary>
    /// <returns>Array of colors with HowMany in between A and B </returns>
    public static Color[] GetColors(Color fromColor, Color toColor, int howMany) {
      List<Color> aRet = new List<Color> {fromColor};
      if (howMany > 0) {
        int iCount = 0;        
        int sA = (toColor.A - fromColor.A) / (howMany + 1);
        int sR = (toColor.R - fromColor.R) / (howMany + 1);
        int sG = (toColor.G - fromColor.G) / (howMany + 1);
        int sB = (toColor.B - fromColor.B) / (howMany + 1);
        int AA = fromColor.A; int AR = fromColor.R; int AG = fromColor.G; int AB = fromColor.B;        
        while (iCount < howMany) {
          AA += sA; AR += sR; AG += sG; AB += sB;
          if (AA > 255) AA = 255;  if (AA < 0) AA = 0;
          if (AR > 255) AR = 255;  if (AR < 0) AR = 0;
          if (AG > 255) AG = 255;  if (AG < 0) AG = 0;
          if (AB > 255) AB = 255;  if (AB < 0) AB = 0;
          aRet.Add(Color.FromArgb(AA, AR, AG, AB));
          iCount++;
        }
      }
      aRet.Add(toColor);
      return aRet.ToArray();
    }

    #endregion


    }

}
