using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.SQL
{
    class SQLiteDataLogin : IDataStore<LoginModel>
    {
        private readonly ISQLiteService _liteService;
        /// <summary>
        /// Inicializacion de los objetos
        /// </summary>
        /// <param name="liteService">Interface para la conexion</param>
        public SQLiteDataLogin(ISQLiteService liteService)
        {
            _liteService = liteService;
            SQLiteConnection conection = _liteService.GetConnection();
            conection.CreateTable<LoginModel>();
            conection.Close();
        }
        /// <summary>
        /// Creacion del objeto para trabajar con los datos de login
        /// </summary>
        public SQLiteDataLogin()
        {
            _liteService = DependencyService.Get<ISQLiteService>();
            SQLiteConnection conection = _liteService.GetConnection();
            conection.CreateTable<LoginModel>();
            conection.Close();

        }

        /// <summary>
        /// agregar nuevo acceso con encriptacion de password(al recordar datos en pantalla de login)
        /// </summary>
        /// <param name="loginModel">usuario que inicio sesion</param>
        /// <returns>indicador de que el proceso fue correcto</returns>
        public async Task<bool> AddItemAsync(LoginModel loginModel)
        {
            loginModel.Password = AesGcm.EncryptString(loginModel.Password, loginModel.access_token);
            return (await _liteService.GetConnectionAsync().InsertAsync(loginModel).ConfigureAwait(false)) > 0;
        }

        /// <summary>
        /// Eliminar inicio de sesion automatico
        /// </summary>
        /// <param name="id">Identificador del Login local</param>
        /// <returns>Indicador de que el proceso fue correcto</returns>
        public async Task<bool> DeleteItemAsync(string id = null)
        {
            return (await _liteService.GetConnectionAsync().DeleteAllAsync<LoginModel>().ConfigureAwait(false)) > 0;
        }

        /// <summary>
        /// Validacion de la existencia de un login
        /// </summary>
        /// <param name="id">Identificador del Login</param>
        /// <returns>Datos del usuario para iniciar sesion</returns>
        public async Task<LoginModel> GetItemAsync(string id = null)
        {
            if (id == "CheckToPass")
            {
                LoginModel Result = await _liteService.GetConnectionAsync().Table<LoginModel>().FirstOrDefaultAsync(item => item.rememberPWS).ConfigureAwait(false);
                if (Result != null)
                    Result.Password = AesGcm.DecryptString(Result.Password, Result.access_token);
                return Result != null ? Result : new LoginModel();
            }
            else
            {
                return await _liteService.GetConnectionAsync().Table<LoginModel>().FirstOrDefaultAsync().ConfigureAwait(false);
            }
        }

        public Task<IEnumerable<LoginModel>> GetItemsAsync(LoginModel loginModel = null, bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(LoginModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}
