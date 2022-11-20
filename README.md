# StaticExtensions
- Extensions for all
```c#

    public static string MMCommonsFolder() { return "C:\\ProgramData\\MMCommons"; }
    public static string AppExeFolder(){ return MMConLocation() + "\\"; }
    public static string MMConLocation()  -- wrapper around Application.CommonAppDataPath backing up and adding "MMCommons";
    
    #region object exts

    public static bool IsNull(this object obj)
    public static bool AsBool(this object obj)
    public static string AsString(this object obj)
    public static int AsInt(this object obj)
    public static long AsLong(this object obj)
    public static DateTime AsDateTime(this object obj)
    public static Double AsDouble(this object obj)
    public static Decimal AsDecimal(this object obj)
    public static string AsJson(this object obj)

    #region Parse strings
    
    public static int ParseCount(this string content, string delims)
    public static string ParseString(this string content, string delims, int take)
    public static string ParseFirst(this string content, string delims)
    public static string ParseLast(this string content, string delims)
    public static string ParseReverse(this string content, string delims, string concatString)

    public static string AddQuotes(this string content) 
    public static string AddQuote(this string content)
    public static string RemoveChar(this string content, char CToRemove) 

    public static decimal AsDecimal(this string obj)
    public static string LTrim(this string content, char c) 

    #region Date to string with popular time formats. 
    
    public static string AsStrDate(this DateTime x)
    public static string AsStrDateTime12H(this DateTime x)
    public static string AsStrDateTime24H(this DateTime x)
    public static string AsStrTime(this DateTime x)
    public static string AsStrDateHHMM(this DateTime x)
    public static string AsStrDay(this DateTime x)

    #region Double

    public static Int32 AsInt32(this double x)
    public static Int32 AsInt32T(this double x)
    public static string AsStr2P(this double x)
    public static string AsStr2P(this double x, Int32 toHowManyPlaces, char paddingChar = ' ')
    public static string AsStr4P(this double x)
    public static string AsStr4P(this double x, Int32 toHowManyPlaces, char paddingChar = ' ')
    public static string AsStr8P(this double x)
    public static string AsStr8P(this double x, Int32 toHowManyPlaces, char paddingChar = ' ')
    public static decimal AsDecimal(this double x)
    public static double AsPointsVertical(this double dIn)
    public static double AsPointsHorizontal(this double dIn)

    #region Decimal
    
    public static float AsFloat(this decimal x) 
    public static int AsInt(this decimal x)
    public static int AsIntT(this decimal x)
    public static string AsStr2P(this decimal x)
    public static string AsStr2P(this decimal x, Int32 toHowManyPlaces, char paddingChar = ' ')
    public static string AsStr4P(this decimal x)
    public static string AsStr4P(this decimal x, Int32 toHowManyPlaces, char paddingChar = ' ')
    public static string AsStr8P(this decimal x)
    public static string AsStr8P(this decimal x, Int32 toHowManyPlaces, char paddingChar = ' ')
    public static double AsDouble(this decimal x)

    #region cryptish masks 
    
    // variant uses ? as fillers instead of = for base64 in inifiles.
    public static string AsBase64Encoded(this string Text)
    public static string AsBase64Decoded(this string Text)
        
    public static string AsHexStr(this byte[] byteArray) 
    public static byte[] AsByteArray(this string hexString)
    public static string AsFiletoMD5(string filePath) 

     
    public static string AsWalkExcTreePath(this Exception e)   
    public static Exception WriteAppLog(this Exception e, string messageString)
    public static void WriteToAppLog(this string messageString)
    public static string WriteToTextFile(this string stringToWrite, string logFileName)
    public static string WriteToTextFileLine(this string stringToWrite, string logFileName)
    public static Exception WriteToLogException(this Exception e, string logFileNamePart)

   
    // GetColors by Matt Meents, creates const foreach ARGB and then sum out the colors...
    public static Color[] GetColors(Color fromColor, Color toColor, int howMany)

  
```
