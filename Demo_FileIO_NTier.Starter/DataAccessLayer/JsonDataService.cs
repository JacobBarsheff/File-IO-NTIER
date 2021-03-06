﻿using Demo_FileIO_NTier.Models;
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
            RootObject rootObject = new RootObject();
            rootObject.Characters = new Characters();
            rootObject.Characters.Character = characters as List<Character>;

            string jsonString = JsonConvert.SerializeObject(rootObject);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonDataService(string datafile)
        {
            _dataFilePath = datafile;
        }
    }
}
