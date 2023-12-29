using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using Test.Web.Api.Controllers;

namespace Test.Web.Api.Extensions
{
    public static class JsonService
    {
        public static bool IsValidJson(this string json)
        {
            try
            {
                var result = JToken.Parse(json);
                return true;
            }
            catch (JsonReaderException e)
            {               
                return false;
            }
        }
    }
}
