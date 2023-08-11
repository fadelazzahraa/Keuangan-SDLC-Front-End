﻿using Newtonsoft.Json.Linq;
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
        static private string hostURL = "http://127.0.0.1:8080/";
        static public string signinURL = hostURL + "auth/signin";
        static public string getRecordsURL = hostURL + "records?sourceRecordId=2";
        static public string getRecordByUserURL(string username)
        {
            return $"{hostURL}records?sourceRecordId=2&tag={username}";
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
        static public string uploadPhotoURL = hostURL + "photos/upload";
        static public string postPhotoURL = hostURL + "photos";
        static public string setPhotoURL(int index)
        {
            return $"{hostURL}photos/{index}/image";
        }
        static public string getPhotoURL = hostURL + "photos?tag=Bank Secure SDLC Photo";
        static public string getPhotoImageWithIndexURL(int index)
        {
            return $"{hostURL}photos/{index}/image";
        }
        static public string getUsersURL = hostURL + "user";
        static public string postUserProfileURL = hostURL + "user/profile";
        static public string changePasswordURL = hostURL + "user/password";

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
    }
}
