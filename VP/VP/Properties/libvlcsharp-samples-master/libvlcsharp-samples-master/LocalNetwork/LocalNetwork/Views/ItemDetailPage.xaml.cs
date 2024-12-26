﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalNetwork.ViewModels;

namespace LocalNetwork.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            viewModel.Stop();
        }

        private void VideoView_MediaPlayerChanged(object sender, LibVLCSharp.Shared.MediaPlayerChangedEventArgs e)
        {
            viewModel.Play();
        }
    }
}