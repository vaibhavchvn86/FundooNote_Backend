// --------------------------------------------------------------------------------------------------------------------
// <copyright file = "FundooDatabaseSetting.cs" Company = "BridgeLabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <Creator Name = "Vaibhav Chavan"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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
