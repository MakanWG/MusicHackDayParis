using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsaaraKemith.Business.Contracts
{
    public interface ISettingsService
    {
        void SaveSetting(string key, object value);
        object GetSetting(string key);
        bool IsKeyStored(string key);
        void ClearSettings();
        void RemoveSetting(string key);
    }
}
