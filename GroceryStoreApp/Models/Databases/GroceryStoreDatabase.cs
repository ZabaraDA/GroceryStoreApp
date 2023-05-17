
using GroceryStoreApp.Models.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApp.Databases
{
    public class GroceryStoreDatabase
    {
        private static GroceryStoreDatabasesEntities _databasesEntities;

        public static GroceryStoreDatabasesEntities GetContext()
        {
            if (_databasesEntities == null)
                _databasesEntities = new GroceryStoreDatabasesEntities();
            return _databasesEntities;
        }
    }
}
