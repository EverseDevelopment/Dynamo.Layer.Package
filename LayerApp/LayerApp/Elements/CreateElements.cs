using Elements;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public static class CreateElements
    {
        public static Dictionary<string, string> CreateElement(
             string projectId,
             string categoryId,
             //Dictionary<string, string>? fields, - Not implemented for now
             string name,
             bool? completed,
             bool? starred,
             string bearerToken
             )
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/elements");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            StringBuilder payload = new StringBuilder();
            payload.Append("{");
            payload.AppendFormat("\"category\":{{\"id\":\"{0}\"}},", categoryId);
            //payload.Append("\"fields\":{"); (Fields not implemented on Element creation for now)
            //foreach (var field in fields)
            //{
            //    payload.AppendFormat("\"{0}\":{{\"value\":\"{1}\"}},", field.Key, field.Value);
            //}
            //if (fields.Count > 0) payload.Length--;  // Remove last comma
            //payload.Append("},");
            payload.AppendFormat("\"name\":\"{0}\",", name);
            payload.AppendFormat("\"completed\":{0},", completed.ToString().ToLower());
            payload.AppendFormat("\"starred\":{0}", starred.ToString().ToLower());
            payload.Append("}");

            request.AddJsonBody(payload.ToString());

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var elementResponse = JsonConvert.DeserializeObject<ElementResponse>(response.Content);
                    var element = elementResponse.Element;

                    Dictionary<string, string> createdElement = new Dictionary<string, string>
                    {
                        { "id", element.Id },
                        { "name", element.Name }
                    };
                    return createdElement;
                }
                else
                {
                    throw new Exception($"API call failed with status: {response.StatusCode}, content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw ex;
            }
        } 
    } 
}
