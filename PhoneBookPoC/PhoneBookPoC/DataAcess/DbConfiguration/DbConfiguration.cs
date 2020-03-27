using System;
using System.IO;

namespace PhoneBookPoC.DataAcess
{
    public class DbConfiguration : IDbConfiguration
    {
        private const string _databaseFileName = "PhoneBook.db3";

        public string GetPath()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            return Path.Combine(basePath, _databaseFileName);
        }
    }
}
