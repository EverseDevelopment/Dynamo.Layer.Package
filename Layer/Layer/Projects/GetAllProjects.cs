using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Projects
{
    public class GetAllProjects
    {
        // Empty constructor to avoid import in Dynamo
        private GetAllProjects() { }

        public static List<Dictionary<string, string>> GetProjects(string bearerToken)
        {

            RestClientOptions options = new RestClientOptions();
            options.MaxTimeout = -1;
            var client = new RestClient(options);
            var request = new RestRequest("https://api.layer.team/projects",Method.Get);

            // Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
                    var projectsDictList = new List<Dictionary<string, string>>();

                    foreach (var project in projectResponse.Projects)
                    {
                        var projectDict = new Dictionary<string, string>
                        {
                            { "createdAt", project.CreatedAt.ToString() },
                            { "createdBy", project.CreatedBy },
                            { "company", project.Company },
                            { "name", project.Name },
                            { "location", project.Location },
                            { "id", project.Id },
                            { "status", project.Status },
                            { "type", project.Type }
                        };
                        projectsDictList.Add(projectDict);
                    }

                    return projectsDictList;
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
}
