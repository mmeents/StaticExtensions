using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using Newtonsoft.Json;

namespace StaticExtensions {

  
  /// <summary>
  /// Common static extension library class DLLExt
  /// </summary>
  public static class DllExt {
    #region Common Locations 
    /// <summary> General Location to put program data  </summary>
    /// <remarks> string C:\ProgramData\MMCommons </remarks>
    public static string MMCommonsFolder() { return "C:\\ProgramData\\MMCommons"; }
    /// <summary> General Location to put program data  </summary>
    /// <remarks> string C:\ProgramData\MMCommons\ via MMConLocation </remarks>
    public static string AppExeFolder(){ return MMConLocation() + "\\"; }
    /// <summary> Common location for apps to store program data based on Application.CommonAppDataPath </summary>
    /// <returns> string C:\ProgramData\MMCommons Path to MMCommons folder </returns>
    public static string MMConLocation() {      
      string sCommon = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      sCommon = sCommon[..sCommon.LastIndexOf('\\')];
      sCommon = sCommon[..sCommon.LastIndexOf('\\')];
      sCommon = sCommon[..(sCommon.LastIndexOf('\\') + 1)];
      return sCommon + "MMCommons";
    }

    /// <summary> Attempts to check WindowHeight > 0 catches any errors </summary>
    /// <returns> bool </returns>
    public static bool HasConsole() {
      try { return Console.WindowHeight > 0; }
      catch { return false; }
    }
    #endregion

    #region object exts
    /// <summary> Check object is null </summary>
    /// <returns> bool </returns>
    public static bool IsNull(this object? obj){ return (obj == null) || Convert.IsDBNull(obj); }
    /// <summary> Casts object as bool, null is false </summary>
    /// <returns> bool </returns>
    public static bool? AsBool(this object? obj){ return obj.IsNull() ? null : Convert.ToBoolean(obj); }
    /// <summary> Casts object as string, null is Exception </summary>
    /// <returns> string </returns>
    public static string AsString(this object? obj){ 
      try{ return Convert.ToString(obj) ?? String.Empty; } catch{ return String.Empty; } }
    /// <summary> Casts object as int, null is Exception </summary>
    /// <returns> int </returns>
    public static int? AsInt(this object? obj){
      return int.TryParse(obj.AsString(), out int r) ? r : null;
    }
    /// <summary> Casts object as long, null is Exception </summary>
    /// <returns> long </returns>
    public static long? AsLong(this object? obj) {
      return long.TryParse(obj.AsString(), out long r) ? r : null;
    }
    /// <summary> Casts object as DateTime, null is Exception </summary>
    /// <returns> DateTime </returns>    
    public static DateTime? AsDateTime(this object? obj) { return obj == null ? null : Convert.ToDateTime(obj); }
    /// <summary> Casts object as double, null is Exception </summary>
    /// <returns> double </returns>    
    public static double? AsDouble(this object? obj) { return obj == null ? null : Convert.ToDouble(obj); }
    /// <summary> Casts object as decimal, null is Exception </summary>
    /// <returns> decimal </returns>    
    public static decimal? AsDecimal(this object? obj) { return obj == null ? null : Convert.ToDecimal(obj); }
    #endregion

    #region Strings
    /// <summary> Splits content by delims and count</summary>
    /// <returns> int </returns>    
    public static int ParseCount(this string content, string delims){
      return content.Parse(delims).Length;
    }
    /// <summary> Splits contents by delims into an array and returns item at take </summary>
    /// <returns> string </returns>
    public static string ParseString(this string content, string delims, int take){
      string[] split = content.Parse(delims);
      return take >= split.Length ? "" : split[take];
    }
    /// <summary> Splits contents by delims and takes first</summary>
    /// <returns> string </returns>
    public static string ParseFirst(this string content, string delims) {
      return content.Parse(delims)[0];
    }
    /// <summary> Splits contents by delims and takes last</summary>
    /// <returns> string </returns>
    public static string ParseLast(this string content, string delims) {
      return content.Parse(delims)[^1];      
    }
    /// <summary> Pasrse String by delim characters return the array result. </summary>    
    /// <returns> string array </returns>
    public static string[] Parse(this string content, string delims) {       
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
     }
    /// <summary> Splits contents by delims and concats adding concatString in middle in reverse order</summary>
    /// <returns> string </returns>
    public static string ParseReverse(this string content, string delims, string concatString) {
      string result = "";
      string[] split = content.Parse(delims);
      for (int i = split.Length - 1; i >= 0; i--) {
        result += (result == "" ? split[i] : concatString + split[i]);
      }
      return result;
    }

