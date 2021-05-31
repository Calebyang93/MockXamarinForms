using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XFApp1.Services
{
    public interface IDictionaryService
    {
        Task<string> GetMeaningAsync(string countryCode, string s);
    }
}
