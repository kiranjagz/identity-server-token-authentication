using Json.Converter.To.Xml.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Net;
using System.Text;
using Test.Web.Api.Extensions;
using Test.Web.Api.Models.JsonConvertor;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class JsonConvertorController : ControllerBase
    {
        private readonly ILogger<JsonConvertorController> _logger;
        private readonly IConvertJsonToXML _convertJsonToXML;

        public JsonConvertorController(IConvertJsonToXML convertJsonToXML,
            ILogger<JsonConvertorController> logger)
        {
            _convertJsonToXML = convertJsonToXML;
            _logger = logger;
        }

        // POST: api/<JsonConvertor>
        [HttpPost]
        public IActionResult GetXML([FromBody] JsonConvertor message)
        {
            try
            {
                _logger.LogTrace($"Note: Trace logs for converting the message to XML");

                var IsValidJson = message.Message.IsValidJson();

                if (!IsValidJson)
                {
                    _logger.LogWarning($"The message is not in correct json format. Payload: {message.Message}");

                    return BadRequest(new { error = "The message is not in correct json format" });
                }

                var convertToXML = _convertJsonToXML.ConvertToXml(message.Message);

                return new ContentResult
                {
                    Content = convertToXML,
                    ContentType = "text/xml",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {

                return BadRequest(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
}
