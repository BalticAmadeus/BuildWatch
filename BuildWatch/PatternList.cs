using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BuildWatch
{
    public class FilterSet
    {
        public List<PatternList> Filters;
    }

    [XmlType("Filter")]
    public class PatternList
    {
        [XmlAttribute("name")]
        public string Name;
        public List<PatternTest> Tests;
    }

    [XmlType("Test")]
    public class PatternTest
    {
        [XmlAttribute("regex")]
        public string Regex;

        [XmlAttribute("action")]
        public ActionType Action;
    }

    public enum ActionType
    {
        hide,
        show,
        notify
    }

}
