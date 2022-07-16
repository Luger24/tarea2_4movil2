using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xam.Forms.VideoPlayer;
using Xamarin.Essentials;
using System.IO;
using tarea2_4movil2.Views;

namespace tarea2_4movil2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public string PhotoPath;

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {

            var status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                return; // si no tiene los permisos no avanza
            }

            GrabarVideo();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdescripcion.Text))
            {
                await DisplayAlert("Aviso", "Debe de ingresar una descripcion del video antes de guardar", "OK");
                txtdescripcion.Focus();
            }
            else
            {
                var videos = new Models.Video
                {
                    Uri = PhotoPath,
                    Descripcion = txtdescripcion.Text
                };

                var resultado = await App.BaseDatosObject.SaveVideoRecord(videos);

                if (resultado == 1)
                {
                    await DisplayAlert("Aviso", "Video Guardado en SQlite exitósamente", "OK");
                    txtdescripcion.Text = "";
                    videoPlayer.Source = null;
                }
                else
                {
                    await DisplayAlert("Error", "Video no guardado", "OK");
                }
            }
        }

        private async void btnlistarvideo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarVideos());
        }


        public async void GrabarVideo()
        {
            try
            {
                var photo = await MediaPicker.CaptureVideoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");

                UriVideoSource uriVideoSurce = new UriVideoSource()
                {
                    Uri = PhotoPath
                };

                videoPlayer.Source = uriVideoSurce;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            PhotoPath = newFile;
        }


    }
}
