namespace AutoHub.Forms.Management_Menus
{
	partial class SaleManagementMenu
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			deleteSaleButton = new Button();
			updateSaleButton = new Button();
			addNewSaleButton = new Button();
			searchSalesByCarModelButton = new Button();
			findSaleByIDButton = new Button();
			viewAllSalesButton = new Button();
			salesMenuLabel = new Label();
			backToMainMenuButton = new Button();
			SuspendLayout();
			// 
			// deleteSaleButton
			// 
			deleteSaleButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			deleteSaleButton.Location = new Point(403, 554);
			deleteSaleButton.Name = "deleteSaleButton";
			deleteSaleButton.Size = new Size(378, 50);
			deleteSaleButton.TabIndex = 13;
			deleteSaleButton.Text = "Delete Sale";
			deleteSaleButton.UseVisualStyleBackColor = true;
			// 
			// updateSaleButton
			// 
			updateSaleButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			updateSaleButton.Location = new Point(403, 498);
			updateSaleButton.Name = "updateSaleButton";
			updateSaleButton.Size = new Size(378, 50);
			updateSaleButton.TabIndex = 12;
			updateSaleButton.Text = "Update Sale";
			updateSaleButton.UseVisualStyleBackColor = true;
			// 
			// addNewSaleButton
			// 
			addNewSaleButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			addNewSaleButton.Location = new Point(403, 442);
			addNewSaleButton.Name = "addNewSaleButton";
			addNewSaleButton.Size = new Size(378, 50);
			addNewSaleButton.TabIndex = 11;
			addNewSaleButton.Text = "Add New Sale";
			addNewSaleButton.UseVisualStyleBackColor = true;
			// 
			// searchSalesByCarModelButton
			// 
			searchSalesByCarModelButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			searchSalesByCarModelButton.Location = new Point(403, 386);
			searchSalesByCarModelButton.Name = "searchSalesByCarModelButton";
			searchSalesByCarModelButton.Size = new Size(378, 50);
			searchSalesByCarModelButton.TabIndex = 10;
			searchSalesByCarModelButton.Text = "Search Sales by Car Model";
			searchSalesByCarModelButton.UseVisualStyleBackColor = true;
			// 
			// findSaleByIDButton
			// 
			findSaleByIDButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			findSaleByIDButton.Location = new Point(403, 330);
			findSaleByIDButton.Name = "findSaleByIDButton";
			findSaleByIDButton.Size = new Size(378, 50);
			findSaleByIDButton.TabIndex = 9;
			findSaleByIDButton.Text = "Find Sale by ID";
			findSaleByIDButton.UseVisualStyleBackColor = true;
			// 
			// viewAllSalesButton
			// 
			viewAllSalesButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			viewAllSalesButton.Location = new Point(403, 274);
			viewAllSalesButton.Name = "viewAllSalesButton";
			viewAllSalesButton.Size = new Size(378, 50);
			viewAllSalesButton.TabIndex = 8;
			viewAllSalesButton.Text = "View All Sales";
			viewAllSalesButton.UseVisualStyleBackColor = true;
			// 
			// salesMenuLabel
			// 
			salesMenuLabel.AutoSize = true;
			salesMenuLabel.Font = new Font("Calibri", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			salesMenuLabel.Location = new Point(469, 156);
			salesMenuLabel.Name = "salesMenuLabel";
			salesMenuLabel.Size = new Size(235, 59);
			salesMenuLabel.TabIndex = 7;
			salesMenuLabel.Text = "Sale Menu";
			// 
			// backToMainMenuButton
			// 
			backToMainMenuButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			backToMainMenuButton.Location = new Point(403, 610);
			backToMainMenuButton.Name = "backToMainMenuButton";
			backToMainMenuButton.Size = new Size(378, 50);
			backToMainMenuButton.TabIndex = 13;
			backToMainMenuButton.Text = "Back to Main Menu";
			backToMainMenuButton.UseVisualStyleBackColor = true;
			backToMainMenuButton.Click += backToMainMenuButton_Click;
			// 
			// SaleManagementMenu
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1184, 761);
			Controls.Add(backToMainMenuButton);
			Controls.Add(deleteSaleButton);
			Controls.Add(updateSaleButton);
			Controls.Add(addNewSaleButton);
			Controls.Add(searchSalesByCarModelButton);
			Controls.Add(findSaleByIDButton);
			Controls.Add(viewAllSalesButton);
			Controls.Add(salesMenuLabel);
			Name = "SaleManagementMenu";
			Text = "SaleManagementMenu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button deleteSaleButton;
		private Button updateSaleButton;
		private Button addNewSaleButton;
		private Button searchSalesByCarModelButton;
		private Button findSaleByIDButton;
		private Button viewAllSalesButton;
		private Label salesMenuLabel;
		private Button backToMainMenuButton;
	}
}