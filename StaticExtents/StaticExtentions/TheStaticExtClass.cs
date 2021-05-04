using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace StaticExtentions {

  public static class DllExt {

    #region Common Locations 
    public static string MMCommonsFolder() { return "C:\\ProgramData\\MMCommons"; }
    public static string AppExeFolder(){ return MMConLocation() + "\\"; }
    public static string MMConLocation() {
      string sCommon = Application.CommonAppDataPath;
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\'));
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\'));
      sCommon = sCommon.Substring(0, sCommon.LastIndexOf('\\') + 1);
      return sCommon + "MMCommons";
    }
    #endregion

    #region object exts
    public static Boolean isNull(this object aObj){
      Boolean isItNull = (aObj == null);
      if (!isItNull) {
        isItNull = Convert.IsDBNull(aObj);
      }
      return isItNull;
    }
    public static bool toBoolean(this object aObj){
      if (!aObj.isNull()) {
        return Convert.ToBoolean(aObj);
      } else {
        return false;
      }
    }
    public static string toString(this object aObj){
      return Convert.ToString(aObj);
    }
    public static Int32 toInt32(this object aObj){
      Int32 r = -1;
      if (Int32.TryParse(aObj.toString(), out r)) {
        return r;
      } else {
        return -1;
      }
    }
    public static DateTime toDateTime(this object aObj) {
      DateTime aOut = Convert.ToDateTime(aObj);
      return aOut;
    }
    public static Double toDouble(this object aObj) { 
      return Convert.ToDouble(aObj);  
    }
    public static Decimal toDecimal(this object aObj) {
      return Convert.ToDecimal(aObj);
    }
    #endregion

    #region Parse strings
    public static int ParseCount(this string content, string delims){
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public static string ParseString(this string content, string delims, int take){
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return (take >= split.Length ? "" : split[take]);
    }
    public static string ParseFirst(this string content, string delims) {
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
    }
    public static string ParseLast(this string content, string delims) {
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return split[split.Length-1];
      //return content.ParseString(delims, content.ParseCount(delims) - 1);
    }

    public static string ParseReverse(this string content, string delims, string concatString) {
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return split[split.Length-1] + concatString +split[0];
    }
    #endregion 

    #region Date to string 
    public static string toStrDate(this DateTime x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", x);
      return y;
    }
    public static string toStrDateTime(this DateTime x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd hh:mm:ss.FFF tt}", x);
      return y;
    }
    public static string toStrTime(this DateTime x){
      string y = String.Format(CultureInfo.InvariantCulture, "{hh:mm:ss}", x);
      return y;
    }
    public static string ToStrDateMM(this DateTime x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy hh:mm}", x);
      return y;
    }
    public static string toStrDay(this DateTime x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", x);
      return y;
    }
    #endregion

    #region Double

    public static Int32 toInt32(this double x){
      Int32 y = Convert.ToInt32(x);  // rounds
      return y;
    }

    public static Int32 toInt32T(this double x){
      Int32 y = Convert.ToInt32(x.toStr2().ParseString(".", 0));
      return y;
    }

    public static string toStr2(this double x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
      return y;
    }
    public static string toStr2P(this double x, Int32 iDigitToPad){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr4(this double x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
      return y;
    }
    public static string toStr4P(this double x, Int32 iDigitToPad){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr8(this double x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
      return y;
    }

    public static string toStr8P(this double x, Int32 iDigitToPad){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }

    public static decimal toDecimal(this double x){
      decimal y = Convert.ToDecimal(x);
      return y;
    }

    public static double toPointsVertical(this double dIn){
      return (dIn * 72);
    }

    public static double toPointsHorizontal(this double dIn){
      return (dIn * 9.72);
    }

    #endregion

    #region Decimal 
    public static float toFloat(this decimal x) { 
      return Convert.ToSingle(x);
    }

    public static Int32 toInt32(this decimal x){
      Int32 y = Convert.ToInt32(x);
      return y;
    }

    public static Int32 toInt32T(this decimal x){
      Int32 y = Convert.ToInt32(x.toStr2().ParseString(".", 0));
      return y;
    }

    public static string toStr2(this decimal x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);
      return y;
    }
    public static string toStr2P(this decimal x, Int32 iDigitToPad){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr4(this decimal x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x);
      return y;
    }
    public static string toStr4P(this decimal x, Int32 iDigitToPad){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.0000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }
    public static string toStr8(this decimal x){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x);
      return y;
    }

    public static string toStr8P(this decimal x, Int32 iDigitToPad){
      string y = String.Format(CultureInfo.InvariantCulture, "{0:0.00000000}", x).PadLeft(iDigitToPad, ' ');
      return y;
    }

    public static double toDouble(this decimal x){
      double y = Convert.ToDouble(x);
      return y;
    }

    #endregion

    #region cryptish masks 
    // variant uses ? as fillers instead of = for base64 in inifiles.
    public static string toBase64EncryptStr(this string Text){
      byte[] encBuff = System.Text.Encoding.UTF8.GetBytes(Text);
      return Convert.ToBase64String(encBuff).Replace('=', '?');
    }
    public static string toBase64DecryptStr(this string Text){
      byte[] decbuff = Convert.FromBase64String(Text.Replace('?', '='));
      string s = System.Text.Encoding.UTF8.GetString(decbuff);
      return s;
    }
    public static Int32 toSum(this byte[] value) {
      Int32 r=0;
      foreach(byte b in value) r += b.toInt32();
      return r;
    }
    public static string toHexStr(this byte[] byteArray) {
      string outString = "";
      foreach (Byte b in byteArray)
        outString += b.ToString("X2");
      return outString;
    }

    public static byte[] toByteArray(this string hexString) {
      byte[] returnBytes = new byte[hexString.Length / 2];
      for (int i = 0; i < returnBytes.Length; i++)
        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      return returnBytes;
    }

    public static string toFiletoMD5(string filePath) {
      using (var md5 = MD5.Create())
      using (var stream = File.OpenRead(filePath))
        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
    }

    #endregion

    #region exceptions
    public static string toWalkExcTreePath(this Exception e) {
      string sThisExcStr = "e.Msg:" + e.Message + Environment.NewLine;
      if (e.InnerException != null) {
        sThisExcStr = sThisExcStr + "e.Inn:" + e.InnerException.toWalkExcTreePath();
      }
      return sThisExcStr;
    }
   
    public static Exception toAppLog(this Exception e, string sLocation){
      try {
        string ToFile = AppExeFolder() + "MMExtLog" + DateTime.Now.toStrDate().Trim() + ".log";
        (DateTime.Now.toStrDateTime() + ":" + sLocation + ":" + e.toWalkExcTreePath()).toTextFileLine(ToFile);
      } catch { }
      return e;
    }
    public static void toAppLog(this string sLine){
      try {
        string ToFile = AppExeFolder() + "MMExtLog" + DateTime.Now.toStrDate().Trim() + ".log";
        (DateTime.Now.toStrDateTime() + ":" + sLine).toTextFileLine(ToFile);
      } catch { }
    }

    public static string toTextFile(this string sMsg, string sLogFileName){
      try {
        using (StreamWriter w = File.AppendText(sLogFileName)) { w.Write(sMsg); }
      } catch (Exception ee) {
        throw ee.toLogException("");
      }
      return sMsg;
    }
    public static string toTextFileLine(this string sMsg, string sLogFileName){
      try {
        using (StreamWriter w = File.AppendText(sLogFileName)) { w.WriteLine(sMsg); }
      } catch (Exception ee) {
        throw ee.toLogException("");
      }
      return sMsg;
    }
    public static Exception toLogException(this Exception e, string sLogName){
      try {
        string ToFile = AppExeFolder() + "CObjectLog" + DateTime.Now.toStrDate().Trim() + ".log";
        e.toWalkExcTreePath().toTextFileLine(ToFile);
      } catch { }
      return e;
    }

    #endregion

    #region Images
    public static Regex r = new Regex(":");
    //retrieves the datetime WITHOUT loading the whole image
    public static DateTime GetDateTakenFromImage(this string path) {
      using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      using (Image myImage = Image.FromStream(fs, false, false)) {
        PropertyItem propItem = myImage.GetPropertyItem(36867);
        string dateTaken = r.Replace(System.Text.Encoding.UTF8.GetString(propItem.Value), "-", 2);
        return DateTime.Parse(dateTaken);
      }
    }
    // GetColors by Matt Meents, creates const foreach ARGB and then sum out the colors...
    public static Color[] GetColors(Color A, Color B, int HowMany) {
      List<Color> aRet = new List<Color>();
      aRet.Add(A);
      if (HowMany > 0) {
        Int32 iCount = 0;        
        int sA = (B.A - A.A) / (HowMany + 1);
        int sR = (B.R - A.R) / (HowMany + 1);
        int sG = (B.G - A.G) / (HowMany + 1);
        int sB = (B.B - A.B) / (HowMany + 1);
        int AA = A.A; int AR = A.R; int AG = A.G; int AB = A.B;        
        while (iCount < HowMany) {
          AA += sA; AR += sR; AG += sG; AB += sB;
          if (AA > 255) AA = 255;  if (AA < 0) AA = 0;
          if (AR > 255) AR = 255;  if (AR < 0) AR = 0;
          if (AG > 255) AG = 255;  if (AG < 0) AG = 0;
          if (AB > 255) AB = 255;  if (AB < 0) AB = 0;
          aRet.Add(Color.FromArgb(AA, AR, AG, AB));
          iCount++;
        }
      }
      aRet.Add(B);
      return aRet.ToArray();
    }

    #endregion


    }

}
