using Autodesk.DesignScript.Runtime;
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
            var client = new RestClient("https://api.layer.team/projects");
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
                    var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
                    var projectsDictList = new List<Dictionary<string, string>>();

                    foreach (var project in projectResponse.projects)
                    {
                        var projectDict = new Dictionary<string, string>
                        {
                            { "createdAt", project.createdAt.ToString() },
                            { "createdBy", project.createdBy },
                            { "company", project.company },
                            { "name", project.name },
                            { "location", project.location },
                            { "id", project.id },
                            { "status", project.status },
                            { "type", project.type }
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

    class ProjectResponse
    {
        public List<Project> projects { get; set; }
    }

   class Project
    {
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public string company { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }
}
