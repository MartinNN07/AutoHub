namespace AutoHub.Forms.Management_Menus
{
	partial class SalespersonManagmentMenu
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
			backToMainMenuButton = new Button();
			deleteSalespersonButton = new Button();
			updateSalespersonButton = new Button();
			addNewSalespersonButton = new Button();
			searchSalespersonsByCarModelButton = new Button();
			findSalespersonByIDButton = new Button();
			viewAllSalespersonsButton = new Button();
			salespersonMenuLabel = new Label();
			SuspendLayout();
			// 
			// backToMainMenuButton
			// 
			backToMainMenuButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			backToMainMenuButton.Location = new Point(403, 582);
			backToMainMenuButton.Name = "backToMainMenuButton";
			backToMainMenuButton.Size = new Size(378, 50);
			backToMainMenuButton.TabIndex = 20;
			backToMainMenuButton.Text = "Back to Main Menu";
			backToMainMenuButton.UseVisualStyleBackColor = true;
			backToMainMenuButton.Click += backToMainMenuButton_Click;
			// 
			// deleteSalespersonButton
			// 
			deleteSalespersonButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			deleteSalespersonButton.Location = new Point(403, 526);
			deleteSalespersonButton.Name = "deleteSalespersonButton";
			deleteSalespersonButton.Size = new Size(378, 50);
			deleteSalespersonButton.TabIndex = 21;
			deleteSalespersonButton.Text = "Delete Salesperson";
			deleteSalespersonButton.UseVisualStyleBackColor = true;
			// 
			// updateSalespersonButton
			// 
			updateSalespersonButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			updateSalespersonButton.Location = new Point(403, 470);
			updateSalespersonButton.Name = "updateSalespersonButton";
			updateSalespersonButton.Size = new Size(378, 50);
			updateSalespersonButton.TabIndex = 19;
			updateSalespersonButton.Text = "Update Salesperson";
			updateSalespersonButton.UseVisualStyleBackColor = true;
			// 
			// addNewSalespersonButton
			// 
			addNewSalespersonButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			addNewSalespersonButton.Location = new Point(403, 414);
			addNewSalespersonButton.Name = "addNewSalespersonButton";
			addNewSalespersonButton.Size = new Size(378, 50);
			addNewSalespersonButton.TabIndex = 18;
			addNewSalespersonButton.Text = "Add New Salesperson";
			addNewSalespersonButton.UseVisualStyleBackColor = true;
			// 
			// searchSalespersonsByCarModelButton
			// 
			searchSalespersonsByCarModelButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			searchSalespersonsByCarModelButton.Location = new Point(403, 358);
			searchSalespersonsByCarModelButton.Name = "searchSalespersonsByCarModelButton";
			searchSalespersonsByCarModelButton.Size = new Size(378, 50);
			searchSalespersonsByCarModelButton.TabIndex = 17;
			searchSalespersonsByCarModelButton.Text = "Search Salespersons by Car Model";
			searchSalespersonsByCarModelButton.UseVisualStyleBackColor = true;
			// 
			// findSalespersonByIDButton
			// 
			findSalespersonByIDButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			findSalespersonByIDButton.Location = new Point(403, 302);
			findSalespersonByIDButton.Name = "findSalespersonByIDButton";
			findSalespersonByIDButton.Size = new Size(378, 50);
			findSalespersonByIDButton.TabIndex = 16;
			findSalespersonByIDButton.Text = "Find Salesperson by ID";
			findSalespersonByIDButton.UseVisualStyleBackColor = true;
			// 
			// viewAllSalespersonsButton
			// 
			viewAllSalespersonsButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			viewAllSalespersonsButton.Location = new Point(403, 246);
			viewAllSalespersonsButton.Name = "viewAllSalespersonsButton";
			viewAllSalespersonsButton.Size = new Size(378, 50);
			viewAllSalespersonsButton.TabIndex = 15;
			viewAllSalespersonsButton.Text = "View All Salespersons";
			viewAllSalespersonsButton.UseVisualStyleBackColor = true;
			// 
			// salespersonMenuLabel
			// 
			salespersonMenuLabel.AutoSize = true;
			salespersonMenuLabel.Font = new Font("Calibri", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			salespersonMenuLabel.Location = new Point(392, 156);
			salespersonMenuLabel.Name = "salespersonMenuLabel";
			salespersonMenuLabel.Size = new Size(389, 59);
			salespersonMenuLabel.TabIndex = 14;
			salespersonMenuLabel.Text = "Salesperson Menu";
			// 
			// SalespersonManagmentMenu
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1184, 761);
			Controls.Add(backToMainMenuButton);
			Controls.Add(deleteSalespersonButton);
			Controls.Add(updateSalespersonButton);
			Controls.Add(addNewSalespersonButton);
			Controls.Add(searchSalespersonsByCarModelButton);
			Controls.Add(findSalespersonByIDButton);
			Controls.Add(viewAllSalespersonsButton);
			Controls.Add(salespersonMenuLabel);
			Name = "SalespersonManagmentMenu";
			Text = "SalespersonManagmentMenu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button backToMainMenuButton;
		private Button deleteSalespersonButton;
		private Button updateSalespersonButton;
		private Button addNewSalespersonButton;
		private Button searchSalespersonsByCarModelButton;
		private Button findSalespersonByIDButton;
		private Button viewAllSalespersonsButton;
		private Label salespersonMenuLabel;
	}
}