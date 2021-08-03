// -----------------------------------------------------------------------
// <copyright file="General.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace General
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Security.Cryptography;
    using System.Net;
    using BAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class General
    {



        public const string AFRIKAANS = "af";
        public const string ALBANIAN = "sq";
        public const string AMHARIC = "am";
        public const string ARABIC = "ar";
        public const string ARMENIAN = "hy";
        public const string AZERBAIJANI = "az";
        public const string BASQUE = "eu";
        public const string BELARUSIAN = "be";
        public const string BENGALI = "bn";
        public const string BIHARI = "bh";
        public const string BULGARIAN = "bg";
        public const string BURMESE = "my";
        public const string CATALAN = "ca";
        public const string CHEROKEE = "chr";
        public const string CHINESE = "zh";
        public const string CHINESE_SIMPLIFIED = "zh-CN";
        public const string CHINESE_TRADITIONAL = "zh-TW";
        public const string CROATIAN = "hr";
        public const string CZECH = "cs";
        public const string DANISH = "da";
        public const string DHIVEHI = "dv";
        public const string DUTCH = "nl";
        public const string ENGLISH = "en";
        public const string ESPERANTO = "eo";
        public const string ESTONIAN = "et";
        public const string FILIPINO = "tl";
        public const string FINNISH = "fi";
        public const string FRENCH = "fr";
        public const string GALICIAN = "gl";
        public const string GEORGIAN = "ka";
        public const string GERMAN = "de";
        public const string GREEK = "el";
        public const string GUARANI = "gn";
        public const string GUJARATI = "gu";
        public const string HEBREW = "iw";
        public const string HINDI = "hi";
        public const string HUNGARIAN = "hu";
        public const string ICELANDIC = "is";
        public const string INDONESIAN = "id";
        public const string INUKTITUT = "iu";
        public const string ITALIAN = "it";
        public const string JAPANESE = "ja";
        public const string KANNADA = "kn";
        public const string KAZAKH = "kk";
        public const string KHMER = "km";
        public const string KOREAN = "ko";
        public const string KURDISH = "ku";
        public const string KYRGYZ = "ky";
        public const string LAOTHIAN = "lo";
        public const string LATVIAN = "lv";
        public const string LITHUANIAN = "lt";
        public const string MACEDONIAN = "mk";
        public const string MALAY = "ms";
        public const string MALAYALAM = "ml";
        public const string MALTESE = "mt";
        public const string MARATHI = "mr";
        public const string MONGOLIAN = "mn";
        public const string NEPALI = "ne";
        public const string NORWEGIAN = "no";
        public const string ORIYA = "or";
        public const string PASHTO = "ps";
        public const string PERSIAN = "fa";
        public const string POLISH = "pl";
        public const string PORTUGUESE = "pt-PT";
        public const string PUNJABI = "pa";
        public const string ROMANIAN = "ro";
        public const string RUSSIAN = "ru";
        public const string SANSKRIT = "sa";
        public const string SERBIAN = "sr";
        public const string SINDHI = "sd";
        public const string SINHALESE = "si";
        public const string SLOVAK = "sk";
        public const string SLOVENIAN = "sl";
        public const string SPANISH = "es";
        public const string SWAHILI = "sw";
        public const string SWEDISH = "sv";
        public const string TAJIK = "tg";
        public const string TAMIL = "ta";
        public const string TAGALOG = "tl";
        public const string TELUGU = "te";
        public const string THAI = "th";
        public const string TIBETAN = "bo";
        public const string TURKISH = "tr";
        public const string UKRAINIAN = "uk";
        public const string URDU = "ur";
        public const string UZBEK = "uz";
        public const string UIGHUR = "ug";
        public const string VIETNAMESE = "vi";
        public const string UNKNOWN = "";






        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }


        public static string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }


        public static string RandomString(int Length)
        {
            string ranStr = string.Empty;
            // Character list to generate Random string
            string[] s = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            Random rnd = new Random();

            // If length is less then 4 then make length 4. min. length should be 4.
            if (Length < 4) { Length = 4; }
            for (int i = 0; i < Length; i++)
            {
                // Generate Random string one by one from list of character.
                ranStr = ranStr + s[rnd.Next(1, s.Length)].ToString();
            }
            return ranStr;
        }

        public static string GetRandomPassword(int length)
        {
            char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            string password = string.Empty;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetation of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }

        // This function will remove all html tag/code from string/data.
        public static string RemoveHTML(string Data)
        {
            // Patter for HTML tags
            Regex reg = new Regex("<\\S[^><]*>");
            return reg.Replace(Convert.ToString(Data), string.Empty);
        }


        public static string ReadFile(string FilePath)
        {
            string _Content = string.Empty;
            StreamReader objReader = new StreamReader(FilePath);
            try
            {
                _Content = objReader.ReadToEnd();
                objReader.Close();
            }
            catch { return _Content; }
            return _Content;
        }
        public static DateTime getNigerianTime()
        {

            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //Print out the date and time
            return dateTime;

        }


        public static string GetMD5(string value)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(value);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }


        public static string Translate(string stringToTranslate,
           string fromLanguage, string toLanguage)
        {

            // Initialize the translator
            Translator t = new Translator();
            t.SourceLanguage = fromLanguage;
            t.TargetLanguage = toLanguage;
            t.SourceText = stringToTranslate;

            
            // Translate the text
            try
            {
                // Forward translation
              
                t.Translate();
                return t.Translation;
            }
            catch (Exception ex)
            {
                return "Can not translate ,please try again later.";
            }
            


        }
          
    }

}



