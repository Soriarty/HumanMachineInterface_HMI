﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HM_Interface_Visu.Classes
{
    public class SecurityCryption
    {
        #region Constructor
        public SecurityCryption(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltValueBytes = Encoding.UTF8.GetBytes(SaltValue);

            _DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, saltValueBytes, PasswordIterations);
            _InitVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            _KeyBytes = _DeriveBytes.GetBytes(32);
        }
        #endregion

        #region Private Fields
        private readonly Rfc2898DeriveBytes _DeriveBytes;
        private readonly byte[] _InitVectorBytes;
        private readonly byte[] _KeyBytes;
        #endregion

        private const string InitVector = "T=A4rAzu94ez-dra";
        private const int PasswordIterations = 1000; //2;
        private const string SaltValue = "d=?ustAF=UstenAr3B@pRu8=ner5sW&h59_Xe9P2za-eFr2fa&ePHE@ras!a+uc@";

        public string Decrypt(string encryptedText)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);
            string plainText;

            RijndaelManaged rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };

            try
            {
                using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(_KeyBytes, _InitVectorBytes))
                {
                    using (MemoryStream memoryStream = new MemoryStream(encryptedTextBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {                            
                            byte[] plainTextBytes = new byte[encryptedTextBytes.Length];

                            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                        }
                    }
                }
            }
            catch (CryptographicException exception)
            {
                plainText = string.Empty;
            }

            return plainText;
        }

        public string Encrypt(string plainText)
        {
            string encryptedText;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            RijndaelManaged rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };

            using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(_KeyBytes, _InitVectorBytes))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        byte[] cipherTextBytes = memoryStream.ToArray();
                        encryptedText = Convert.ToBase64String(cipherTextBytes);
                    }
                }
            }

            return encryptedText;
        }
    }    
}
