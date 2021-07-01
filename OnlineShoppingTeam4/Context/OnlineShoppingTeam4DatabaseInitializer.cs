using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using OnlineShoppingTeam4.Models;
using System.Text;
using System.IO;

namespace OnlineShoppingTeam4.Context
{

    /// <summary>
    /// Northwind Database Tables  Initializer.
    /// </summary>
    public class OnlineShoppingTeam4DatabaseInitializer : CreateDatabaseIfNotExists<OnlineShoppingTeam4Database>
    {
        /// <summary>
        /// Seed database ; fill tables.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(OnlineShoppingTeam4Database context)
        {
            InsertInDatabase(context);

            // add indexes
            context.Database.ExecuteSqlCommand("CREATE INDEX IX_Person_Name ON Persons (FirstName)");

            //add views
            var sqlFiles = Directory.GetFiles(AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\SQL", "*.sql").OrderBy(x => x);

            foreach (string file in sqlFiles)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            }

            //seed
            base.Seed(context);
        }

        private void InsertInDatabase(OnlineShoppingTeam4Database context)
        {
          
        }

        /// <summary>
        /// In case there are all Views in one sql file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <remarks>
            ///var sqlCommands = Directory
            ///                    .EnumerateFiles(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "*.sql")
            ///                    .OrderBy(x => x)
            ///                    .SelectMany(ReadAllCommands);
            ///
            ///foreach (string command in sqlCommands)
            ///{
            ///    context.Database.ExecuteSqlCommand(command);
            ///}
    /// </remarks>
    static IEnumerable<string> ReadAllCommands(string path)
        {
            StringBuilder sb = null;
            foreach (string line in File.ReadLines(path))
            {
                if (string.Equals(line, "GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (null != sb && 0 != sb.Length)
                    {
                        string item = sb.ToString();
                        if (!string.IsNullOrWhiteSpace(item)) yield return item;
                        sb = null;
                    }
                }
                else
                {
                    if (null == sb) sb = new StringBuilder();
                    sb.AppendLine(line);
                }
            }
            if (null != sb && 0 != sb.Length)
            {
                string item = sb.ToString();
                if (!string.IsNullOrWhiteSpace(item)) yield return item;
            }
        }
    }
}