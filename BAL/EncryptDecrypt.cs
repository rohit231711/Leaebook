using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;

public class Security
{
    private static string EncryptKey = Config.EncryptionKey;

    // By creating private constructor this class will not allow to create an object of this class 
    public Security()
    {
    }

    #region Simple Encrypt/Decrypt string using MD5 and TripleDES Crypto Algorithm
    /// <summary>
    /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
    /// </summary>
    /// <param name="toEncrypt">string to be encrypted</param>
    /// <returns></returns>
    //public static string Encrypt(string toEncrypt)
    //{
    //    byte[] keyArray;
    //    byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

    //    // Get the key from config file
    //    string key = EncryptKey;

    //    //if (useHashing){
    //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
    //    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
    //    hashmd5.Clear();
    //    //}
    //    //else
    //    //keyArray = UTF8Encoding.UTF8.GetBytes(key);

    //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    //    tdes.Key = keyArray;
    //    tdes.Mode = CipherMode.ECB;
    //    tdes.Padding = PaddingMode.PKCS7;

    //    ICryptoTransform cTransform = tdes.CreateEncryptor();
    //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
    //    tdes.Clear();
    //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    //}

    ///// <summary>
    ///// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
    ///// </summary>
    ///// <param name="cipherString">encrypted string</param>
    ///// <returns></returns>
    //public static string Decrypt(string cipherString)
    //{
    //    byte[] keyArray;
    //    byte[] toEncryptArray = Convert.FromBase64String(cipherString);

    //    //Get your key from config file to open the lock!
    //    string key = EncryptKey;

    //    //if (useHashing)
    //    //{
    //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
    //    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

    //    hashmd5.Clear();
    //    //}
    //    //else
    //    //    keyArray = UTF8Encoding.UTF8.GetBytes(key);

    //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    //    tdes.Key = keyArray;
    //    tdes.Mode = CipherMode.ECB;
    //    tdes.Padding = PaddingMode.PKCS7;

    //    ICryptoTransform cTransform = tdes.CreateDecryptor();
    //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

