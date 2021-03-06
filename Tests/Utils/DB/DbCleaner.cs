using System.Linq;
using BaseFramework.DL.Module.Db;
using Dapper;

namespace Tests.Utils.DB {
    public class DbCleaner {
        public static void TruncateAll() {
            var connection = DbConnection.Connection();
            var tables = connection.Query<string>(
                "SELECT table_name FROM information_schema.tables WHERE table_schema='public'"
            ).ToList();

            var listOfTables = "";

            tables.RemoveAll(x => x == "phinxlog");

            foreach (var table in tables) {
                listOfTables += table + ",";
            }

            listOfTables = listOfTables.Substring(0, listOfTables.Length - 1);

            connection.Execute("TRUNCATE " + listOfTables + ";");
        }
    }
}
