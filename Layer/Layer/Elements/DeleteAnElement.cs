using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class DeleteAnElement
    {
        private DeleteAnElement() { }
        public static bool DeleteElement(string projectId, string elementId, string bearerToken)
        {
            var client = new RestClient();
            var request = new RestRequest($"https://api.layer.team/projects/{projectId}/elements/{elementId}", Method.Delete);

            // Add Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    return true;
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