    //    tdes.Clear();
    //    return UTF8Encoding.UTF8.GetString(resultArray);
    //}
    #endregion


    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }








    #region Encrypt/Decrypt string using AES 256 bits key algorithm
    /// <summary>
    /// Encrypt original data string using AES 256 algorithm
    /// </summary>
    /// <param name="toEncrypt">String you want to encrypt</param>
    //public static string AES256Encrypt(string toEncrypt)
    //{
    //    InitializeVariables();

    //    // Get the key from config file        
    //    Key.Text = EncryptKey;
    //    Data dataEncrypt = new Data(toEncrypt);
    //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
    //    ValidateKeyAndIv(true);
    //    CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
    //    cs.Write(dataEncrypt.Bytes, 0, dataEncrypt.Bytes.Length);
    //    cs.Close();
    //    ms.Close();
    //    return Convert.ToString((new Data(ms.ToArray())).Base64);
    //}

    public static string AES256Encrypt(string toEncrypt)
    {
        InitializeVariables();

        // Get the key from config file        
        Key.Text = EncryptKey;
        Data dataEncrypt = new Data(toEncrypt);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        ValidateKeyAndIv(true);
        CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(dataEncrypt.Bytes, 0, dataEncrypt.Bytes.Length);
        cs.Close();
        ms.Close();
        return Convert.ToString((new Data(ms.ToArray())).Base64);
    }

    /// <summary>
    /// Decrypt AES 256 encrypted string
    /// </summary>
    /// <param name="CipherString">Encrypted data string</param>
    public static string AES256Decrypt(string CipherString)
    {
        InitializeVariables();

        // Get the key from config file        
        Key.Text = EncryptKey;
        Data dataDecrypt = new Data(CipherString);
        byte[] byteData = Data.FromBase64(dataDecrypt.Text.Trim().ToString());
        System.IO.MemoryStream ms = new System.IO.MemoryStream(byteData, 0, byteData.Length);
        byte[] b = new Byte[byteData.Length];

        ValidateKeyAndIv(false);
        CryptoStream cs = new CryptoStream(ms, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

        try
        {
            cs.Read(b, 0, byteData.Length);
        }
        catch (CryptographicException ex)
        {
            throw new CryptographicException("Unable to decrypt data. The provided key may be invalid.", ex);
        }
        finally { cs.Close(); }
        return Convert.ToString((new Data(b)).Text);
    }
    #endregion

    #region classes & functions used for AES 256 bits key algorithm
    //AES Encryption with 256 bits key
    private static Data _key;
    private static Data _iv;
    private static SymmetricAlgorithm _crypto;
    private const string _DefaultIntializationVector = "%1Az=-@qT";

    /// <summary>
    /// The key used to encrypt/decrypt data
    /// </summary>
    private static Data Key
    {
        get { return _key; }
        set
        {
            _key = value;
            _key.MaxBytes = _crypto.LegalKeySizes[0].MaxSize / 8;
            _key.MinBytes = _crypto.LegalKeySizes[0].MinSize / 8;
            _key.StepBytes = _crypto.LegalKeySizes[0].SkipSize / 8;
        }
    }

    /// <summary>
    /// Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
    /// the value derived from the previous block; the first data block has no previous data block
    /// to use, so it needs an InitializationVector to feed the first block
    /// </summary>
    private static Data IntializationVector
    {
        get { return _iv; }
        set
        {
            _iv = value;
            _iv.MaxBytes = _crypto.BlockSize / 8;
            _iv.MinBytes = _crypto.BlockSize / 8;
        }
    }

    private static Data RandomKey()
    {
        _crypto.GenerateKey();
        Data d = new Data(_crypto.Key);
        return d;
    }

    /// <summary>
    /// generates a random Initialization Vector, if one was not provided
    /// </summary>
    private static Data RandomInitializationVector()
    {
        _crypto.GenerateIV();
        Data d = new Data(_crypto.IV);
        return d;
    }

    private static void ValidateKeyAndIv(bool isEncrypting)
    {
        if (_key.IsEmpty)
        {
            if (isEncrypting) { _key = RandomKey(); }
            else
            {
                throw new CryptographicException("No key was provided for the decryption operation!");
            }
        }
        if (_iv.IsEmpty)
        {
            if (isEncrypting) { _iv = RandomInitializationVector(); }
            else
            {
                throw new CryptographicException("No initialization vector was provided for the decryption operation!");
            }
        }
        _crypto.Key = _key.Bytes;
        _crypto.IV = _iv.Bytes;
    }

    private static void InitializeVariables()
    {
        _crypto = new RijndaelManaged();
        ///-- make sure key and IV are always set, no matter what
        Key = RandomKey();
        ///if (useDefaultInitializationVector ){
        IntializationVector = new Data(_DefaultIntializationVector);
        ///}else{
        ///Me.IntializationVector = RandomInitializationVector();
        ///}
    }

    private class Data
    {
        private byte[] _b;
        private int _MaxBytes = 0;
        private int _MinBytes = 0;
        private int _StepBytes = 0;

        /// <summary>
        /// Determines the default text encoding for this Data instance
        /// </summary>
        private System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding("Windows-1252");

        public Data(byte[] b) { _b = b; }

        /// <summary>
        /// Creates new encryption data with the specified string; 
        /// will be converted to byte array using default encoding
        /// </summary>
        public Data(string s) { this.Text = s; }

        /// <summary>
        /// Creates new encryption data using the specified string and the 
        /// specified encoding to convert the string to a byte array.
        /// </summary>
        public Data(string s, System.Text.Encoding encoding)
        {
            this.Encoding = encoding;
            this.Text = s;
        }

        /// <summary>
        /// returns true if no data is present
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (_b == null || _b.Length == 0)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// allowed step interval, in bytes, for this data; if 0, no limit
        /// </summary>
        public int StepBytes
        {
            get { return _StepBytes; }
            set { _StepBytes = value; }
        }

        /// <summary>
        /// allowed step interval, in bits, for this data; if 0, no limit
        /// </summary>
        public int StepBits
        {
            get { return _StepBytes * 8; }
            set { _StepBytes = value / 8; }
        }

        /// <summary>
        /// minimum number of bytes allowed for this data; if 0, no limit
        /// </summary>
        public int MinBytes
        {
            get { return _MinBytes; }
            set { _MinBytes = value; }
        }

        /// <summary>
        /// maximum number of bytes allowed for this data; if 0, no limit
        /// </summary>
        public int MaxBytes
        {
            get { return _MaxBytes; }
            set { _MaxBytes = value; }
        }

        /// <summary>
        /// maximum number of bits allowed for this data; if 0, no limit
        /// </summary>
        public int MaxBits
        {
            get { return _MaxBytes * 8; }
            set { _MaxBytes = value / 8; }
        }

        /// <summary>
        /// Returns the byte representation of the data; 
        /// This will be padded to MinBytes and trimmed to MaxBytes as necessary!
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                if (_MaxBytes > 0)
                {
                    if (_b.Length > _MaxBytes)
                    {
                        byte[] b = new byte[_MaxBytes];
                        Array.Copy(_b, b, b.Length);
                        _b = b;
                    }
                }
                if (_MinBytes > 0)
                {
                    if (_b.Length < _MinBytes)
                    {
                        byte[] b = new byte[_MinBytes];
                        Array.Copy(_b, b, _b.Length);
                        _b = b;
                    }
                }
                return _b;
            }
            set { _b = value; }
        }

        /// <summary>
        /// Sets or returns text representation of bytes using the default text encoding
        /// </summary>
        public string Text
        {
            get
            {
                if (_b == null) { return string.Empty; }
                else
                {
                    /// need to handle nulls here; oddly, C# will happily convert
                    /// nulls into the string whereas VB stops converting at the
                    /// first null!
                    int i = Array.IndexOf(_b, Convert.ToByte(0));
                    if (i >= 0) { return this.Encoding.GetString(_b, 0, i); }
                    else { return this.Encoding.GetString(_b); }
                }
            }
            set { _b = this.Encoding.GetBytes(value); }
        }

        /// <summary>
        /// returns Base64 string representation of this data
        /// </summary>
        internal static string ToBase64(byte[] b)
        {
            if (b == null || b.Length == 0) { return string.Empty; }
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// converts from a string Base64 representation to an array of bytes
        /// </summary>
        internal static byte[] FromBase64(string base64Encoded)
        {
            if (base64Encoded == null || base64Encoded.Length == 0) { return null; }
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (System.FormatException ex)
            {
                throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// Sets or returns Base64 string representation of this data
        /// </summary>
        public string Base64
        {
            get { return ToBase64(_b); }
            set { _b = FromBase64(value); }
        }
    }
    #endregion

    //public static string MD5Hash(string text)
    //{
    //    MD5 md5 = new MD5CryptoServiceProvider();

    //    //compute hash from the bytes of text
    //    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

    //    //get hash result after compute it
    //    byte[] result = md5.Hash;

    //    StringBuilder strBuilder = new StringBuilder();
    //    for (int i = 0; i < result.Length; i++)
    //    {
    //        //change it into 2 hexadecimal digits
    //        //for each byte
    //        strBuilder.Append(result[i].ToString("x2"));
    //    }

    //    return strBuilder.ToString();
    //}

    public static string md5Hash(string input)
    {
        String result = "";

        using (MD5 md5 = new MD5CryptoServiceProvider())
        {
            result = BitConverter.ToString(md5.ComputeHash(ASCIIEncoding.Default.GetBytes(input)));
        }

        return result.Replace("-", String.Empty).ToLower();
    }
}