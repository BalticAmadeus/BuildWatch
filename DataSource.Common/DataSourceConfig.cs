using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildWatch.DataSource.Common
{
    public class DataSourceConfig : IEnumerable<string>
    {
        private Dictionary<string, string> _values;

        public string this[string key]
        {
            get
            {
                return Get(key);
            }

            set
            {
                _values[key] = value;
            }
        }

        public DataSourceConfig()
        {
            _values = new Dictionary<string, string>();
        }


        public IEnumerator<string> GetEnumerator()
        {
            return _values.Keys.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.Keys.GetEnumerator();
        }

        public string Get(string key)
        {
            string val;
            if (_values.TryGetValue(key, out val))
                return val;
            else
                throw new ConfigurationKeyNotFoundException(key);
        }

        public string Get(string key, string defaultValue)
        {
            string val;
            if (_values.TryGetValue(key, out val))
                return val;
            else
                return defaultValue;
        }
    }

    public class ConfigurationKeyNotFoundException : Exception
    {
        public ConfigurationKeyNotFoundException(string key)
            : base(string.Format("Required key '{0}' not found in data source configuration", key))
        {
            // done
        }
    }

}
