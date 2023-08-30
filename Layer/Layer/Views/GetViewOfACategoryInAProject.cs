using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Views
{
    public class GetCategoryViewDetails
    {
        // Empty constructor to avoid import in Dynamo
        private GetCategoryViewDetails() { }

        public static Dictionary<string, object> GetViewDetails(string projectId, string categoryId, string viewId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/categories/{categoryId}/views/{viewId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            // Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var viewDetailResponse = JsonConvert.DeserializeObject<ViewDetailResponse>(response.Content);

                    // Map view details
                    var result = new Dictionary<string, object>
                    {
                        { "createdAt", viewDetailResponse.view.createdAt.ToString() },
                        { "id", viewDetailResponse.view.id },
                        { "name", viewDetailResponse.view.name },
                        { "type", viewDetailResponse.view.type },
                        { "updatedAt", viewDetailResponse.view.updatedAt.ToString() }
                    };

                    // Map field display
                    var fieldDisplayList = new List<Dictionary<string, string>>();
                    foreach (var field in viewDetailResponse.view.fieldDisplay)
                    {
                        var fieldDict = new Dictionary<string, string>
                        {
                            { "id", field.id },
                            { "type", field.type }
                        };
                        fieldDisplayList.Add(fieldDict);
                    }

                    result["fieldDisplay"] = fieldDisplayList;
                    result["filterConfig"] = new Dictionary<string, object>
                    {
                        { "condition", viewDetailResponse.view.filterConfig.condition },
                        { "filters", viewDetailResponse.view.filterConfig.filters }
                    };
                    result["sort"] = viewDetailResponse.view.sort;

                    return result;
                }
                else
                {
                    // Handle non-successful response
                    throw new Exception($"API call failed with status: {response.StatusCode}, content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }

    class ViewDetailResponse
    {
        public ViewDetail view { get; set; }
    }

    class ViewDetail
    {
        public DateTime createdAt { get; set; }
        public List<FieldDisplay> fieldDisplay { get; set; }
        public FilterConfig filterConfig { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<string> sort { get; set; } // Adjust this if 'sort' field in the response has a different structure
        public DateTime updatedAt { get; set; }
    }

    class FieldDisplay
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    class FilterConfig
    {
        public string condition { get; set; }
        public List<string> filters { get; set; } // Adjust this if 'filters' field in the response has a different structure
    }
}
