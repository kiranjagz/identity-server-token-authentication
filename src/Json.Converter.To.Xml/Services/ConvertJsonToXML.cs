using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Json.Converter.To.Xml.Services
{
    public interface IConvertJsonToXML
    {
        string ConvertToXml(string jsonData);
    }

    public class ConvertJsonToXML : IConvertJsonToXML
    {
        public string ConvertToXml(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                return string.Empty;
            }

            // Using Newtonsoft.Json to deserialize JSON
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

            // Creating an XmlDocument and adding root element
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            // Adding elements and attributes from JSON data
            foreach (var property in jsonObject)
            {
                XmlNode node = xmlDocument.CreateElement(property.Name);
                node.InnerText = property.Value.ToString();
                rootNode.AppendChild(node);
            }

            string xmlOutput = xmlDocument.OuterXml;
           
            return xmlOutput;
        }
    }
}