    /// <summary> Adds Double Quotes around content</summary>
    /// <returns> string </returns>
    public static string AddQuotes(this string content) { return "\""+content+ "\"";  }
    /// <summary> Adds Single Quotes around content</summary>
    /// <returns> string </returns>
    public static string AddQuote(this string content) { return "'" + content + "'";  }
    /// <summary> Remove all instances of CToRemove from content</summary>
    /// <returns> string </returns>
    public static string RemoveChar(this string content, char CToRemove) { 
      string r = content;
      while (r.Contains(CToRemove)) { 
        r =  r.Remove(r.IndexOf(CToRemove), 1);  
      }
      return r;
    }
    /// <summary> Converts String to decimal</summary>
    /// <returns> decimal </returns>
    public static decimal AsDecimal(this string obj) {      
      return (decimal.TryParse(obj, out decimal r) ? r : throw new Exception("Failed to parse "+obj));      
    }
    /// <summary> Removes all char c from content[0]</summary>
    /// <returns> string </returns>
    public static string LTrim(this string content, char c) { 
      string r = content;
      while((r.Length > 0)&&(r[0]==c)) { 
        r = r.Remove(0,1); 
      }
      return r;
    }
    /// <summary> Uppercase first letter of content concat with rest of content. </summary>
    /// <param name="content"></param>
    /// <returns> string </returns>
    public static string AsUpperCaseFirstLetter(this string content) {
      return string.Concat(content[..1].ToUpper(), content.AsSpan(1));
    }
    /// <summary> lower case first letter of content concat with remainder. </summary>
    /// <param name="content"></param>
    /// <returns> string </returns>
    public static string AsLowerCaseFirstLetter(this string content) {
      return string.Concat(content[..1].ToLower(), content.AsSpan(1));
    }
    /// <summary> Newtonsoft.Json JsonConvert.SerializeObject(obj) route. </summary>
    /// <param name="obj"></param>
    /// <returns> null or Json string of object</returns>
    public static string? AsJsonString(this object obj) {
      if (obj.IsNull()) { 
        return null;
      } else { 
        return JsonConvert.SerializeObject(obj);
      }
    }
    /// <summary> Newtonsoft.Json JsonConvert.DeserializeObject(obj) route. </summary>
    /// <param name="jsonString"></param>
    /// <returns>null or Object from DeserializeObject</returns>
    public static object? AsFromJsonString(this string? jsonString) { 
      if (jsonString==null) {
        return null;
      } else { 
        return JsonConvert.DeserializeObject(jsonString);
      }
    }


    #endregion

