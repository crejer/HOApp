using SQLite;
using System;
using System.IO;
using System.Linq;
using HOAppShared.SGV.DAL;

#if __IOS__
using Foundation;
#endif

namespace HOAppShared.SGV.DL
{
    public class Database
    {
        #region delegates

        #endregion

        #region variables
        SQLiteConnection database;

#if __ANDROID__
		    string originalDBLocation = "ho.sqlite";
#elif __IOS__
        string originalDBLocation = "SharedAssets/ho.sqlite";
#endif
        const int DATABASE_VERSION = 1;
        string currentDBName = "ho" + DATABASE_VERSION + ".sqlite";
        string oldDBName = "ho" + (DATABASE_VERSION - 1) + ".sqlite";
        #endregion

        #region constructor
        //initializes a new instance of the database
        //if the database doesn't exist, it will create the database
        public Database()
        {
            var dbPath = DatabasePath;

            if (!File.Exists(dbPath))
                CreateDatabase(dbPath);

            database = new SQLiteConnection(dbPath);
        }
        #endregion

        #region properties
        string DatabasePath
        {
            get
            {
                var sqliteFilename = currentDBName;

#if __IOS__
                int SystemVersion = Convert.ToInt16(UIKit.UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
                string documentsPath = SystemVersion >= 8 ? NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path : Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);

#elif __ANDROID__
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var path = Path.Combine(documentsPath, sqliteFilename);
#endif

                return path;
            }
        }

        //path of previous version of database
        string OldDatabasePath
        {
            get
            {
                var sqliteFilename = oldDBName;

#if __IOS__
                int SystemVersion = Convert.ToInt16(UIKit.UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
                string documentsPath = SystemVersion >= 8 ? NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path : Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);

#elif __ANDROID__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);
#endif

                return path;
            }
        }
        #endregion

        #region public methods

        #region overided methods

        #region viewlifecycle

        #endregion

        #endregion

        #endregion

        #region private methods
        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            var buffer = new byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }

        

        void CreateDatabase(string dbPath)
        {
#if __ANDROID__
            var s = Application.Context.Assets.Open(originalDBLocation);
            var writeStream = new FileStream(dbPath, FileMode.OpenOrCreate, FileAccess.Write);
            ReadWriteStream(s, writeStream);
            writeStream.Close();

#elif __IOS__
            var appDir = NSBundle.MainBundle.ResourcePath;
            var originalLocation = Path.Combine(appDir, originalDBLocation);
            File.Copy(originalLocation, dbPath);
#endif

            //copies profiles and selected eigenschappen from the old database to the new one
            if (File.Exists(OldDatabasePath))
            {
                SQLiteConnection oldDb = new SQLiteConnection(OldDatabasePath);
                SQLiteConnection newDB = new SQLiteConnection(dbPath);
                oldDb.Dispose();
                newDB.Dispose();
                File.Delete(OldDatabasePath);
            }
        }


        public T GetItem<T>(string key) where T:IDatabaseObject, new()
        {
            lock (database)
            {
                return database.Table<T>().FirstOrDefault(x => x.Key == key);
            }
        }
        #endregion




    }
}
