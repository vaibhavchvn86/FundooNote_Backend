using System;
using System.Collections.Generic;
using System.Text;

namespace FundooModels
{
    public class FundooDatabaseSetting : IFundooDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IFundooDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }
}
