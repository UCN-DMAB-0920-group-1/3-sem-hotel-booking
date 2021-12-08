using API;
using Enviroment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = Database.Version;

namespace Tests.Integration.Common
{
    public class BaseDbSetup
    {
        #region setup
        internal string connectionString = new ENV().ConnectionStringTest;
        internal static SqlDataContext _dataContext;
        internal static List<Action> _dropDatabaseActions = new();

        [TestInitialize]
        public void SetUp()
        {
            _dataContext = new SqlDataContext(connectionString);
            Version.Upgrade(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Parallel.Invoke(_dropDatabaseActions.ToArray());
        }

        [TestCleanup]
        public void CleanUp()
        {
            _dropDatabaseActions.Add(() => Version.Drop(connectionString));
        }
        #endregion
    }
}
