using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOAppShared.SGV.DAL.DatabaseObjects
{
    [Table("tblCopy")]
    public class CopyItem : IDatabaseObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("copyID")]
        public int copyID { get; set; }
        [Column("copyKey")]
        public string Key { get; set; }
        [Column("copyValue")]
        public string copyValue { get; set; }
    }
}
