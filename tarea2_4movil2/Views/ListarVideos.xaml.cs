using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xam.Forms.VideoPlayer;

namespace tarea2_4movil2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarVideos : ContentPage
    {
        public ListarVideos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listaVideos.ItemsSource = await App.BaseDatosObject.GetVideoList();
        }

        Models.Video video;
        private async void listaVideos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            video = (Models.Video)e.Item;

            await Navigation.PushAsync(new ReproducirVideo(this.video));

        }
    }
}