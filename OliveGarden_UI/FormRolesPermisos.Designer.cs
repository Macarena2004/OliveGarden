namespace OliveGarden_UI
{
    partial class FormRolesPermisos
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
            this.gbUsuarios = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lstUsuarios = new System.Windows.Forms.ListBox();
            this.gbArbolUsuario = new System.Windows.Forms.GroupBox();
            this.trvUsuario = new System.Windows.Forms.TreeView();
            this.gbRoles = new System.Windows.Forms.GroupBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.gbPermisos = new System.Windows.Forms.GroupBox();
            this.lstPermisos = new System.Windows.Forms.ListBox();
            this.btnAsociarRol = new System.Windows.Forms.Button();
            this.btnQuitarRol = new System.Windows.Forms.Button();
            this.btnAsociarPermiso = new System.Windows.Forms.Button();
            this.btnQuitarPermiso = new System.Windows.Forms.Button();
            this.btnAsociarPermisoARol = new System.Windows.Forms.Button();
            this.btnQuitarPermisoDeRol = new System.Windows.Forms.Button();
            this.btnNuevoRol = new System.Windows.Forms.Button();
            this.btnNuevoPermiso = new System.Windows.Forms.Button();
            this.gbUsuarios.SuspendLayout();
            this.gbArbolUsuario.SuspendLayout();
            this.gbRoles.SuspendLayout();
            this.gbPermisos.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUsuarios
            // 
            this.gbUsuarios.Controls.Add(this.lstUsuarios);
            this.gbUsuarios.Location = new System.Drawing.Point(12, 12);
            this.gbUsuarios.Name = "gbUsuarios";
            this.gbUsuarios.Size = new System.Drawing.Size(152, 331);
            this.gbUsuarios.TabIndex = 0;
            this.gbUsuarios.TabStop = false;
            this.gbUsuarios.Text = "Usuarios";
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.FormattingEnabled = true;
            this.lstUsuarios.Location = new System.Drawing.Point(6, 19);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(140, 303);
            this.lstUsuarios.TabIndex = 1;
            // 
            // gbArbolUsuario
            // 
            this.gbArbolUsuario.Controls.Add(this.trvUsuario);
            this.gbArbolUsuario.Location = new System.Drawing.Point(170, 12);
            this.gbArbolUsuario.Name = "gbArbolUsuario";
            this.gbArbolUsuario.Size = new System.Drawing.Size(158, 331);
            this.gbArbolUsuario.TabIndex = 1;
            this.gbArbolUsuario.TabStop = false;
            this.gbArbolUsuario.Text = "Roles y permisos del usuario";
            // 
            // trvUsuario
            // 
            this.trvUsuario.Location = new System.Drawing.Point(6, 17);
            this.trvUsuario.Name = "trvUsuario";
            this.trvUsuario.Size = new System.Drawing.Size(140, 305);
            this.trvUsuario.TabIndex = 0;
            // 
            // gbRoles
            // 
            this.gbRoles.Controls.Add(this.lstRoles);
            this.gbRoles.Location = new System.Drawing.Point(334, 12);
            this.gbRoles.Name = "gbRoles";
            this.gbRoles.Size = new System.Drawing.Size(152, 331);
            this.gbRoles.TabIndex = 2;
            this.gbRoles.TabStop = false;
            this.gbRoles.Text = "Roles";
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(6, 16);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(140, 303);
            this.lstRoles.TabIndex = 0;
            // 
            // gbPermisos
            // 
            this.gbPermisos.Controls.Add(this.lstPermisos);
            this.gbPermisos.Location = new System.Drawing.Point(492, 12);
            this.gbPermisos.Name = "gbPermisos";
            this.gbPermisos.Size = new System.Drawing.Size(152, 331);
            this.gbPermisos.TabIndex = 3;
            this.gbPermisos.TabStop = false;
            this.gbPermisos.Text = "Permisos del sistema";
            // 
            // lstPermisos
            // 
            this.lstPermisos.FormattingEnabled = true;
            this.lstPermisos.Location = new System.Drawing.Point(6, 16);
            this.lstPermisos.Name = "lstPermisos";
            this.lstPermisos.Size = new System.Drawing.Size(132, 303);
            this.lstPermisos.TabIndex = 0;
            // 
            // btnAsociarRol
            // 
            this.btnAsociarRol.Location = new System.Drawing.Point(18, 348);
            this.btnAsociarRol.Name = "btnAsociarRol";
            this.btnAsociarRol.Size = new System.Drawing.Size(75, 38);
            this.btnAsociarRol.TabIndex = 4;
            this.btnAsociarRol.Text = "Rol al usuario";
            this.btnAsociarRol.UseVisualStyleBackColor = true;
            // 
            // btnQuitarRol
            // 
            this.btnQuitarRol.Location = new System.Drawing.Point(99, 349);
            this.btnQuitarRol.Name = "btnQuitarRol";
            this.btnQuitarRol.Size = new System.Drawing.Size(75, 39);
            this.btnQuitarRol.TabIndex = 5;
            this.btnQuitarRol.Text = "Quitar rol";
            this.btnQuitarRol.UseVisualStyleBackColor = true;
            // 
            // btnAsociarPermiso
            // 
            this.btnAsociarPermiso.Location = new System.Drawing.Point(180, 349);
            this.btnAsociarPermiso.Name = "btnAsociarPermiso";
            this.btnAsociarPermiso.Size = new System.Drawing.Size(75, 39);
            this.btnAsociarPermiso.TabIndex = 6;
            this.btnAsociarPermiso.Text = " Permiso al usuario";
            this.btnAsociarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPermiso
            // 
            this.btnQuitarPermiso.Location = new System.Drawing.Point(261, 349);
            this.btnQuitarPermiso.Name = "btnQuitarPermiso";
            this.btnQuitarPermiso.Size = new System.Drawing.Size(75, 37);
            this.btnQuitarPermiso.TabIndex = 7;
            this.btnQuitarPermiso.Text = "Quitar permiso";
            this.btnQuitarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnAsociarPermisoARol
            // 
            this.btnAsociarPermisoARol.Location = new System.Drawing.Point(342, 349);
            this.btnAsociarPermisoARol.Name = "btnAsociarPermisoARol";
            this.btnAsociarPermisoARol.Size = new System.Drawing.Size(75, 41);
            this.btnAsociarPermisoARol.TabIndex = 8;
            this.btnAsociarPermisoARol.Text = "Permiso al rol";
            this.btnAsociarPermisoARol.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPermisoDeRol
            // 
            this.btnQuitarPermisoDeRol.Location = new System.Drawing.Point(423, 349);
            this.btnQuitarPermisoDeRol.Name = "btnQuitarPermisoDeRol";
            this.btnQuitarPermisoDeRol.Size = new System.Drawing.Size(75, 51);
            this.btnQuitarPermisoDeRol.TabIndex = 9;
            this.btnQuitarPermisoDeRol.Text = "Quitar permiso del rol";
            this.btnQuitarPermisoDeRol.UseVisualStyleBackColor = true;
            // 
            // btnNuevoRol
            // 
            this.btnNuevoRol.Location = new System.Drawing.Point(504, 363);
            this.btnNuevoRol.Name = "btnNuevoRol";
            this.btnNuevoRol.Size = new System.Drawing.Size(75, 23);
            this.btnNuevoRol.TabIndex = 10;
            this.btnNuevoRol.Text = "Nuevo Rol";
            this.btnNuevoRol.UseVisualStyleBackColor = true;
            // 
            // btnNuevoPermiso
            // 
            this.btnNuevoPermiso.Location = new System.Drawing.Point(585, 355);
            this.btnNuevoPermiso.Name = "btnNuevoPermiso";
            this.btnNuevoPermiso.Size = new System.Drawing.Size(75, 38);
            this.btnNuevoPermiso.TabIndex = 11;
            this.btnNuevoPermiso.Text = "Nuevo Permiso";
            this.btnNuevoPermiso.UseVisualStyleBackColor = true;
            // 
            // FormRolesPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 581);
            this.Controls.Add(this.btnNuevoPermiso);
            this.Controls.Add(this.btnNuevoRol);
            this.Controls.Add(this.btnQuitarPermisoDeRol);
            this.Controls.Add(this.btnAsociarPermisoARol);
            this.Controls.Add(this.btnQuitarPermiso);
            this.Controls.Add(this.btnAsociarPermiso);
            this.Controls.Add(this.btnQuitarRol);
            this.Controls.Add(this.btnAsociarRol);
            this.Controls.Add(this.gbPermisos);
            this.Controls.Add(this.gbRoles);
            this.Controls.Add(this.gbArbolUsuario);
            this.Controls.Add(this.gbUsuarios);
            this.Name = "FormRolesPermisos";
            this.Text = "FormRolesPermisos";
            this.gbUsuarios.ResumeLayout(false);
            this.gbArbolUsuario.ResumeLayout(false);
            this.gbRoles.ResumeLayout(false);
            this.gbPermisos.ResumeLayout(false);
            this.ResumeLayout(false);

            this.btnAsociarRol.Click += new System.EventHandler(this.btnAsociarRol_Click);
            this.btnQuitarRol.Click += new System.EventHandler(this.btnQuitarRol_Click);
            this.btnAsociarPermiso.Click += new System.EventHandler(this.btnAsociarPermiso_Click);
            this.btnQuitarPermiso.Click += new System.EventHandler(this.btnQuitarPermiso_Click);
            this.btnAsociarPermisoARol.Click += new System.EventHandler(this.btnAsociarPermisoARol_Click);
            this.btnQuitarPermisoDeRol.Click += new System.EventHandler(this.btnQuitarPermisoDeRol_Click);
            this.btnNuevoRol.Click += new System.EventHandler(this.btnNuevoRol_Click);
            this.btnNuevoPermiso.Click += new System.EventHandler(this.btnNuevoPermiso_Click);
            this.lstUsuarios.SelectedIndexChanged += new System.EventHandler(this.lstUsuarios_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.FormRolesPermisos_Load);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbUsuarios;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.GroupBox gbArbolUsuario;
        private System.Windows.Forms.TreeView trvUsuario;
        private System.Windows.Forms.GroupBox gbRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.GroupBox gbPermisos;
        private System.Windows.Forms.ListBox lstPermisos;
        private System.Windows.Forms.Button btnAsociarRol;
        private System.Windows.Forms.Button btnQuitarRol;
        private System.Windows.Forms.Button btnAsociarPermiso;
        private System.Windows.Forms.Button btnQuitarPermiso;
        private System.Windows.Forms.Button btnAsociarPermisoARol;
        private System.Windows.Forms.Button btnQuitarPermisoDeRol;
        private System.Windows.Forms.Button btnNuevoRol;
        private System.Windows.Forms.Button btnNuevoPermiso;
    }
}