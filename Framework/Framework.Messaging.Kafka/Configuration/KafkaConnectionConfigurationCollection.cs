using System;
using System.Collections.Generic;
using System.Configuration;

namespace Framework.Messaging.Kafka.Configuration
{
    public sealed class KafkaConnectionConfigurationCollection : ConfigurationElementCollection, IEnumerable<KafkaConnectionConfigurationElement>
    {
        private const string PropertyName = "connection";

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

        protected override string ElementName => PropertyName;

        public KafkaConnectionConfigurationElement this[int idx] => (KafkaConnectionConfigurationElement)BaseGet(idx);

        public override bool IsReadOnly()
        {
            return false;
        }

        public void Add(ConfigurationElement element)
        {
            BaseAdd(element);
        }

        public new IEnumerator<KafkaConnectionConfigurationElement> GetEnumerator()
        {
            foreach (var key in BaseGetAllKeys())
            {
                yield return (KafkaConnectionConfigurationElement)BaseGet(key);
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new KafkaConnectionConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KafkaConnectionConfigurationElement)element).ConnectionName;
        }
    }
}