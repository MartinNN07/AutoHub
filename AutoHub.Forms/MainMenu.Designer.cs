namespace AutoHub.Forms
{
    partial class MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			mainmenulabel = new Label();
			carmanagementbutton = new Button();
			customermanagementbutton = new Button();
			salespersonmanagementbutton = new Button();
			salesmanagementbutton = new Button();
			brandmanagementbutton = new Button();
			exitbutton = new Button();
			SuspendLayout();
			// 
			// mainmenulabel
			// 
			mainmenulabel.AutoSize = true;
			mainmenulabel.Font = new Font("Calibri", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			mainmenulabel.Location = new Point(414, 43);
			mainmenulabel.Name = "mainmenulabel";
			mainmenulabel.Size = new Size(256, 59);
			mainmenulabel.TabIndex = 0;
			mainmenulabel.Text = "Main Menu";
			// 
			// carmanagementbutton
			// 
			carmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			carmanagementbutton.Location = new Point(348, 161);
			carmanagementbutton.Name = "carmanagementbutton";
			carmanagementbutton.Size = new Size(378, 50);
			carmanagementbutton.TabIndex = 1;
			carmanagementbutton.Text = "Car Management";
			carmanagementbutton.UseVisualStyleBackColor = true;
			// 
			// customermanagementbutton
			// 
			customermanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			customermanagementbutton.Location = new Point(348, 217);
			customermanagementbutton.Name = "customermanagementbutton";
			customermanagementbutton.Size = new Size(378, 50);
			customermanagementbutton.TabIndex = 2;
			customermanagementbutton.Text = "Customer Management";
			customermanagementbutton.UseVisualStyleBackColor = true;
			// 
			// salespersonmanagementbutton
			// 
			salespersonmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			salespersonmanagementbutton.Location = new Point(348, 273);
			salespersonmanagementbutton.Name = "salespersonmanagementbutton";
			salespersonmanagementbutton.Size = new Size(378, 50);
			salespersonmanagementbutton.TabIndex = 3;
			salespersonmanagementbutton.Text = "Salesperson Management";
			salespersonmanagementbutton.UseVisualStyleBackColor = true;
			// 
			// salesmanagementbutton
			// 
			salesmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			salesmanagementbutton.Location = new Point(348, 329);
			salesmanagementbutton.Name = "salesmanagementbutton";
			salesmanagementbutton.Size = new Size(378, 50);
			salesmanagementbutton.TabIndex = 4;
			salesmanagementbutton.Text = "Sales Management";
			salesmanagementbutton.UseVisualStyleBackColor = true;
			salesmanagementbutton.Click += salesmanagementbutton_Click;
			// 
			// brandmanagementbutton
			// 
			brandmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			brandmanagementbutton.Location = new Point(348, 385);
			brandmanagementbutton.Name = "brandmanagementbutton";
			brandmanagementbutton.Size = new Size(378, 50);
			brandmanagementbutton.TabIndex = 5;
			brandmanagementbutton.Text = "Brand Management";
			brandmanagementbutton.UseVisualStyleBackColor = true;
			// 
			// exitbutton
			// 
			exitbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
			exitbutton.Location = new Point(348, 441);
			exitbutton.Name = "exitbutton";
			exitbutton.Size = new Size(378, 50);
			exitbutton.TabIndex = 6;
			exitbutton.Text = "Exit";
			exitbutton.UseVisualStyleBackColor = true;
			// 
			// MainMenu
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1184, 761);
			Controls.Add(exitbutton);
			Controls.Add(brandmanagementbutton);
			Controls.Add(salesmanagementbutton);
			Controls.Add(salespersonmanagementbutton);
			Controls.Add(customermanagementbutton);
			Controls.Add(carmanagementbutton);
			Controls.Add(mainmenulabel);
			Name = "MainMenu";
			Text = "Main Menu";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label mainmenulabel;
        private Button carmanagementbutton;
        private Button customermanagementbutton;
        private Button salespersonmanagementbutton;
        private Button salesmanagementbutton;
        private Button brandmanagementbutton;
        private Button exitbutton;
    }
}
