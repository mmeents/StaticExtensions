using System;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;

namespace StaticExtentions {

  public static class DllExt {
    public static string MMCommonsFolder() { return "C:\\ProgramData\\MMCommons"; }
    public static string AppExeFolder(){ return MMCommonsFolder() + "\\"; }
    public static string MMConLocation(){ return AppExeFolder();}

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

    #region Parse strings
    public static int ParseCount(this string content, string delims){
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public static string ParseString(this string content, string delims, int take){
      string[] split = content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      return (take >= split.Length ? "" : split[take]);
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

    public static string toFiletoMD5(string filePath){
      using (var md5 = MD5.Create())
      using (var stream = File.OpenRead(filePath))
        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
    }
    
  }

}
