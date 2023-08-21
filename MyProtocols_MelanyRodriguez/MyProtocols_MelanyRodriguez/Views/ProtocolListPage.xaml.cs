using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProtocols_MelanyRodriguez.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocols_MelanyRodriguez.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProtocolListPage : ContentPage
	{
		ProtocolViewModel protocolViewModel;
		public ProtocolListPage ()
		{
            InitializeComponent();
            BindingContext = ProtocolViewModel = new ProtocolViewModel();
		}

		private async void LoadProtocolList()
		{
			LvList.ItemsSource = await ProtocolViewModel.GetProtocolsAsync(GlobalObjects.MyLocalUser.UsuarioId);

        }
	}
}