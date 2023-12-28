using System;

namespace Testing.Console.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new Person
            {
                Name = "Jefff",
                Age = 56
            };

            var toJson = Newtonsoft.Json.JsonConvert.SerializeObject(person);

            var convertToXml = new Json.Converter.To.Xml.Services.ConvertJsonToXML();
            var output = convertToXml.ConvertToXml(toJson);

            System.Console.WriteLine(output);
        }

        public class Person {
            public int Age { get; set; }
            public string? Name { get; set; }
        }
    }
}