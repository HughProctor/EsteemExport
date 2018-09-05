using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LayrCake.WCFServiceHost
{
    /// <summary>
    /// Represents a service configuration section in a configuration file.
    /// </summary>
    public class ServiceConfigurationSection : ConfigurationSection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigurationSection"/> class.
        /// </summary>
        public ServiceConfigurationSection()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>The services.</value>
        [ConfigurationProperty("services", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServiceElementCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ServiceElementCollection Services
        {
            get
            {
                var servicesCollection = (ServiceElementCollection)base["services"];
                return servicesCollection;
            }
        }

        #endregion Properties
    }
}
