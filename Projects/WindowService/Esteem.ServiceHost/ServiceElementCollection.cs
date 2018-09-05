using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LayrCake.WCFServiceHost
{
    /// <summary>
    /// Represents a list of service elements in a configuration file.
    /// </summary>
    public class ServiceElementCollection : ConfigurationElementCollection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceElementCollection"/> class.
        /// </summary>
        public ServiceElementCollection()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ServiceConfigurationElement"/> at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ServiceConfigurationElement this[int index]
        {
            get
            {
                return (ServiceConfigurationElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="ServiceConfigurationElement"/> with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ServiceConfigurationElement this[string name]
        {
            get
            {
                return (ServiceConfigurationElement)BaseGet(name);
            }
        }

        /// <summary>
        /// Gets the type of this collection.
        /// </summary>
        /// <returns>The type of this collection.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new <see cref="ServiceConfigurationElement"/>
        /// </summary>
        /// <returns>A new <see cref="ServiceConfigurationElement"/></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceConfigurationElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class
        /// </summary>
        /// <param name="element">The <see cref="ServiceConfigurationElement"/> to return the key for.</param>
        /// <returns>An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="ServiceConfigurationElement"/></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceConfigurationElement)element).Name;
        }

        /// <summary>
        /// Returns the index of the specified <see cref="ServiceConfigurationElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="ServiceConfigurationElement"/>.</param>
        /// <returns>The index of the specified <see cref="ServiceConfigurationElement"/>.</returns>
        public int IndexOf(ServiceConfigurationElement element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The <see cref="ServiceConfigurationElement"/>.</param>
        public void Add(ServiceConfigurationElement element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The <see cref="ServiceConfigurationElement"/>.</param>
        public void Remove(ServiceConfigurationElement element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Name);
        }

        /// <summary>
        /// Removes the <see cref="ServiceConfigurationElement"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Removes the <see cref="ServiceConfigurationElement"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Clears this collection.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        #endregion Methods
    }
}
