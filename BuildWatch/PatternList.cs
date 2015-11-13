using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BuildWatch
{
    public class FilterSet
    {
        public List<PatternList> Filters;

        public FilterSet()
        {
            Filters = new List<PatternList>();
        }
    }

    [XmlType("Filter")]
    public class PatternList
    {
        [XmlAttribute("name")]
        public string Name;
        public List<PatternTest> Tests;

        public PatternList()
        {
            Tests = new List<PatternTest>();
        }

        public override string ToString()
        {
            return Name;
        }

        public FilterAction Match(string s)
        {
            foreach (PatternTest t in Tests)
            {
                if (Regex.IsMatch(s, t.Regex))
                    return t.Action;
            }
            return FilterAction.show;
        }
    }

    [XmlType("Test")]
    public class PatternTest
    {
        [XmlAttribute("regex")]
        public string Regex;

        [XmlAttribute("action")]
        public FilterAction Action;
    }

    public enum FilterAction
    {
        hide,
        show,
        notify
    }

}
