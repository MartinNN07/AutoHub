namespace AutoHub.Forms.Management_Menus
{
    partial class CarManagementMenu
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
            exitbutton = new Button();
            brandmanagementbutton = new Button();
            salesmanagementbutton = new Button();
            salespersonmanagementbutton = new Button();
            customermanagementbutton = new Button();
            carmanagementbutton = new Button();
            mainmenulabel = new Label();
            SuspendLayout();
            // 
            // exitbutton
            // 
            exitbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            exitbutton.Location = new Point(218, 386);
            exitbutton.Name = "exitbutton";
            exitbutton.Size = new Size(378, 50);
            exitbutton.TabIndex = 12;
            exitbutton.Text = "Exit";
            exitbutton.UseVisualStyleBackColor = true;
            // 
            // brandmanagementbutton
            // 
            brandmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            brandmanagementbutton.Location = new Point(218, 330);
            brandmanagementbutton.Name = "brandmanagementbutton";
            brandmanagementbutton.Size = new Size(378, 50);
            brandmanagementbutton.TabIndex = 11;
            brandmanagementbutton.Text = "Brand Management";
            brandmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // salesmanagementbutton
            // 
            salesmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            salesmanagementbutton.Location = new Point(218, 274);
            salesmanagementbutton.Name = "salesmanagementbutton";
            salesmanagementbutton.Size = new Size(378, 50);
            salesmanagementbutton.TabIndex = 10;
            salesmanagementbutton.Text = "Sales Management";
            salesmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // salespersonmanagementbutton
            // 
            salespersonmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            salespersonmanagementbutton.Location = new Point(218, 218);
            salespersonmanagementbutton.Name = "salespersonmanagementbutton";
            salespersonmanagementbutton.Size = new Size(378, 50);
            salespersonmanagementbutton.TabIndex = 9;
            salespersonmanagementbutton.Text = "Salesperson Management";
            salespersonmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // customermanagementbutton
            // 
            customermanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            customermanagementbutton.Location = new Point(218, 162);
            customermanagementbutton.Name = "customermanagementbutton";
            customermanagementbutton.Size = new Size(378, 50);
            customermanagementbutton.TabIndex = 8;
            customermanagementbutton.Text = "Customer Management";
            customermanagementbutton.UseVisualStyleBackColor = true;
            // 
            // carmanagementbutton
            // 
            carmanagementbutton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            carmanagementbutton.Location = new Point(218, 106);
            carmanagementbutton.Name = "carmanagementbutton";
            carmanagementbutton.Size = new Size(378, 50);
            carmanagementbutton.TabIndex = 7;
            carmanagementbutton.Text = "Car Management";
            carmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // mainmenulabel
            // 
            mainmenulabel.AutoSize = true;
            mainmenulabel.Font = new Font("Calibri", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            mainmenulabel.Location = new Point(277, 9);
            mainmenulabel.Name = "mainmenulabel";
            mainmenulabel.Size = new Size(256, 59);
            mainmenulabel.TabIndex = 13;
            mainmenulabel.Text = "Main Menu";
            // 
            // CarManagementMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(849, 487);
            Controls.Add(mainmenulabel);
            Controls.Add(exitbutton);
            Controls.Add(brandmanagementbutton);
            Controls.Add(salesmanagementbutton);
            Controls.Add(salespersonmanagementbutton);
            Controls.Add(customermanagementbutton);
            Controls.Add(carmanagementbutton);
            Name = "CarManagementMenu";
            Text = "CarManagementMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exitbutton;
        private Button brandmanagementbutton;
        private Button salesmanagementbutton;
        private Button salespersonmanagementbutton;
        private Button customermanagementbutton;
        private Button carmanagementbutton;
        private Label mainmenulabel;
    }
}