using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LayrCake.WCFServiceHost
{
    public class DebugConfigurationElement : ConfigurationElement
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigurationElemet" /> class.
        /// </summary>
        public DebugConfigurationElement()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the debug value.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("debug", IsRequired = true, IsKey = true)]
        public string Debug
        {
            get { return (string)this["debug"]; }
            set { this["debug"] = value; }
        }

        #endregion Properties
    }
}
