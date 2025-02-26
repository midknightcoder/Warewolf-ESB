using System;
using System.IO;
using System.Text;
using Dev2.Runtime.Hosting;
using Dev2.Runtime.Security;
using Dev2.Runtime.WebServer.Responses;
using Newtonsoft.Json;

namespace Dev2.Runtime.WebServer.Handlers
{
    public class GetApisJsonServiceHandler : AbstractWebRequestHandler
    {

        public override void ProcessRequest(ICommunicationContext ctx)
        {
            if(ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }
            var basePath = ctx.Request.BoundVariables["path"];
            var result = GetApisJson(basePath);
            
            ctx.Send(result);
        }

        static IResponseWriter GetApisJson(string basePath)
        {
            var apiBuilder = new ApisJsonBuilder(ServerAuthorizationService.Instance, ResourceCatalog.Instance);
            var apis = apiBuilder.BuildForPath(basePath);
            var converter = new JsonSerializer();
            StringBuilder result = new StringBuilder();
            var jsonTextWriter = new JsonTextWriter(new StringWriter(result)) { Formatting = Formatting.Indented };
            converter.Serialize(jsonTextWriter, apis);
            jsonTextWriter.Flush();
            var apisJson = result.ToString();
            var stringResponseWriter = new StringResponseWriter(apisJson, ContentTypes.Json,false);
            return stringResponseWriter;
        }
    }
}