using AutoHub.Forms.Management_Menus;

namespace AutoHub.Forms
{
	public partial class MainMenu : Form
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void salesmanagementbutton_Click(object sender, EventArgs e)
		{
			SaleManagementMenu saleManagementMenu = new SaleManagementMenu();
			this.Hide();
			saleManagementMenu.ShowDialog();
			this.Close();
		}
	}
}
