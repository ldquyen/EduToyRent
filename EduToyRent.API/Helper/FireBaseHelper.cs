using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

using EduToyRent.Service.Services;
using EduToyRent.Service.Interfaces;
using Google.Cloud.Storage.V1;

namespace EduToyRent.API.Helper
{
    public static class FireBaseHelper
    {
        public static IServiceCollection AddFirebaseServices(this IServiceCollection services)
        {
            var credentialPath = Path.Combine(Directory.GetCurrentDirectory(),
                "edutoyrent-firebase-adminsdk-3e8dj-7ca1c273d0.json");
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(credentialPath)
                });
            }

            services.AddSingleton(FirebaseApp.DefaultInstance);
            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile(credentialPath)
            //});
            services.AddSingleton(StorageClient.Create(GoogleCredential.FromFile(credentialPath)));
            services.AddScoped<IFirebaseService, FirebaseService>();
            return services;
        }
    }
}
