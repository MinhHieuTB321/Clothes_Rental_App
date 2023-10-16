using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopService.Application.ViewModels.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Commons
{
    public static class FirebaseUtility
    {
        //private static IConfiguration _configuration= new Configuration;
        // Vulnurable Data
        private static string API_KEY = "AIzaSyD8jIG3rxybg8_SsskVhiHV8K6dBOCgNvk";
        private static string Bucket = "clothes-rental-app.appspot.com";
        private static string AuthEmail = "admin1@gmail.com";
        private static string AuthPassword = "Admin@@";
        public static async Task<FileUploadModel> UploadFileAsync(this IFormFile fileUpload,string folder)
        {
            if (fileUpload.Length > 0)
            {
                var fs = fileUpload.OpenReadStream();
                var auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                var cancellation = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true

                    }
                    ).Child("assets/"+folder).Child(fileUpload.FileName)
                    .PutAsync(fs, CancellationToken.None);
                try
                {
                    var result = await cancellation;

                    return new FileUploadModel
                    {
                        FileName = fileUpload.FileName,
                        URL = result
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }
            else throw new Exception("File is not existed!");
        }

        public static async Task<bool> RemoveFileAsync(this string fileName,string folder)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
            var loginInfo = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var storage = new FirebaseStorage(Bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(loginInfo.FirebaseToken),
                ThrowOnCancel = true
            });
            await storage.Child("assets/"+folder).Child(fileName).DeleteAsync();
            return true;

        }
    }
}
