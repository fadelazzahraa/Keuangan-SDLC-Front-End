using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keuangan
{
    public class Connection
    {
        /* HOST URL */
        static private string hostURL = "http://127.0.0.1:8080/";
        
        /* USER MANAGEMENT */
        static public string signinURL = hostURL + "auth/signin";
        static public string signupURL = hostURL + "auth/signup";
        static public string getUsersURL = hostURL + "user";
        static public string getUserProfileURL = hostURL + "user/profile";
        static public string postUserProfileURL = hostURL + "user/profile";
        static public string changePasswordURL = hostURL + "user/password";
        static public string changeRoleURL = hostURL + "user/role";

        /* RECORD */
        static public string getRecordsURL = hostURL + "records";
        static public string getRecordByUserURL(int userId)
        {
            return $"{hostURL}records?actorId={userId}";
        }
        static public string addRecordURL = hostURL + "records";
        static public string editRecordURL(int index)
        {
            return $"{hostURL}records/{index}";
        }
        static public string deleteRecordURL(int index)
        {
            return $"{hostURL}records/{index}";
        }

        /* PHOTO */
        static public string uploadPhotoURL = hostURL + "photos/upload";
        static public string postPhotoURL = hostURL + "photos";
        static public string setPhotoURL(int index)
        {
            return $"{hostURL}photos/{index}/image";
        }
        static public string getPhotoURL = hostURL + "photos";
        static public string getPhotoImageWithIndexURL(int index)
        {
            return $"{hostURL}photos/{index}/image";
        }

        /* CATEGORY */
        static public string getCategoriesURL = hostURL + "category";
        static public string addCategoryURL = hostURL + "category";
        static public string editCategoryURL(int index)
        {
            return $"{hostURL}category/{index}";
        }
        static public string deleteCategoryURL(int index)
        {
            return $"{hostURL}category/{index}";
        }

        /* REQUEST CONTROLLER */
        static public async Task<string> PostDataAsync(string url, string requestBody)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }

        static public async Task<string> GetAuthorizedDataAsync(string url, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                HttpResponseMessage response = await httpClient.GetAsync(url);

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }

        static public async Task<string> PostAuthorizedDataAsync(string url, string requestBody, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }

        static public async Task<string> PostFormDataAuthorizedDataAsync(string url, MultipartFormDataContent formData, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                HttpResponseMessage response = await httpClient.PostAsync(url, formData);

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }

        static public async Task<string> DeleteAuthorizedDataAsync(string url, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                HttpResponseMessage response = await httpClient.DeleteAsync(url);

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }

        
    }
}
