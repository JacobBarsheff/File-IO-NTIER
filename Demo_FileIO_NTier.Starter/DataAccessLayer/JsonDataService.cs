using Demo_FileIO_NTier.Models;
using Demo_FileIO_NTier.Starter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo_FileIO_NTier.Starter.DataAccessLayer
{
    public class JsonDataService : IDataService
    {
        private string _dataFilePath;


        IEnumerable<Character> IDataService.ReadAll()
        {
            List<Character> characters = new List<Character>();

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    Characters characterList = JsonConvert.DeserializeObject<RootObject>(jsonString).Characters;

                    characters = characterList.Character;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return characters;
        }

        void IDataService.WriteAll(IEnumerable<Character> characters)
        {
            throw new NotImplementedException();
        }
        public JsonDataService(string datafile)
        {
            _dataFilePath = datafile;
        }
    }
}
