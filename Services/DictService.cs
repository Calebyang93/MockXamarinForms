using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XFApp1.Models;

namespace XFApp1.Services
{
    // IDictionaryService d = new IDictionaryService();   <<< WRONG
    // IDictionaryService d = new DictionaryService()     <<< OK
    public class DictionaryService : IDictionaryService
    {
        public object GetKey(object value)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetMeaningAsync(string countryCode, string w)
        {
            string jsonText = "unset";
            try
            {
                string baseUrl = $@"https://api.dictionaryapi.dev/api/v2/entries/{countryCode}/";
                HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                var response = await client.GetAsync(w);
                var stream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(stream);
                jsonText = reader.ReadToEnd();
                // deserializing json to c# logic objects 
                DictionaryAPIResponse[] x = JsonSerializer.Deserialize<DictionaryAPIResponse[]>(jsonText);
                var result = x[0].meanings[0].definitions[0].definition; // take first item in each array/list         }
                return result;
            }
            catch (HttpRequestException ex)
            {
                return ex.Message;
            }
            catch (UnsupportedMediaTypeException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return jsonText;
            }
        }

        public object GetValue(object key)
        {
            throw new NotImplementedException();
        }

        public void SetValue(object key, object value)
        {
            throw new NotImplementedException();
        }
    }
}
