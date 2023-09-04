using Elements;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class UpdateAnElement
    {
        private UpdateAnElement() { }

        public static Dictionary<string, string> UpdateElement(
             string projectId,
             string elementId,
             string categoryId = null,
             string name = null,
             bool? completed = null,
             bool? starred = null,
             string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/elements/{elementId}");
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            StringBuilder payload = new StringBuilder();
            payload.Append("{");

            // Only add properties to payload if they are not null
            if (!string.IsNullOrEmpty(categoryId))
            {
                payload.AppendFormat("\"category\":{{\"id\":\"{0}\"}},", categoryId);
            }
            if (name != null)
            {
                payload.AppendFormat("\"name\":\"{0}\",", name);
            }
            if (completed.HasValue)
            {
                payload.AppendFormat("\"completed\":{0},", completed.ToString().ToLower());
            }
            if (starred.HasValue)
            {
                payload.AppendFormat("\"starred\":{0},", starred.ToString().ToLower());
            }

            // Remove last comma and close payload
            if (payload.Length > 1)
            {
                payload.Remove(payload.Length - 1, 1);
            }
            payload.Append("}");

            request.AddJsonBody(payload.ToString());

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var elementResponse = JsonConvert.DeserializeObject<ElementResponse>(response.Content);
                    var element = elementResponse.Element;

                    Dictionary<string, string> updatedElement = new Dictionary<string, string>
                        {
                            { "id", element.Id },
                            { "name", element.Name }
                        };
                    return updatedElement;
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
