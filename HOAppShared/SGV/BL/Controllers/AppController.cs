using HOAppShared.SGV.DAL;
using HOAppShared.SGV.DAL.DatabaseObjects;
using HOAppShared.SGV.DL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOAppShared.SGV.BL.Controllers
{
    public class AppController
    {

        #region delegates

        #endregion

        #region variables
        static readonly AppController _instance = new AppController();
        readonly Database database = DatabaseHelper.GetInstance();
        Dictionary<string, string> _copyDictionary;
        #endregion

        #region constructor
        public AppController()
        {
            _copyDictionary = new Dictionary<string, string>();
        }
        #endregion

        #region properties
        public static AppController Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region public methods

        #region overided methods

        #region viewlifecycle

        #endregion

        #endregion

        /// <summary>
		/// Gets the copy.
		/// </summary>
		/// <returns>The copy.</returns>
		/// <param name="key">Key.</param>
		public string GetCopy(string key)
        {
            string returnVal = "";
            if (_copyDictionary.ContainsKey(key))
            {
                _copyDictionary.TryGetValue(key, out returnVal);
            }
            else
            {
                returnVal = database.GetItem<CopyItem>(key).copyValue;
                if (!string.IsNullOrEmpty(returnVal))
                    _copyDictionary.Add(key, returnVal);
            }
            return returnVal;
        }

        #endregion

        #region private methods

        #endregion
    }
}
