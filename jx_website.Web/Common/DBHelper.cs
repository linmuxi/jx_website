using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jx_website.Common
{
    public class DBHelper
    {
        private static DBHelper dbHelper = new DBHelper();

        private PetaPoco.Database db;

        private DBHelper() { 
        }

        public static DBHelper newInstance()
        {
            return dbHelper;
        }

        public void initDB(String connectionStringName)
        {
            this.db = new PetaPoco.Database(connectionStringName);
        }

        public PetaPoco.Database getDB()
        {
            if(this.db == null){
                this.initDB("MySqlConnection");
            }
            return this.db;
        }

        public Boolean insert(Object poco)
        {
            if(this.db != null){
               this.db.Insert(poco);
               return true;
            }
            return false;
        }
    }
}
