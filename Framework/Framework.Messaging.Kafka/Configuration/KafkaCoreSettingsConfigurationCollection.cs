using System;
using System.Collections.Generic;
using System.Configuration;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaCoreSettingsConfigurationCollection : ConfigurationElementCollection,
        IEnumerable<KafkaCoreConfigurationElement>
    {
        private const string PropertyName = "config";

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => PropertyName;

        public KafkaCoreConfigurationElement this[int idx] => (KafkaCoreConfigurationElement) BaseGet(idx);

        public new IEnumerator<KafkaCoreConfigurationElement> GetEnumerator()
        {
            foreach (var key in BaseGetAllKeys()) yield return (KafkaCoreConfigurationElement) BaseGet(key);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public void Add(ConfigurationElement element)
        {
            BaseAdd(element);
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new KafkaCoreConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KafkaCoreConfigurationElement) element).PublishClientId;
        }
    }
}