using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Projects
{
    public class GetASpecificProject
    {
        // Empty constructor to avoid import in Dynamo
        private GetASpecificProject() { }

        public static Dictionary<string, string> GetProjectById(string projectId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}");
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
                    var projectResponse = JsonConvert.DeserializeObject<SpecificProjectResponse>(response.Content);
                    var project = projectResponse.project;

                    var projectDict = new Dictionary<string, string>
                    {
                        { "createdAt", project.createdAt.ToString() },
                        { "createdBy", project.createdBy },
                        { "company", project.company },
                        { "name", project.name },
                        { "location", project.location },
                        { "id", project.id },
                        { "status", project.status },
                        { "type", project.type },
                        { "role", projectResponse.role } // role is outside the project object
                    };

                    return projectDict;
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

    class SpecificProjectResponse
    {
        public Project project { get; set; }
        public string role { get; set; }
    }
}