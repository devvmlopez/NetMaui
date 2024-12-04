using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static InntecMobileNetMaui.Models.Enumeradores;

namespace InntecMobileNetMaui.Services
{
    /// <summary>
    /// Expreciones para validacion de cadenas
    /// </summary>
    public class ExpReg
    {
        /// <summary>
        /// Exprecion para password
        /// </summary>
        private const string ExpRegPassword = @"^(?=.{10,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$";
        /// <summary>
        /// Exprecion para Caracteres expeciales
        /// </summary>
        private const string ExpRegIdentificador = "^([A-Za-z\\d_\\-\\.])+$";
        /// <summary>
        /// Exprecion para correo
        /// </summary>
        private const string ExpRegEmail = @"^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)$";
        /// <summary>
        /// Exprecion para numero telefonico de 10 digitos
        /// </summary>
        private const string ExpRegPhone = @"^\d{10,15}$";
        /// <summary>
        /// Exprecion para RFC
        /// </summary>
        private const string ExpRegRFC = @"^[A-Z&Ñ]{4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";

        /// <summary>
        /// Validacion de la cadena
        /// </summary>
        /// <param name="cadena">Cadena a validar</param>
        /// <param name="validar">Tipo de cadena</param>
        /// <returns>Identificador de validacion correcta</returns>
        public bool ValidarCadena(string cadena, EnumValidar validar)
        {
            try
            {
                switch (validar)
                {
                    case EnumValidar.Correo:
                        return !string.IsNullOrEmpty(cadena) && Regex.IsMatch(cadena, ExpRegEmail);
                    case EnumValidar.Contrasenia:
                        return !string.IsNullOrEmpty(cadena) && Regex.IsMatch(cadena, ExpRegPassword);
                    case EnumValidar.CampoTexto:
                        return !string.IsNullOrEmpty(cadena) && Regex.IsMatch(cadena, ExpRegIdentificador);
                    case EnumValidar.Telefono_Celular:
                        return !string.IsNullOrEmpty(cadena) && Regex.IsMatch(cadena, ExpRegPhone);
                    case EnumValidar.RFC:
                        return !string.IsNullOrEmpty(cadena) && Regex.IsMatch(cadena, ExpRegRFC);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
