using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowclone.Data
{
    public interface IDataProvider
    {
        bool SupportsStoredProcedures { get; } // { get; } is same as { get; private set; }

        DbParameter GetParameter();
    }
}
