using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace InntecMobileNetMaui.Services
{
    public class AesGcm
    {
        /*
         MIT License

        Copyright (c) 2018 Luke Park

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in all
        copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        SOFTWARE.
        https://github.com/luke-park/SecureCompatibleEncryptionExamples
        https://github.com/luke-park/SecureCompatibleEncryptionExamples/blob/master/LICENSE
         */

        #region Constants and Fields
        private readonly SecureRandom _random;

        private const string AlgorithmName = "AES";
        private const int AlgorithmNonceSize = 16;
        private const int AlgorithmKeySize = 32;
        private const int Pbkdf2SaltSize = 16;
        private const int Pbkdf2Iterations = 32767;

        #endregion

        #region Public Methods and Operators
        public AesGcm()
        {
            _random = new SecureRandom();

        }

        /// <summary>
        /// Helper that generates a random new key on each call.
        /// </summary>
        /// <returns>Base 64 encoded string</returns>
        public string NewKeyAesGcm()
        {
            var key = new byte[AlgorithmKeySize * 8];
            _random.NextBytes(key);
            return Convert.ToBase64String(key);
        }

        /// <summary>
        /// Metodo para encryptar un texto plano por medio del algoritomo-modo AES-GCM
        /// </summary>
        /// <param name="plaintext">texto plano</param>
        /// <param name="password">palabla clave</param>
        /// <returns>texto plano encriptado</returns>
        public static string EncryptString(string plaintext, string password)
        {
            try
            {
                // Generate a 128-bit salt using a CSPRNG.
                var rand = new SecureRandom();
                var salt = new byte[Pbkdf2SaltSize];
                rand.NextBytes(salt);

                // Create an instance of PBKDF2 and derive a key.
                var pbkdf2 = new Pkcs5S2ParametersGenerator(new Sha256Digest());
                pbkdf2.Init(Encoding.UTF8.GetBytes(password), salt, Pbkdf2Iterations);
                var key = ((KeyParameter)pbkdf2.GenerateDerivedMacParameters(AlgorithmKeySize * 8)).GetKey();

                // Encrypt and prepend salt.
                var ciphertextAndNonce = Encrypt(Encoding.UTF8.GetBytes(plaintext), key);
                var ciphertextAndNonceAndSalt = new byte[salt.Length + ciphertextAndNonce.Length];
                Array.Copy(salt, 0, ciphertextAndNonceAndSalt, 0, salt.Length);
                Array.Copy(ciphertextAndNonce, 0, ciphertextAndNonceAndSalt, salt.Length, ciphertextAndNonce.Length);

                // Return as base64 string.
                return Convert.ToBase64String(ciphertextAndNonceAndSalt);
            }
            catch (Exception e)
            {
                throw new Exception("Error: Al intentar encriptar el texto. AesGcm -> EncryptString()", e);
            }
        }

        /// <summary>
        /// Metodo para desencriptar un texto encriptado con el algoritmo-modo AES-GCM
        /// </summary>
        /// <param name="base64CiphertextAndNonceAndSalt">texto encriptado</param>
        /// <param name="password">palabra clave</param>
        /// <returns>texto plano desencriptado</returns>
        public static string DecryptString(string base64CiphertextAndNonceAndSalt, string password)
        {
            try
            {
                // Decode the base64.
                var ciphertextAndNonceAndSalt = Convert.FromBase64String(base64CiphertextAndNonceAndSalt);

                // Retrieve the salt and ciphertextAndNonce.
                var salt = new byte[Pbkdf2SaltSize];
                var ciphertextAndNonce = new byte[ciphertextAndNonceAndSalt.Length - Pbkdf2SaltSize];
                Array.Copy(ciphertextAndNonceAndSalt, 0, salt, 0, salt.Length);
                Array.Copy(ciphertextAndNonceAndSalt, salt.Length, ciphertextAndNonce, 0, ciphertextAndNonce.Length);

                // Create an instance of PBKDF2 and derive a key.
                var pbkdf2 = new Pkcs5S2ParametersGenerator(new Sha256Digest());
                pbkdf2.Init(Encoding.UTF8.GetBytes(password), salt, Pbkdf2Iterations);
                var key = ((KeyParameter)pbkdf2.GenerateDerivedMacParameters(AlgorithmKeySize * 8)).GetKey();

                // Decrypt and return result.
                return Encoding.UTF8.GetString(Decrypt(ciphertextAndNonce, key));
            }
            catch (Exception e)
            {
                throw new Exception("Error: Debes ingresar nuevamente por contraseña", e);
            }
        }
        #endregion

        #region Private Methods

        private static byte[] Encrypt(byte[] plaintext, byte[] key)
        {
            try
            {
                // Generate a 96-bit nonce using a CSPRNG.
                var rand = new SecureRandom();
                var nonce = new byte[AlgorithmNonceSize];
                rand.NextBytes(nonce);

                // Create the cipher instance and initialize.
                var cipher = new GcmBlockCipher(new AesEngine());
                var keyParam = ParameterUtilities.CreateKeyParameter(AlgorithmName, key);
                var cipherParameters = new ParametersWithIV(keyParam, nonce);
                cipher.Init(true, cipherParameters);

                // Encrypt and prepend nonce.
                var ciphertext = new byte[cipher.GetOutputSize(plaintext.Length)];
                var length = cipher.ProcessBytes(plaintext, 0, plaintext.Length, ciphertext, 0);
                cipher.DoFinal(ciphertext, length);

                var ciphertextAndNonce = new byte[nonce.Length + ciphertext.Length];
                Array.Copy(nonce, 0, ciphertextAndNonce, 0, nonce.Length);
                Array.Copy(ciphertext, 0, ciphertextAndNonce, nonce.Length, ciphertext.Length);

                return ciphertextAndNonce;
            }
            catch (Exception e)
            {
                throw new Exception("Error al encriptar tu contraseña: los datos no se enviaran, intenta mas tarde.", e);
            }
        }

        private static byte[] Decrypt(byte[] ciphertextAndNonce, byte[] key)
        {
            try
            {
                // Retrieve the nonce and ciphertext.
                var nonce = new byte[AlgorithmNonceSize];
                var ciphertext = new byte[ciphertextAndNonce.Length - AlgorithmNonceSize];
                Array.Copy(ciphertextAndNonce, 0, nonce, 0, nonce.Length);
                Array.Copy(ciphertextAndNonce, nonce.Length, ciphertext, 0, ciphertext.Length);

                // Create the cipher instance and initialize.
                var cipher = new GcmBlockCipher(new AesEngine());
                var keyParam = ParameterUtilities.CreateKeyParameter(AlgorithmName, key);
                var cipherParameters = new ParametersWithIV(keyParam, nonce);
                cipher.Init(false, cipherParameters);

                // Decrypt and return result.
                var plaintext = new byte[cipher.GetOutputSize(ciphertext.Length)];
                var length = cipher.ProcessBytes(ciphertext, 0, ciphertext.Length, plaintext, 0);
                cipher.DoFinal(plaintext, length);

                return plaintext;
            }
            catch (Exception e)
            {
                throw new Exception("Error: Al intentar desencriptar. AesGcm -> Decrypt()", e);
            }
        }
        #endregion
    }
}
