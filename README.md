# StaticExtensions
- Extensions for all
```c#
     
    public static string AppExeFolder(){ return MMConLocation() + "\\"; }
    public static string MMConLocation()  -- wrapper around Application.CommonAppDataPath backing up and adding "MMCommons";
    
    
    #region object exts

    public static Boolean isNull(this object aObj)
    public static bool toBoolean(this object aObj)
    public static string toString(this object aObj)
    public static Int32 toInt32(this object aObj)
    public static DateTime toDateTime(this object aObj)
    public static Double toDouble(this object aObj)
    public static Decimal toDecimal(this object aObj)

    #endregion

    #region Parse strings
    
    public static int ParseCount(this string content, string delims)
    public static string ParseString(this string content, string delims, int take)
    public static string ParseFirst(this string content, string delims)
    public static string ParseLast(this string content, string delims)
    public static string ParseReverse(this string content, string delims, string concatString)

    #endregion 

    #region Date to string with popular time formats. 
    
    public static string toStrDate(this DateTime x)
    public static string toStrDateTime(this DateTime x)
    public static string toStrTime(this DateTime x)
    public static string ToStrDateMM(this DateTime x)
    public static string toStrDay(this DateTime x)

    #endregion

    #region Double

    public static Int32 toInt32(this double x)
    public static Int32 toInt32T(this double x)
    public static string toStr2(this double x)
    public static string toStr2P(this double x, Int32 iDigitToPad)
    public static string toStr4(this double x)
    public static string toStr4P(this double x, Int32 iDigitToPad)
    public static string toStr8(this double x)
    public static string toStr8P(this double x, Int32 iDigitToPad)
    public static decimal toDecimal(this double x)
    public static double toPointsVertical(this double dIn)
    public static double toPointsHorizontal(this double dIn)

    #endregion

    #region Decimal
    
    public static float toFloat(this decimal x) 
    public static Int32 toInt32(this decimal x)
    public static Int32 toInt32T(this decimal x)
    public static string toStr2(this decimal x)
    public static string toStr2P(this decimal x, Int32 iDigitToPad)
    public static string toStr4(this decimal x)
    public static string toStr4P(this decimal x, Int32 iDigitToPad)
    public static string toStr8(this decimal x)
    public static string toStr8P(this decimal x, Int32 iDigitToPad)
    public static double toDouble(this decimal x)

    #endregion

    #region cryptish masks 
    
    // variant uses ? as fillers instead of = for base64 in inifiles.
    public static string toBase64EncryptStr(this string Text)
    public static string toBase64DecryptStr(this string Text)

    public static Int32 toSum(this byte[] value)
    public static string toHexStr(this byte[] byteArray) 
    public static byte[] toByteArray(this string hexString)
    public static string toFiletoMD5(string filePath) 

    #endregion

    #region exceptions
    
    public static string toWalkExcTreePath(this Exception e)   
    public static Exception toAppLog(this Exception e, string sLocation)
    public static void toAppLog(this string sLine)
    public static string toTextFile(this string sMsg, string sLogFileName)
    public static string toTextFileLine(this string sMsg, string sLogFileName)
    public static Exception toLogException(this Exception e, string sLogName)

    #endregion

    #region Images

    public static Regex r = new Regex(":");
    
    //retrieves the datetime WITHOUT loading the whole image
    public static DateTime GetDateTakenFromImage(this string path) 

    // GetColors by Matt Meents, creates const foreach ARGB and then sum out the colors...
    public static Color[] GetColors(Color A, Color B, int HowMany)

    #endregion
```
