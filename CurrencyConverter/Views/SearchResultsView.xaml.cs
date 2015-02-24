using CurrencyConvertor.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240

namespace CurrencyConvertor.Views
{
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResultsView : CurrencyConvertor.Common.LayoutAwarePage
    {
        ViewModels.SearchViewModel currentViewModel = null;

        public SearchResultsView()
        {
            this.InitializeComponent();

            this.currentViewModel = new ViewModels.SearchViewModel();

            this.DataContext = this.currentViewModel;
            Frame rootFrame = Window.Current.Content as Frame;

            var viewModel = this.DataContext as SearchViewModel;
            viewModel.Frame = rootFrame;
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var queryText = navigationParameter as String;
            this.currentViewModel.QueryText = queryText;
        }

        private void ResultsGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            //TODO: View Details Page for each result
        }
    }
}
