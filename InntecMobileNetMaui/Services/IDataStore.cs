﻿using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id = null);
        Task<T> GetItemAsync(string id = null);
        Task<IEnumerable<T>> GetItemsAsync(LoginModel loginModel = null, bool forceRefresh = false);
    }
}
