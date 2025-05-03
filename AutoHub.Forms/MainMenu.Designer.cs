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
            mainmenulabel.Font = new Font("Calibri", 48F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            mainmenulabel.Location = new Point(382, 57);
            mainmenulabel.Name = "mainmenulabel";
            mainmenulabel.Size = new Size(404, 107);
            mainmenulabel.TabIndex = 0;
            mainmenulabel.Text = "Main Menu";
            mainmenulabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // carmanagementbutton
            // 
            carmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            carmanagementbutton.Location = new Point(382, 185);
            carmanagementbutton.Name = "carmanagementbutton";
            carmanagementbutton.Size = new Size(404, 81);
            carmanagementbutton.TabIndex = 1;
            carmanagementbutton.Text = "Car Management";
            carmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // customermanagementbutton
            // 
            customermanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            customermanagementbutton.Location = new Point(382, 272);
            customermanagementbutton.Name = "customermanagementbutton";
            customermanagementbutton.Size = new Size(404, 81);
            customermanagementbutton.TabIndex = 2;
            customermanagementbutton.Text = "Customer Management";
            customermanagementbutton.UseVisualStyleBackColor = true;
            // 
            // salespersonmanagementbutton
            // 
            salespersonmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            salespersonmanagementbutton.Location = new Point(382, 359);
            salespersonmanagementbutton.Name = "salespersonmanagementbutton";
            salespersonmanagementbutton.Size = new Size(404, 81);
            salespersonmanagementbutton.TabIndex = 3;
            salespersonmanagementbutton.Text = "Salesperson Management";
            salespersonmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // salesmanagementbutton
            // 
            salesmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            salesmanagementbutton.Location = new Point(382, 446);
            salesmanagementbutton.Name = "salesmanagementbutton";
            salesmanagementbutton.Size = new Size(404, 81);
            salesmanagementbutton.TabIndex = 4;
            salesmanagementbutton.Text = "Sales Management";
            salesmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // brandmanagementbutton
            // 
            brandmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            brandmanagementbutton.Location = new Point(382, 533);
            brandmanagementbutton.Name = "brandmanagementbutton";
            brandmanagementbutton.Size = new Size(404, 81);
            brandmanagementbutton.TabIndex = 5;
            brandmanagementbutton.Text = "Brand Management";
            brandmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // exitbutton
            // 
            exitbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            exitbutton.Location = new Point(382, 620);
            exitbutton.Name = "exitbutton";
            exitbutton.Size = new Size(404, 81);
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
