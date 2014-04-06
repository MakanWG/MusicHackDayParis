using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Business.Contracts;

namespace UsaaraKemith.Business.Services
{
    public class SettingsService : ISettingsService
    {
        public void SaveSetting(string key, object value)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                IsolatedStorageSettings
                    .ApplicationSettings
                    .Remove(key);
                IsolatedStorageSettings
                    .ApplicationSettings
                    .Save();
            }

            IsolatedStorageSettings
                .ApplicationSettings
                .Add(key, value);
            IsolatedStorageSettings
                .ApplicationSettings
                .Save();
        }

        public object GetSetting(string key)
        {
            return IsolatedStorageSettings
                .ApplicationSettings
                .SingleOrDefault(element => element.Key == key)
                .Value;
        }

        public bool IsKeyStored(string key)
        {
            return IsolatedStorageSettings
                .ApplicationSettings
                .Any(element => element.Key == key);
        }

        public void ClearSettings()
        {
            IsolatedStorageSettings
                .ApplicationSettings
                .Clear();
        }

        public void RemoveSetting(string key)
        {
            IsolatedStorageSettings
                .ApplicationSettings
                .Remove(key);
        }
    }
}
