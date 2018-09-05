using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LayrCake.WCFServiceHost
{
    public class ServiceConfigurationElement : ConfigurationElement
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigurationElemet" /> class.
        /// </summary>
        public ServiceConfigurationElement()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [ConfigurationProperty("serviceType", IsRequired = true)]
        public string ServiceType
        {
            get { return (string)this["serviceType"]; }
            set { this["serviceType"] = value; }
        }

        /// <summary>
        /// Gets or sets the Assembly Path for Runtime loading
        /// </summary>
        [ConfigurationProperty("servicePath", IsRequired = false)]
        public string ServicePath
        {
            get { return (string)this["servicePath"]; }
            set { this["servicePath"] = value; }
        }

        #endregion Properties
    }
}
