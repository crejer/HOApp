using System;
using System.Collections.Generic;
using System.Text;

namespace HOAppShared.SGV.DL
{
    //Database singleton
    public static class DatabaseHelper
    {

        static Database dbInstance;

        public static Database GetInstance()
        {
            if (dbInstance == null)
                dbInstance = new Database();

            return dbInstance;
        }
    }
}
