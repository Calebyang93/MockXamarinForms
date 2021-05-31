using System;
using System.Collections.Generic;
using System.Text;

namespace XFApp1.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // 4 methods that were converted from Json to C Sharp.
    public class Phonetic
    {
        public string text { get; set; }
    }

    public class Definition
    {
        public List<object> antonyms { get; set; }
        public string definition { get; set; }
        public List<object> synonyms { get; set; }
        public string example { get; set; }
    }

    public class Meaning
    {
        public string partOfSpeech { get; set; }
        public List<Definition> definitions { get; set; }
    }

    public class DictionaryAPIResponse
    {
        public List<Phonetic> phonetics { get; set; }
        public string phonetic { get; set; }
        public string word { get; set; }
        public List<Meaning> meanings { get; set; }
    }

    class DictAPIresponse
    {
    }
}
