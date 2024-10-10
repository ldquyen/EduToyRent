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
                "edutoyrent-3f3f53b2f1c2.json");
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