    #region Date to string 
    /// <summary> Day to string Sortable yyyy-MM-dd</summary>
    /// <returns> string </returns>
    public static string AsStrDate(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", x);
    }
    /// <summary> DateTime to string yyyy-MM-dd hh:mm:ss.FFF tt </summary>
    /// <returns> string </returns>
    public static string AsStrDateTime12H(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd hh:mm:ss.FFF tt}", x);
    }
    /// <summary> DateTime to string yyyy-MM-dd HH:mm:ss.FFF</summary>
    /// <returns> string </returns>
    public static string AsStrDateTime24H(this DateTime x) {
      return String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm:ss.FFF}", x);
    }
    /// <summary> DateTime to string time h:mm:ss tt</summary>
    /// <returns> string </returns>
    public static string AsStrTime(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:h:mm:ss tt}", x);
    }
    /// <summary> DateTime to string Day Time MM/dd/yyyy hh:mm</summary>
    /// <returns> string </returns>
    public static string AsStrDayHHMM(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy hh:mm}", x);
    }
    /// <summary> DateTime to string Day MM/dd/yyyy</summary>
    /// <returns> string </returns>
    public static string AsStrDay(this DateTime x){
      return String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", x);
    }

    #endregion

    #region Double
    /// <summary> Convert double to int via Convert</summary>
    /// <returns> int </returns>
    public static int AsInt(this double x){
      return Convert.ToInt32(x);
    }
    /// <summary> Convert double to int via cast to string and cast trunc string to int. </summary>
    /// <returns> int </returns>
    public static int AsIntT(this double x){
      return Convert.ToInt32(x.AsStr2P().ParseString(".", 0));
    }
    /// <summary> Cast double to 2 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr2P(this double x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
    }
    /// <summary> Cast double to 2 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr2P(this double x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    /// <summary> Cast double to 4 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr4P(this double x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
    }
    /// <summary> Cast double to 4 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr4P(this double x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    /// <summary> Cast double to 8 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr8P(this double x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
    }
    /// <summary> Cast double to 8 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>

    public static string AsStr8P(this double x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    /// <summary> Common constant when converting pixels to points</summary>
    /// <returns> double </returns>    
    public static double AsPointsVertical(this double dIn){ return (dIn * 72); }
    /// <summary> Common constant when converting pixels to points</summary>
    /// <returns> double </returns>
    public static double AsPointsHorizontal(this double dIn){ return (dIn * 9.72); }

    #endregion

    #region Decimal 
    /// <summary> Converts decimal to float </summary>
    /// <returns> float </returns>
    public static float AsFloat(this decimal x) { 
      return Convert.ToSingle(x);
    }
    /// <summary> Cast decimal to int via Convert</summary>
    /// <returns> int </returns>
    public static int AsInt(this decimal x){      
      return Convert.ToInt32(x);
    }
    /// <summary> Cast decimal to int via covert to string and cast trunc string to int </summary>
    /// <returns> int </returns>
    public static int AsIntT(this decimal x){
      return  Convert.ToInt32(x.AsStr2P().ParseString(".", 0));
    }
    /// <summary> Cast double to 2 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr2P(this decimal x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
    }
    /// <summary> Cast double to 2 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr2P(this decimal x, int toHowManyPlaces, char paddingChar = ' '){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    /// <summary> Cast double to 4 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr4P(this decimal x){      
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
    }
    /// <summary> Cast double to 4 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr4P(this decimal x, int toHowManyPlaces, char paddingChar = ' ') {
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(toHowManyPlaces, paddingChar);
      return y;
    }
    /// <summary> Cast double to 8 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr8P(this decimal x){
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
    }
    /// <summary> Cast double to 8 decimal places, optional pad toHowManyPlace, optional paddingChar default ' '</summary>
    /// <returns> string </returns>
    public static string AsStr8P(this decimal x, int toHowManyPlaces, char paddingChar = ' ') {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(toHowManyPlaces, paddingChar);
    }
    /// <summary> Convert decimal to double </summary>
    /// <returns> string </returns>
    public static double AsDouble(this decimal x){
      return Convert.ToDouble(x);
    }

    #endregion

    #region cryptish masks 
    /// <summary> Base 64 encodes string variant uses ? as fillers instead of = for inifiles. </summary>
    /// <returns> Base64 encoded string </returns>
    public static string AsBase64Encoded(this string Text){
      byte[] encBuff = System.Text.Encoding.UTF8.GetBytes(Text);
      return Convert.ToBase64String(encBuff).Replace('=', '?');
    }
    /// <summary> Base 64 decodes string variant uses converts ? back to = as fillers for inifiles. </summary>
    /// <returns> Base 64 decoded string </returns>
    public static string AsBase64Decoded(this string Text){
      byte[] decbuff = Convert.FromBase64String(Text.Replace('?', '='));
      string s = System.Text.Encoding.UTF8.GetString(decbuff);
      return s;
    }
    /// <summary> Converts Byte Array to Hex String, useful for saving byte[] to string and back via AsByteArray </summary>
    /// <returns> Hex string </returns>    
    public static string AsHexStr(this byte[] byteArray) {
      string outString = "";
      foreach (Byte b in byteArray)
        outString += b.ToString("X2");
      return outString;
    }
    /// <summary> Converts Hex String created by AsHexStr back into Byte Array </summary>
    /// <returns> byte[] </returns>    
    public static byte[] AsByteArray(this string hexString) {
      byte[] returnBytes = new byte[hexString.Length / 2];
      for (int i = 0; i < returnBytes.Length; i++)
        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      return returnBytes;
    }
    /// <summary> Computes hash for file at filePath </summary>
    /// <returns> string Hash </returns>    
    public static string AsMD5HashFile(string filePath) {
      using var md5 = MD5.Create();
      using var stream = File.OpenRead(filePath);
      return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
    }

    #endregion

    #region exceptions
    /// <summary> Recursive sum of error messages via error.InnerException </summary>
    /// <returns> string </returns>    
    public static string AsWalkExcTreePath(this Exception e) {
      string sThisExcStr = "e.Msg:" + e.Message ;
      if (e.InnerException != null) {
        sThisExcStr = sThisExcStr + Environment.NewLine + "e.Inn:" + e.InnerException.AsWalkExcTreePath();
      }
      return sThisExcStr;
    }
    /// <summary> Writes to daily DllExtLog[date].log file messageString and exception chain. </summary>
    /// <returns> Exception for chaining </returns>    
    public static Exception WriteToAppLog(this Exception e, string messageString) {
      try {
        string ToFile = AppExeFolder() + "DllExtLog" + DateTime.Now.AsStrDate().Trim() + ".log";
        (DateTime.Now.AsStrDateTime12H() + ":" + messageString + ":" + e.AsWalkExcTreePath()).WriteToTextFileLine(ToFile);
      } catch { }
      return e;
    }
    /// <summary> WriteLine to daily DllExtLog[date].log file messageString </summary>
    /// <returns> none void </returns>    
    public static void WriteAppLog(this string messageString){
      try {
        string ToFile = AppExeFolder() + "DllExtLog" + DateTime.Now.AsStrDate().Trim() + ".log";
        (DateTime.Now.AsStrDateTime12H() + ":" + messageString).WriteToTextFileLine(ToFile);
      } catch { }
    }
    /// <summary> WriteLine to daily DllExtLog[date].log file messageString </summary>
    /// <returns> none void </returns>    
    public static string WriteToTextFile(this string stringToWrite, string logFileName){
      try {
        using StreamWriter w = File.AppendText(logFileName);
        w.Write(stringToWrite);
      } catch (Exception ee) {
        throw ee.WriteToLogException("");
      }
      return stringToWrite;
    }
    /// <summary> WriteLine to daily DllExtLog[date].log file messageString </summary>
    /// <returns> none void </returns>    
    public static string WriteToTextFileLine(this string stringToWrites, string logFileName){
      try {
        using StreamWriter w = File.AppendText(logFileName);
        w.WriteLine(stringToWrites);
      } catch (Exception ee) {
        throw ee.WriteToLogException("");
      }
      return stringToWrites;
    }
    /// <summary> WriteLine to daily logFileNamePart[date].log file the Exception </summary>
    /// <returns> the Exception for chaining </returns>    
    public static Exception WriteToLogException(this Exception e, string logFileNamePart){
      try {
        string ToFile = AppExeFolder() + logFileNamePart + DateTime.Now.AsStrDate().Trim() + ".log";
        e.AsWalkExcTreePath().WriteToTextFileLine(ToFile);
      } catch { }
      return e;
    }

    #endregion

    #region Images
    /// <summary>
    /// GetColors, finds howMany colors inbetween fromColor and toColor 
    /// </summary>
    /// <returns>Array howMany of colors in between fromColor and toColor </returns>
    public static Color[] GetColors(Color fromColor, Color toColor, int howMany) {
      List<Color> aRet = new() { fromColor};
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
