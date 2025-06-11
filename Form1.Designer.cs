namespace TOOLS_eVOT
    {
    partial class Form1
        {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
            {
            if (disposing && (components != null))
                {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
            {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.status = new System.Windows.Forms.TextBox();
            this.obreINput = new System.Windows.Forms.OpenFileDialog();
            this.DesaOutput = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.censNomésPDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nomésÚnicsIActiusLDAPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarCensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarCENS = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportUnicsLDAP = new System.Windows.Forms.ToolStripMenuItem();
            this.comptesBloquejatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comptesNoActivatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.censElegibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.lleidaSemiobertaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.llistaObertaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cEnsElegibleMultiEleccióToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarEleccionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totesElegiblesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loaderBar = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btStop = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(16, 57);
            this.status.Margin = new System.Windows.Forms.Padding(4);
            this.status.Multiline = true;
            this.status.Name = "status";
            this.status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.status.Size = new System.Drawing.Size(841, 331);
            this.status.TabIndex = 0;
            this.status.Text = "                     ";
            // 
            // obreINput
            // 
            this.obreINput.DefaultExt = "*.csv";
            this.obreINput.FileName = "*.csv";
            this.obreINput.Filter = "\"CSV|*.csv\"";
            this.obreINput.Multiselect = true;
            this.obreINput.Title = "Carregar arxiu CSV amb Cens";
            // 
            // DesaOutput
            // 
            this.DesaOutput.DefaultExt = "*.csv";
            this.DesaOutput.FileName = "PosaAquiElNomDelArxiuSortida.csv";
            this.DesaOutput.Filter = "\"CSV|*.csv\"";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.censNomésPDIToolStripMenuItem,
            this.importarCensToolStripMenuItem,
            this.censElegibleToolStripMenuItem,
            this.cEnsElegibleMultiEleccióToolStripMenuItem,
            this.sortirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(891, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // censNomésPDIToolStripMenuItem
            // 
            this.censNomésPDIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarToolStripMenuItem,
            this.exportarToolStripMenuItem1});
            this.censNomésPDIToolStripMenuItem.Name = "censNomésPDIToolStripMenuItem";
            this.censNomésPDIToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.censNomésPDIToolStripMenuItem.Text = "Cens només PDI";
            // 
            // carregarToolStripMenuItem
            // 
            this.carregarToolStripMenuItem.Name = "carregarToolStripMenuItem";
            this.carregarToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.carregarToolStripMenuItem.Text = "Carregar";
            this.carregarToolStripMenuItem.Click += new System.EventHandler(this.carregarToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem1
            // 
            this.exportarToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nomésÚnicsIActiusLDAPToolStripMenuItem});
            this.exportarToolStripMenuItem1.Name = "exportarToolStripMenuItem1";
            this.exportarToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.exportarToolStripMenuItem1.Text = "Exportar";
            // 
            // nomésÚnicsIActiusLDAPToolStripMenuItem
            // 
            this.nomésÚnicsIActiusLDAPToolStripMenuItem.Name = "nomésÚnicsIActiusLDAPToolStripMenuItem";
            this.nomésÚnicsIActiusLDAPToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.nomésÚnicsIActiusLDAPToolStripMenuItem.Text = "Només únics i actius LDAP";
            this.nomésÚnicsIActiusLDAPToolStripMenuItem.Click += new System.EventHandler(this.nomésÚnicsIActiusLDAPToolStripMenuItem_Click);
            // 
            // importarCensToolStripMenuItem
            // 
            this.importarCensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarCENS,
            this.exportarToolStripMenuItem});
            this.importarCensToolStripMenuItem.Name = "importarCensToolStripMenuItem";
            this.importarCensToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.importarCensToolStripMenuItem.Text = "Cens només Alumnes";
            // 
            // carregarCENS
            // 
            this.carregarCENS.Name = "carregarCENS";
            this.carregarCENS.Size = new System.Drawing.Size(152, 24);
            this.carregarCENS.Text = "Carregar";
            this.carregarCENS.Click += new System.EventHandler(this.totalÚnicsINoActiusALDAPToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportUnicsLDAP,
            this.comptesBloquejatsToolStripMenuItem,
            this.comptesNoActivatsToolStripMenuItem});
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.exportarToolStripMenuItem.Text = "Exportar";
            // 
            // ExportUnicsLDAP
            // 
            this.ExportUnicsLDAP.Name = "ExportUnicsLDAP";
            this.ExportUnicsLDAP.Size = new System.Drawing.Size(256, 24);
            this.ExportUnicsLDAP.Text = "Només Únics i Actius LDAP";
            this.ExportUnicsLDAP.Click += new System.EventHandler(this.ExportUnicsLDAP_Click);
            // 
            // comptesBloquejatsToolStripMenuItem
            // 
            this.comptesBloquejatsToolStripMenuItem.Name = "comptesBloquejatsToolStripMenuItem";
            this.comptesBloquejatsToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.comptesBloquejatsToolStripMenuItem.Text = "Comptes bloquejats";
            this.comptesBloquejatsToolStripMenuItem.Click += new System.EventHandler(this.comptesBloquejatsToolStripMenuItem_Click);
            // 
            // comptesNoActivatsToolStripMenuItem
            // 
            this.comptesNoActivatsToolStripMenuItem.Name = "comptesNoActivatsToolStripMenuItem";
            this.comptesNoActivatsToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.comptesNoActivatsToolStripMenuItem.Text = "Comptes no activats";
            this.comptesNoActivatsToolStripMenuItem.Click += new System.EventHandler(this.comptesNoActivatsToolStripMenuItem_Click);
            // 
            // censElegibleToolStripMenuItem
            // 
            this.censElegibleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarToolStripMenuItem1,
            this.exportarToolStripMenuItem2});
            this.censElegibleToolStripMenuItem.Name = "censElegibleToolStripMenuItem";
            this.censElegibleToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.censElegibleToolStripMenuItem.Text = "Cens elegible";
            // 
            // carregarToolStripMenuItem1
            // 
            this.carregarToolStripMenuItem1.Name = "carregarToolStripMenuItem1";
            this.carregarToolStripMenuItem1.Size = new System.Drawing.Size(159, 24);
            this.carregarToolStripMenuItem1.Text = "Carregar";
            this.carregarToolStripMenuItem1.Click += new System.EventHandler(this.carregarToolStripMenuItem1_Click);
            // 
            // exportarToolStripMenuItem2
            // 
            this.exportarToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lleidaSemiobertaToolStripMenuItem,
            this.llistaObertaToolStripMenuItem});
            this.exportarToolStripMenuItem2.Name = "exportarToolStripMenuItem2";
            this.exportarToolStripMenuItem2.Size = new System.Drawing.Size(159, 24);
            this.exportarToolStripMenuItem2.Text = "Exportar a ...";
            this.exportarToolStripMenuItem2.Click += new System.EventHandler(this.exportarToolStripMenuItem2_Click);
            // 
            // lleidaSemiobertaToolStripMenuItem
            // 
            this.lleidaSemiobertaToolStripMenuItem.Name = "lleidaSemiobertaToolStripMenuItem";
            this.lleidaSemiobertaToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.lleidaSemiobertaToolStripMenuItem.Text = "Llista semi-oberta";
            this.lleidaSemiobertaToolStripMenuItem.Click += new System.EventHandler(this.lleidaSemiobertaToolStripMenuItem_Click);
            // 
            // llistaObertaToolStripMenuItem
            // 
            this.llistaObertaToolStripMenuItem.Name = "llistaObertaToolStripMenuItem";
            this.llistaObertaToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.llistaObertaToolStripMenuItem.Text = "Llista Oberta ";
            this.llistaObertaToolStripMenuItem.Click += new System.EventHandler(this.llistaObertaToolStripMenuItem_Click);
            // 
            // cEnsElegibleMultiEleccióToolStripMenuItem
            // 
            this.cEnsElegibleMultiEleccióToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarToolStripMenuItem2,
            this.carregarEleccionsToolStripMenuItem,
            this.exportarAToolStripMenuItem});
            this.cEnsElegibleMultiEleccióToolStripMenuItem.Name = "cEnsElegibleMultiEleccióToolStripMenuItem";
            this.cEnsElegibleMultiEleccióToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.cEnsElegibleMultiEleccióToolStripMenuItem.Text = "Cens Elegible Multi Elecció";
            // 
            // carregarToolStripMenuItem2
            // 
            this.carregarToolStripMenuItem2.Name = "carregarToolStripMenuItem2";
            this.carregarToolStripMenuItem2.Size = new System.Drawing.Size(200, 24);
            this.carregarToolStripMenuItem2.Text = "Carregar";
            this.carregarToolStripMenuItem2.Click += new System.EventHandler(this.carregarToolStripMenuItem2_Click);
            // 
            // carregarEleccionsToolStripMenuItem
            // 
            this.carregarEleccionsToolStripMenuItem.Name = "carregarEleccionsToolStripMenuItem";
            this.carregarEleccionsToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.carregarEleccionsToolStripMenuItem.Text = "Carregar Eleccions";
            this.carregarEleccionsToolStripMenuItem.ToolTipText = "Carregar informació dels llocs a cobrir per cada Id àlies d\'elecció.";
            this.carregarEleccionsToolStripMenuItem.Click += new System.EventHandler(this.carregarEleccionsToolStripMenuItem_Click);
            // 
            // exportarAToolStripMenuItem
            // 
            this.exportarAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totesElegiblesToolStripMenuItem});
            this.exportarAToolStripMenuItem.Name = "exportarAToolStripMenuItem";
            this.exportarAToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.exportarAToolStripMenuItem.Text = "Exportar a...";
            this.exportarAToolStripMenuItem.Click += new System.EventHandler(this.exportarAToolStripMenuItem_Click);
            // 
            // totesElegiblesToolStripMenuItem
            // 
            this.totesElegiblesToolStripMenuItem.Name = "totesElegiblesToolStripMenuItem";
            this.totesElegiblesToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.totesElegiblesToolStripMenuItem.Text = "Totes Elegibles";
            this.totesElegiblesToolStripMenuItem.Click += new System.EventHandler(this.totesElegiblesToolStripMenuItem_Click);
            // 
            // sortirToolStripMenuItem
            // 
            this.sortirToolStripMenuItem.Name = "sortirToolStripMenuItem";
            this.sortirToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.sortirToolStripMenuItem.Text = "Sortir";
            this.sortirToolStripMenuItem.Click += new System.EventHandler(this.sortirToolStripMenuItem_Click);
            // 
            // loaderBar
            // 
            this.loaderBar.Location = new System.Drawing.Point(17, 396);
            this.loaderBar.Margin = new System.Windows.Forms.Padding(4);
            this.loaderBar.Name = "loaderBar";
            this.loaderBar.Size = new System.Drawing.Size(841, 28);
            this.loaderBar.TabIndex = 2;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(159, 438);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(59, 17);
            this.lbStatus.TabIndex = 3;
            this.lbStatus.Text = "lbStatus";
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(17, 432);
            this.btStop.Margin = new System.Windows.Forms.Padding(4);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(100, 28);
            this.btStop.TabIndex = 4;
            this.btStop.Text = "Stop!";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 465);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.loaderBar);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Tool eVOT - UdL ( 2025 3.1 )";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.OpenFileDialog obreINput;
        private System.Windows.Forms.SaveFileDialog DesaOutput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem importarCensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarCENS;
        private System.Windows.Forms.ToolStripMenuItem sortirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportUnicsLDAP;
        private System.Windows.Forms.ProgressBar loaderBar;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ToolStripMenuItem comptesBloquejatsToolStripMenuItem;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.ToolStripMenuItem comptesNoActivatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem censNomésPDIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nomésÚnicsIActiusLDAPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem censElegibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem lleidaSemiobertaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem llistaObertaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cEnsElegibleMultiEleccióToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exportarAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totesElegiblesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarEleccionsToolStripMenuItem;
        }
    }

