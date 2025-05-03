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
            findcarbyidbutton = new Button();
            viewallcarsbutton = new Button();
            carmanagementlabel = new Label();
            SuspendLayout();
            // 
            // exitbutton
            // 
            exitbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            exitbutton.Location = new Point(390, 621);
            exitbutton.Name = "exitbutton";
            exitbutton.Size = new Size(404, 81);
            exitbutton.TabIndex = 13;
            exitbutton.Text = "Exit";
            exitbutton.UseVisualStyleBackColor = true;
            // 
            // brandmanagementbutton
            // 
            brandmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            brandmanagementbutton.Location = new Point(390, 534);
            brandmanagementbutton.Name = "brandmanagementbutton";
            brandmanagementbutton.Size = new Size(404, 81);
            brandmanagementbutton.TabIndex = 12;
            brandmanagementbutton.Text = "Brand Management";
            brandmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // salesmanagementbutton
            // 
            salesmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            salesmanagementbutton.Location = new Point(390, 447);
            salesmanagementbutton.Name = "salesmanagementbutton";
            salesmanagementbutton.Size = new Size(404, 81);
            salesmanagementbutton.TabIndex = 11;
            salesmanagementbutton.Text = "Sales Management";
            salesmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // salespersonmanagementbutton
            // 
            salespersonmanagementbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            salespersonmanagementbutton.Location = new Point(390, 360);
            salespersonmanagementbutton.Name = "salespersonmanagementbutton";
            salespersonmanagementbutton.Size = new Size(404, 81);
            salespersonmanagementbutton.TabIndex = 10;
            salespersonmanagementbutton.Text = "Salesperson Management";
            salespersonmanagementbutton.UseVisualStyleBackColor = true;
            // 
            // findcarbyidbutton
            // 
            findcarbyidbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            findcarbyidbutton.Location = new Point(390, 273);
            findcarbyidbutton.Name = "findcarbyidbutton";
            findcarbyidbutton.Size = new Size(404, 81);
            findcarbyidbutton.TabIndex = 9;
            findcarbyidbutton.Text = "Find Car by ID";
            findcarbyidbutton.UseVisualStyleBackColor = true;
            // 
            // viewallcarsbutton
            // 
            viewallcarsbutton.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            viewallcarsbutton.Location = new Point(390, 186);
            viewallcarsbutton.Name = "viewallcarsbutton";
            viewallcarsbutton.Size = new Size(404, 81);
            viewallcarsbutton.TabIndex = 8;
            viewallcarsbutton.Text = "View All Cars";
            viewallcarsbutton.UseVisualStyleBackColor = true;
            // 
            // carmanagementlabel
            // 
            carmanagementlabel.Font = new Font("Calibri", 48F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            carmanagementlabel.Location = new Point(336, 53);
            carmanagementlabel.Name = "carmanagementlabel";
            carmanagementlabel.Size = new Size(497, 107);
            carmanagementlabel.TabIndex = 7;
            carmanagementlabel.Text = "Car Management";
            carmanagementlabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CarManagementMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(exitbutton);
            Controls.Add(brandmanagementbutton);
            Controls.Add(salesmanagementbutton);
            Controls.Add(salespersonmanagementbutton);
            Controls.Add(findcarbyidbutton);
            Controls.Add(viewallcarsbutton);
            Controls.Add(carmanagementlabel);
            Name = "CarManagementMenu";
            Text = "CarManagementMenu";
            ResumeLayout(false);
        }

        #endregion

        private Button exitbutton;
        private Button brandmanagementbutton;
        private Button salesmanagementbutton;
        private Button salespersonmanagementbutton;
        private Button findcarbyidbutton;
        private Button viewallcarsbutton;
        private Label carmanagementlabel;
    }
}