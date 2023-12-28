using Json.Converter.To.Xml.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class JsonConvertorController : ControllerBase
    {
        private readonly IConvertJsonToXML _convertJsonToXML;

        public JsonConvertorController(IConvertJsonToXML convertJsonToXML)
        {
            _convertJsonToXML = convertJsonToXML;
        }

        // GET: api/<JsonConvertor>
        [HttpGet("{message}")]
        public IActionResult GetXML(string message)
        {
            var convertToXML = _convertJsonToXML.ConvertToXml(message);

            return new ContentResult
            {
                Content = convertToXML,
                ContentType = "text/xml",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
