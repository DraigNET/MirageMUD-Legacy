using System.Drawing;
using System.Windows.Forms;

namespace Client.Forms
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        // Container Panels
        private Panel mnuMainMenu;
        private Panel mnuNewCharacter;
        private Panel mnuNewAccount;
        private Panel mnuChars;
        private Panel mnuLogin;
        private Panel mnuIPConfig;
        private Panel mnuGameOptions;
        private Label lblMainMenuT;
        private Label lblMainMenu_NewAccount;
        private Label lblMainMenu_Exit;
        private Label lblMainMenu_Login;
        private Label lblMainMenu_GameOptions;
        private Label lblMainMenu_IPConfig;

        // New Account controls
        private TextBox txtNewAcctName;
        private TextBox txtNewAcctEmail;
        private Label lblNewAccountTitle;
        private Label lblNewAccountInfo;
        private Label lblNewAccountName;
        private Label lblNewAccountEmail;
        private Label lblNewAccountCancel;

        // Characters controls
        private ListBox lstChars;
        private PictureBox picSelChar;
        private Label lblCharsUse;
        private Label lblCharsNew;
        private Label lblCharsDelete;
        private Label lblCharsCancel;
        private Label lblCharsTitle;

        // Login controls
        private TextBox txtLoginName;
        private TextBox txtLoginPassword;
        private CheckBox chkLogin;
        private Label lblLoginTitle;
        private Label lblLoginInfo;
        private Label lblLoginName;
        private Label lblLoginPassword;
        private Label lblLoginRemember;
        private Label lblLoginConnect;
        private Label lblLoginCancel;

        // IP Config controls
        private TextBox txtIP;
        private TextBox txtPort;
        private Label lblIPConfigTitle;
        private Label lblIPLabel;
        private Label lblPortLabel;
        private Label lblIPConfigSave;
        private Label lblIPConfigCancel;

        // Game Options controls
        private CheckBox chkMusic;
        private CheckBox chkSound;
        private TextBox txtFonts;
        private ComboBox cmbFonts;
        private Label lblGameOptionsTitle;
        private Label lblMusicLabel;
        private Label lblSoundLabel;
        private Label lblFontLabel;
        private Label lblGameOptionsSave;
        private Label lblGameOptionsCancel;

        // New Character controls
        private Label lblNewCharTitle;
        private Label lblNameLabel;
        private TextBox txtNewCharName;
        private Label lblClassLabel;
        private ComboBox cmbClass;
        private Label lblGenderLabel;
        private RadioButton optMale;
        private RadioButton optFemale;
        private PictureBox picPic;
        private Label lblAvatarPrev;
        private Label lblAvatarNext;
        private Label lblNewCharCreate;
        private Label lblNewCharCancel;
        private Label lblSTRLabel;
        private Label lblHPLabel;
        private Label lblMPLabel;
        private Label lblSPLabel;
        private Label lblMAGILabel;
        private Label lblSPEEDLabel;
        private Label lblSTR;
        private Label lblHP;
        private Label lblMP;
        private Label lblSP;
        private Label lblMAGI;
        private Label lblSPEED;
        private Label lblDEFLabel;
        private Label lblDEF;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            mnuNewCharacter = new Panel();
            lblDEF = new Label();
            lblDEFLabel = new Label();
            lblNewCharTitle = new Label();
            lblNameLabel = new Label();
            txtNewCharName = new TextBox();
            lblClassLabel = new Label();
            cmbClass = new ComboBox();
            lblGenderLabel = new Label();
            optMale = new RadioButton();
            optFemale = new RadioButton();
            picPic = new PictureBox();
            lblAvatarPrev = new Label();
            lblAvatarNext = new Label();
            lblNewCharCreate = new Label();
            lblNewCharCancel = new Label();
            lblSTRLabel = new Label();
            lblHPLabel = new Label();
            lblMPLabel = new Label();
            lblSPLabel = new Label();
            lblMAGILabel = new Label();
            lblSPEEDLabel = new Label();
            lblSTR = new Label();
            lblHP = new Label();
            lblMP = new Label();
            lblSP = new Label();
            lblMAGI = new Label();
            lblSPEED = new Label();
            mnuNewAccount = new Panel();
            label1 = new Label();
            txtNewAcctPassword = new TextBox();
            lblNewAccountTitle = new Label();
            lblNewAccountInfo = new Label();
            lblNewAccountName = new Label();
            txtNewAcctName = new TextBox();
            lblNewAccountEmail = new Label();
            txtNewAcctEmail = new TextBox();
            lblNewAccountCancel = new Label();
            mnuChars = new Panel();
            lstChars = new ListBox();
            picSelChar = new PictureBox();
            lblCharsUse = new Label();
            lblCharsNew = new Label();
            lblCharsDelete = new Label();
            lblCharsCancel = new Label();
            lblCharsTitle = new Label();
            mnuLogin = new Panel();
            lblLoginTitle = new Label();
            lblLoginInfo = new Label();
            lblLoginName = new Label();
            txtLoginName = new TextBox();
            lblLoginPassword = new Label();
            txtLoginPassword = new TextBox();
            chkLogin = new CheckBox();
            lblLoginRemember = new Label();
            lblLoginConnect = new Label();
            lblLoginCancel = new Label();
            mnuIPConfig = new Panel();
            lblIPConfigTitle = new Label();
            lblIPLabel = new Label();
            txtIP = new TextBox();
            lblPortLabel = new Label();
            txtPort = new TextBox();
            lblIPConfigSave = new Label();
            lblIPConfigCancel = new Label();
            mnuGameOptions = new Panel();
            lblGameOptionsTitle = new Label();
            chkMusic = new CheckBox();
            lblMusicLabel = new Label();
            chkSound = new CheckBox();
            lblSoundLabel = new Label();
            txtFonts = new TextBox();
            lblFontLabel = new Label();
            cmbFonts = new ComboBox();
            lblGameOptionsSave = new Label();
            lblGameOptionsCancel = new Label();
            mnuMainMenu = new Panel();
            lblMainMenuT = new Label();
            lblMainMenu_NewAccount = new Label();
            lblMainMenu_Exit = new Label();
            lblMainMenu_Login = new Label();
            lblMainMenu_GameOptions = new Label();
            lblMainMenu_IPConfig = new Label();
            lblNewAccountConnect = new Label();
            mnuNewCharacter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPic).BeginInit();
            mnuNewAccount.SuspendLayout();
            mnuChars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSelChar).BeginInit();
            mnuLogin.SuspendLayout();
            mnuIPConfig.SuspendLayout();
            mnuGameOptions.SuspendLayout();
            mnuMainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // mnuNewCharacter
            // 
            mnuNewCharacter.BackColor = Color.Black;
            mnuNewCharacter.BackgroundImage = (Image)resources.GetObject("mnuNewCharacter.BackgroundImage");
            mnuNewCharacter.BackgroundImageLayout = ImageLayout.None;
            mnuNewCharacter.Controls.Add(lblDEF);
            mnuNewCharacter.Controls.Add(lblDEFLabel);
            mnuNewCharacter.Controls.Add(lblNewCharTitle);
            mnuNewCharacter.Controls.Add(lblNameLabel);
            mnuNewCharacter.Controls.Add(txtNewCharName);
            mnuNewCharacter.Controls.Add(lblClassLabel);
            mnuNewCharacter.Controls.Add(cmbClass);
            mnuNewCharacter.Controls.Add(lblGenderLabel);
            mnuNewCharacter.Controls.Add(optMale);
            mnuNewCharacter.Controls.Add(optFemale);
            mnuNewCharacter.Controls.Add(picPic);
            mnuNewCharacter.Controls.Add(lblAvatarPrev);
            mnuNewCharacter.Controls.Add(lblAvatarNext);
            mnuNewCharacter.Controls.Add(lblNewCharCreate);
            mnuNewCharacter.Controls.Add(lblNewCharCancel);
            mnuNewCharacter.Controls.Add(lblSTRLabel);
            mnuNewCharacter.Controls.Add(lblHPLabel);
            mnuNewCharacter.Controls.Add(lblMPLabel);
            mnuNewCharacter.Controls.Add(lblSPLabel);
            mnuNewCharacter.Controls.Add(lblMAGILabel);
            mnuNewCharacter.Controls.Add(lblSPEEDLabel);
            mnuNewCharacter.Controls.Add(lblSTR);
            mnuNewCharacter.Controls.Add(lblHP);
            mnuNewCharacter.Controls.Add(lblMP);
            mnuNewCharacter.Controls.Add(lblSP);
            mnuNewCharacter.Controls.Add(lblMAGI);
            mnuNewCharacter.Controls.Add(lblSPEED);
            mnuNewCharacter.ForeColor = Color.Goldenrod;
            mnuNewCharacter.Location = new Point(0, 0);
            mnuNewCharacter.Name = "mnuNewCharacter";
            mnuNewCharacter.Size = new Size(500, 300);
            mnuNewCharacter.TabIndex = 7;
            mnuNewCharacter.Visible = false;
            // 
            // lblDEF
            // 
            lblDEF.BackColor = Color.Transparent;
            lblDEF.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblDEF.ForeColor = Color.White;
            lblDEF.Location = new Point(385, 173);
            lblDEF.Name = "lblDEF";
            lblDEF.Size = new Size(35, 20);
            lblDEF.TabIndex = 26;
            lblDEF.Text = "0";
            lblDEF.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDEFLabel
            // 
            lblDEFLabel.BackColor = Color.Transparent;
            lblDEFLabel.ForeColor = Color.Goldenrod;
            lblDEFLabel.Location = new Point(344, 172);
            lblDEFLabel.Name = "lblDEFLabel";
            lblDEFLabel.Size = new Size(35, 20);
            lblDEFLabel.TabIndex = 25;
            lblDEFLabel.Text = "DEF :";
            lblDEFLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblNewCharTitle
            // 
            lblNewCharTitle.BackColor = Color.Transparent;
            lblNewCharTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblNewCharTitle.ForeColor = Color.White;
            lblNewCharTitle.Location = new Point(208, 12);
            lblNewCharTitle.Name = "lblNewCharTitle";
            lblNewCharTitle.Size = new Size(280, 25);
            lblNewCharTitle.TabIndex = 0;
            lblNewCharTitle.Text = "New Character";
            lblNewCharTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNameLabel
            // 
            lblNameLabel.BackColor = Color.Transparent;
            lblNameLabel.ForeColor = Color.White;
            lblNameLabel.Location = new Point(208, 48);
            lblNameLabel.Name = "lblNameLabel";
            lblNameLabel.Size = new Size(63, 24);
            lblNameLabel.TabIndex = 1;
            lblNameLabel.Text = "Name :";
            lblNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNewCharName
            // 
            txtNewCharName.BackColor = Color.White;
            txtNewCharName.Font = new Font("Tahoma", 6.75F);
            txtNewCharName.ForeColor = Color.Black;
            txtNewCharName.Location = new Point(277, 53);
            txtNewCharName.MaxLength = 20;
            txtNewCharName.Name = "txtNewCharName";
            txtNewCharName.Size = new Size(133, 18);
            txtNewCharName.TabIndex = 2;
            // 
            // lblClassLabel
            // 
            lblClassLabel.BackColor = Color.Transparent;
            lblClassLabel.ForeColor = Color.White;
            lblClassLabel.Location = new Point(212, 72);
            lblClassLabel.Name = "lblClassLabel";
            lblClassLabel.Size = new Size(59, 24);
            lblClassLabel.TabIndex = 3;
            lblClassLabel.Text = "Class :";
            lblClassLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbClass
            // 
            cmbClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClass.Location = new Point(277, 74);
            cmbClass.Name = "cmbClass";
            cmbClass.Size = new Size(133, 23);
            cmbClass.TabIndex = 4;
            // 
            // lblGenderLabel
            // 
            lblGenderLabel.BackColor = Color.Transparent;
            lblGenderLabel.ForeColor = Color.White;
            lblGenderLabel.Location = new Point(208, 97);
            lblGenderLabel.Name = "lblGenderLabel";
            lblGenderLabel.Size = new Size(63, 24);
            lblGenderLabel.TabIndex = 5;
            lblGenderLabel.Text = "Gender :";
            lblGenderLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // optMale
            // 
            optMale.BackColor = Color.Transparent;
            optMale.Checked = true;
            optMale.ForeColor = Color.White;
            optMale.Location = new Point(277, 102);
            optMale.Name = "optMale";
            optMale.Size = new Size(51, 25);
            optMale.TabIndex = 6;
            optMale.TabStop = true;
            optMale.Text = "Male";
            optMale.UseVisualStyleBackColor = false;
            // 
            // optFemale
            // 
            optFemale.BackColor = Color.Transparent;
            optFemale.ForeColor = Color.White;
            optFemale.Location = new Point(334, 102);
            optFemale.Name = "optFemale";
            optFemale.Size = new Size(68, 25);
            optFemale.TabIndex = 7;
            optFemale.Text = "Female";
            optFemale.UseVisualStyleBackColor = false;
            // 
            // picPic
            // 
            picPic.BackColor = Color.Black;
            picPic.Location = new Point(426, 53);
            picPic.Name = "picPic";
            picPic.Size = new Size(48, 48);
            picPic.SizeMode = PictureBoxSizeMode.StretchImage;
            picPic.TabIndex = 8;
            picPic.TabStop = false;
            // 
            // lblAvatarPrev
            // 
            lblAvatarPrev.BackColor = Color.Transparent;
            lblAvatarPrev.ForeColor = Color.White;
            lblAvatarPrev.Location = new Point(414, 107);
            lblAvatarPrev.Name = "lblAvatarPrev";
            lblAvatarPrev.Size = new Size(38, 19);
            lblAvatarPrev.TabIndex = 9;
            lblAvatarPrev.Text = "Prev";
            // 
            // lblAvatarNext
            // 
            lblAvatarNext.BackColor = Color.Transparent;
            lblAvatarNext.ForeColor = Color.White;
            lblAvatarNext.Location = new Point(454, 105);
            lblAvatarNext.Name = "lblAvatarNext";
            lblAvatarNext.Size = new Size(34, 19);
            lblAvatarNext.TabIndex = 10;
            lblAvatarNext.Text = "Next";
            lblAvatarNext.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblNewCharCreate
            // 
            lblNewCharCreate.BackColor = Color.Transparent;
            lblNewCharCreate.ForeColor = Color.White;
            lblNewCharCreate.Location = new Point(408, 248);
            lblNewCharCreate.Name = "lblNewCharCreate";
            lblNewCharCreate.Size = new Size(80, 20);
            lblNewCharCreate.TabIndex = 11;
            lblNewCharCreate.Text = "Create";
            lblNewCharCreate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNewCharCancel
            // 
            lblNewCharCancel.BackColor = Color.Transparent;
            lblNewCharCancel.ForeColor = Color.White;
            lblNewCharCancel.Location = new Point(408, 268);
            lblNewCharCancel.Name = "lblNewCharCancel";
            lblNewCharCancel.Size = new Size(80, 20);
            lblNewCharCancel.TabIndex = 12;
            lblNewCharCancel.Text = "Cancel";
            lblNewCharCancel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSTRLabel
            // 
            lblSTRLabel.BackColor = Color.Transparent;
            lblSTRLabel.ForeColor = Color.Goldenrod;
            lblSTRLabel.Location = new Point(344, 151);
            lblSTRLabel.Name = "lblSTRLabel";
            lblSTRLabel.Size = new Size(35, 20);
            lblSTRLabel.TabIndex = 13;
            lblSTRLabel.Text = "STR :";
            lblSTRLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblHPLabel
            // 
            lblHPLabel.BackColor = Color.Transparent;
            lblHPLabel.ForeColor = Color.Goldenrod;
            lblHPLabel.Location = new Point(222, 151);
            lblHPLabel.Name = "lblHPLabel";
            lblHPLabel.Size = new Size(35, 20);
            lblHPLabel.TabIndex = 14;
            lblHPLabel.Text = "HP :";
            lblHPLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMPLabel
            // 
            lblMPLabel.BackColor = Color.Transparent;
            lblMPLabel.ForeColor = Color.Goldenrod;
            lblMPLabel.Location = new Point(222, 172);
            lblMPLabel.Name = "lblMPLabel";
            lblMPLabel.Size = new Size(35, 20);
            lblMPLabel.TabIndex = 15;
            lblMPLabel.Text = "MP :";
            lblMPLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSPLabel
            // 
            lblSPLabel.BackColor = Color.Transparent;
            lblSPLabel.ForeColor = Color.Goldenrod;
            lblSPLabel.Location = new Point(222, 193);
            lblSPLabel.Name = "lblSPLabel";
            lblSPLabel.Size = new Size(35, 20);
            lblSPLabel.TabIndex = 16;
            lblSPLabel.Text = "SP :";
            lblSPLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMAGILabel
            // 
            lblMAGILabel.BackColor = Color.Transparent;
            lblMAGILabel.ForeColor = Color.Goldenrod;
            lblMAGILabel.Location = new Point(334, 193);
            lblMAGILabel.Name = "lblMAGILabel";
            lblMAGILabel.Size = new Size(44, 20);
            lblMAGILabel.TabIndex = 17;
            lblMAGILabel.Text = "MAGI :";
            lblMAGILabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSPEEDLabel
            // 
            lblSPEEDLabel.BackColor = Color.Transparent;
            lblSPEEDLabel.ForeColor = Color.Goldenrod;
            lblSPEEDLabel.Location = new Point(323, 214);
            lblSPEEDLabel.Name = "lblSPEEDLabel";
            lblSPEEDLabel.Size = new Size(55, 20);
            lblSPEEDLabel.TabIndex = 18;
            lblSPEEDLabel.Text = "SPEED :";
            lblSPEEDLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSTR
            // 
            lblSTR.BackColor = Color.Transparent;
            lblSTR.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblSTR.ForeColor = Color.White;
            lblSTR.Location = new Point(385, 152);
            lblSTR.Name = "lblSTR";
            lblSTR.Size = new Size(35, 20);
            lblSTR.TabIndex = 19;
            lblSTR.Text = "0";
            lblSTR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHP
            // 
            lblHP.BackColor = Color.Transparent;
            lblHP.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblHP.ForeColor = Color.White;
            lblHP.Location = new Point(263, 151);
            lblHP.Name = "lblHP";
            lblHP.Size = new Size(35, 20);
            lblHP.TabIndex = 20;
            lblHP.Text = "0";
            lblHP.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMP
            // 
            lblMP.BackColor = Color.Transparent;
            lblMP.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblMP.ForeColor = Color.White;
            lblMP.Location = new Point(263, 172);
            lblMP.Name = "lblMP";
            lblMP.Size = new Size(35, 20);
            lblMP.TabIndex = 21;
            lblMP.Text = "0";
            lblMP.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSP
            // 
            lblSP.BackColor = Color.Transparent;
            lblSP.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblSP.ForeColor = Color.White;
            lblSP.Location = new Point(263, 193);
            lblSP.Name = "lblSP";
            lblSP.Size = new Size(35, 20);
            lblSP.TabIndex = 22;
            lblSP.Text = "0";
            lblSP.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMAGI
            // 
            lblMAGI.BackColor = Color.Transparent;
            lblMAGI.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblMAGI.ForeColor = Color.White;
            lblMAGI.Location = new Point(385, 194);
            lblMAGI.Name = "lblMAGI";
            lblMAGI.Size = new Size(35, 20);
            lblMAGI.TabIndex = 23;
            lblMAGI.Text = "0";
            lblMAGI.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSPEED
            // 
            lblSPEED.BackColor = Color.Transparent;
            lblSPEED.Font = new Font("Arial", 8.25F, FontStyle.Bold);
            lblSPEED.ForeColor = Color.White;
            lblSPEED.Location = new Point(385, 215);
            lblSPEED.Name = "lblSPEED";
            lblSPEED.Size = new Size(35, 20);
            lblSPEED.TabIndex = 24;
            lblSPEED.Text = "0";
            lblSPEED.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // mnuNewAccount
            // 
            mnuNewAccount.BackColor = Color.Black;
            mnuNewAccount.BackgroundImage = (Image)resources.GetObject("mnuNewAccount.BackgroundImage");
            mnuNewAccount.Controls.Add(lblNewAccountConnect);
            mnuNewAccount.Controls.Add(label1);
            mnuNewAccount.Controls.Add(txtNewAcctPassword);
            mnuNewAccount.Controls.Add(lblNewAccountTitle);
            mnuNewAccount.Controls.Add(lblNewAccountInfo);
            mnuNewAccount.Controls.Add(lblNewAccountName);
            mnuNewAccount.Controls.Add(txtNewAcctName);
            mnuNewAccount.Controls.Add(lblNewAccountEmail);
            mnuNewAccount.Controls.Add(txtNewAcctEmail);
            mnuNewAccount.Controls.Add(lblNewAccountCancel);
            mnuNewAccount.Location = new Point(0, 0);
            mnuNewAccount.Name = "mnuNewAccount";
            mnuNewAccount.Size = new Size(500, 300);
            mnuNewAccount.TabIndex = 9;
            mnuNewAccount.Visible = false;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(208, 173);
            label1.Name = "label1";
            label1.Size = new Size(85, 25);
            label1.TabIndex = 9;
            label1.Text = "Password :";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNewAcctPassword
            // 
            txtNewAcctPassword.BackColor = Color.White;
            txtNewAcctPassword.Font = new Font("Tahoma", 6.75F);
            txtNewAcctPassword.ForeColor = Color.Black;
            txtNewAcctPassword.Location = new Point(299, 177);
            txtNewAcctPassword.MaxLength = 20;
            txtNewAcctPassword.Name = "txtNewAcctPassword";
            txtNewAcctPassword.PasswordChar = '•';
            txtNewAcctPassword.Size = new Size(185, 18);
            txtNewAcctPassword.TabIndex = 8;
            // 
            // lblNewAccountTitle
            // 
            lblNewAccountTitle.BackColor = Color.Transparent;
            lblNewAccountTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblNewAccountTitle.ForeColor = Color.White;
            lblNewAccountTitle.Location = new Point(208, 12);
            lblNewAccountTitle.Name = "lblNewAccountTitle";
            lblNewAccountTitle.Size = new Size(280, 25);
            lblNewAccountTitle.TabIndex = 0;
            lblNewAccountTitle.Text = "New Account";
            lblNewAccountTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNewAccountInfo
            // 
            lblNewAccountInfo.BackColor = Color.Transparent;
            lblNewAccountInfo.ForeColor = Color.White;
            lblNewAccountInfo.Location = new Point(208, 59);
            lblNewAccountInfo.Name = "lblNewAccountInfo";
            lblNewAccountInfo.Size = new Size(280, 55);
            lblNewAccountInfo.TabIndex = 1;
            lblNewAccountInfo.Text = "Enter an account name and password.  You can name yourself whatever you want, we have no restrictions on names.";
            // 
            // lblNewAccountName
            // 
            lblNewAccountName.BackColor = Color.Transparent;
            lblNewAccountName.ForeColor = Color.White;
            lblNewAccountName.Location = new Point(208, 114);
            lblNewAccountName.Name = "lblNewAccountName";
            lblNewAccountName.Size = new Size(85, 24);
            lblNewAccountName.TabIndex = 2;
            lblNewAccountName.Text = "Name :";
            lblNewAccountName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNewAcctName
            // 
            txtNewAcctName.BackColor = Color.White;
            txtNewAcctName.Font = new Font("Tahoma", 6.75F);
            txtNewAcctName.ForeColor = Color.Black;
            txtNewAcctName.Location = new Point(299, 120);
            txtNewAcctName.MaxLength = 20;
            txtNewAcctName.Name = "txtNewAcctName";
            txtNewAcctName.Size = new Size(185, 18);
            txtNewAcctName.TabIndex = 3;
            // 
            // lblNewAccountEmail
            // 
            lblNewAccountEmail.BackColor = Color.Transparent;
            lblNewAccountEmail.ForeColor = Color.White;
            lblNewAccountEmail.Location = new Point(208, 144);
            lblNewAccountEmail.Name = "lblNewAccountEmail";
            lblNewAccountEmail.Size = new Size(85, 25);
            lblNewAccountEmail.TabIndex = 4;
            lblNewAccountEmail.Text = "Email :";
            lblNewAccountEmail.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNewAcctEmail
            // 
            txtNewAcctEmail.BackColor = Color.White;
            txtNewAcctEmail.Font = new Font("Tahoma", 6.75F);
            txtNewAcctEmail.ForeColor = Color.Black;
            txtNewAcctEmail.Location = new Point(299, 149);
            txtNewAcctEmail.MaxLength = 20;
            txtNewAcctEmail.Name = "txtNewAcctEmail";
            txtNewAcctEmail.Size = new Size(185, 18);
            txtNewAcctEmail.TabIndex = 5;
            // 
            // lblNewAccountCancel
            // 
            lblNewAccountCancel.BackColor = Color.Transparent;
            lblNewAccountCancel.ForeColor = Color.White;
            lblNewAccountCancel.Location = new Point(408, 268);
            lblNewAccountCancel.Name = "lblNewAccountCancel";
            lblNewAccountCancel.Size = new Size(80, 20);
            lblNewAccountCancel.TabIndex = 7;
            lblNewAccountCancel.Text = "Cancel";
            lblNewAccountCancel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mnuChars
            // 
            mnuChars.BackColor = Color.Black;
            mnuChars.BackgroundImage = (Image)resources.GetObject("mnuChars.BackgroundImage");
            mnuChars.Controls.Add(lstChars);
            mnuChars.Controls.Add(picSelChar);
            mnuChars.Controls.Add(lblCharsUse);
            mnuChars.Controls.Add(lblCharsNew);
            mnuChars.Controls.Add(lblCharsDelete);
            mnuChars.Controls.Add(lblCharsCancel);
            mnuChars.Controls.Add(lblCharsTitle);
            mnuChars.Location = new Point(0, 0);
            mnuChars.Name = "mnuChars";
            mnuChars.Size = new Size(500, 300);
            mnuChars.TabIndex = 10;
            mnuChars.Visible = false;
            // 
            // lstChars
            // 
            lstChars.BackColor = Color.FromArgb(64, 64, 64);
            lstChars.Font = new Font("Arial", 12F, FontStyle.Bold);
            lstChars.ForeColor = Color.Goldenrod;
            lstChars.ItemHeight = 19;
            lstChars.Location = new Point(208, 39);
            lstChars.Name = "lstChars";
            lstChars.Size = new Size(280, 99);
            lstChars.TabIndex = 0;
            // 
            // picSelChar
            // 
            picSelChar.BackColor = Color.FromArgb(64, 64, 64);
            picSelChar.BorderStyle = BorderStyle.FixedSingle;
            picSelChar.Location = new Point(324, 144);
            picSelChar.Name = "picSelChar";
            picSelChar.Size = new Size(48, 48);
            picSelChar.TabIndex = 1;
            picSelChar.TabStop = false;
            // 
            // lblCharsUse
            // 
            lblCharsUse.BackColor = Color.Transparent;
            lblCharsUse.ForeColor = Color.White;
            lblCharsUse.Location = new Point(251, 198);
            lblCharsUse.Name = "lblCharsUse";
            lblCharsUse.Size = new Size(193, 19);
            lblCharsUse.TabIndex = 2;
            lblCharsUse.Text = "Use Character";
            lblCharsUse.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCharsNew
            // 
            lblCharsNew.BackColor = Color.Transparent;
            lblCharsNew.ForeColor = Color.White;
            lblCharsNew.Location = new Point(251, 221);
            lblCharsNew.Name = "lblCharsNew";
            lblCharsNew.Size = new Size(193, 19);
            lblCharsNew.TabIndex = 3;
            lblCharsNew.Text = "New Character";
            lblCharsNew.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCharsDelete
            // 
            lblCharsDelete.BackColor = Color.Transparent;
            lblCharsDelete.ForeColor = Color.White;
            lblCharsDelete.Location = new Point(251, 245);
            lblCharsDelete.Name = "lblCharsDelete";
            lblCharsDelete.Size = new Size(193, 19);
            lblCharsDelete.TabIndex = 4;
            lblCharsDelete.Text = "Delete Character";
            lblCharsDelete.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCharsCancel
            // 
            lblCharsCancel.BackColor = Color.Transparent;
            lblCharsCancel.ForeColor = Color.White;
            lblCharsCancel.Location = new Point(408, 268);
            lblCharsCancel.Name = "lblCharsCancel";
            lblCharsCancel.Size = new Size(80, 20);
            lblCharsCancel.TabIndex = 5;
            lblCharsCancel.Text = "Cancel";
            lblCharsCancel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCharsTitle
            // 
            lblCharsTitle.BackColor = Color.Transparent;
            lblCharsTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblCharsTitle.ForeColor = Color.White;
            lblCharsTitle.Location = new Point(208, 12);
            lblCharsTitle.Name = "lblCharsTitle";
            lblCharsTitle.Size = new Size(280, 25);
            lblCharsTitle.TabIndex = 6;
            lblCharsTitle.Text = "Characters";
            lblCharsTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mnuLogin
            // 
            mnuLogin.BackColor = Color.Black;
            mnuLogin.BackgroundImage = (Image)resources.GetObject("mnuLogin.BackgroundImage");
            mnuLogin.Controls.Add(lblLoginTitle);
            mnuLogin.Controls.Add(lblLoginInfo);
            mnuLogin.Controls.Add(lblLoginName);
            mnuLogin.Controls.Add(txtLoginName);
            mnuLogin.Controls.Add(lblLoginPassword);
            mnuLogin.Controls.Add(txtLoginPassword);
            mnuLogin.Controls.Add(chkLogin);
            mnuLogin.Controls.Add(lblLoginRemember);
            mnuLogin.Controls.Add(lblLoginConnect);
            mnuLogin.Controls.Add(lblLoginCancel);
            mnuLogin.Location = new Point(0, 0);
            mnuLogin.Name = "mnuLogin";
            mnuLogin.Size = new Size(500, 300);
            mnuLogin.TabIndex = 11;
            mnuLogin.Visible = false;
            // 
            // lblLoginTitle
            // 
            lblLoginTitle.BackColor = Color.Transparent;
            lblLoginTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblLoginTitle.ForeColor = Color.White;
            lblLoginTitle.Location = new Point(208, 12);
            lblLoginTitle.Name = "lblLoginTitle";
            lblLoginTitle.Size = new Size(280, 25);
            lblLoginTitle.TabIndex = 0;
            lblLoginTitle.Text = "Login";
            lblLoginTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLoginInfo
            // 
            lblLoginInfo.BackColor = Color.Transparent;
            lblLoginInfo.ForeColor = Color.White;
            lblLoginInfo.Location = new Point(222, 59);
            lblLoginInfo.Name = "lblLoginInfo";
            lblLoginInfo.Size = new Size(222, 19);
            lblLoginInfo.TabIndex = 1;
            lblLoginInfo.Text = "Enter an account name and password.";
            // 
            // lblLoginName
            // 
            lblLoginName.BackColor = Color.Transparent;
            lblLoginName.ForeColor = Color.White;
            lblLoginName.Location = new Point(208, 97);
            lblLoginName.Name = "lblLoginName";
            lblLoginName.Size = new Size(75, 24);
            lblLoginName.TabIndex = 2;
            lblLoginName.Text = "Name :";
            lblLoginName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLoginName
            // 
            txtLoginName.BackColor = Color.White;
            txtLoginName.Font = new Font("Tahoma", 6.75F);
            txtLoginName.ForeColor = Color.Black;
            txtLoginName.Location = new Point(290, 102);
            txtLoginName.MaxLength = 20;
            txtLoginName.Name = "txtLoginName";
            txtLoginName.Size = new Size(194, 18);
            txtLoginName.TabIndex = 3;
            // 
            // lblLoginPassword
            // 
            lblLoginPassword.BackColor = Color.Transparent;
            lblLoginPassword.ForeColor = Color.White;
            lblLoginPassword.Location = new Point(208, 121);
            lblLoginPassword.Name = "lblLoginPassword";
            lblLoginPassword.Size = new Size(75, 25);
            lblLoginPassword.TabIndex = 4;
            lblLoginPassword.Text = "Password :";
            lblLoginPassword.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLoginPassword
            // 
            txtLoginPassword.BackColor = Color.White;
            txtLoginPassword.Font = new Font("Tahoma", 6.75F);
            txtLoginPassword.ForeColor = Color.Black;
            txtLoginPassword.Location = new Point(290, 126);
            txtLoginPassword.MaxLength = 20;
            txtLoginPassword.Name = "txtLoginPassword";
            txtLoginPassword.PasswordChar = '•';
            txtLoginPassword.Size = new Size(194, 18);
            txtLoginPassword.TabIndex = 5;
            // 
            // chkLogin
            // 
            chkLogin.BackColor = Color.Transparent;
            chkLogin.ForeColor = Color.Black;
            chkLogin.Location = new Point(290, 150);
            chkLogin.Name = "chkLogin";
            chkLogin.Size = new Size(20, 20);
            chkLogin.TabIndex = 6;
            chkLogin.UseVisualStyleBackColor = false;
            // 
            // lblLoginRemember
            // 
            lblLoginRemember.BackColor = Color.Transparent;
            lblLoginRemember.ForeColor = Color.White;
            lblLoginRemember.Location = new Point(308, 152);
            lblLoginRemember.Name = "lblLoginRemember";
            lblLoginRemember.Size = new Size(102, 19);
            lblLoginRemember.TabIndex = 7;
            lblLoginRemember.Text = "Remember Me";
            // 
            // lblLoginConnect
            // 
            lblLoginConnect.BackColor = Color.Transparent;
            lblLoginConnect.ForeColor = Color.White;
            lblLoginConnect.Location = new Point(299, 195);
            lblLoginConnect.Name = "lblLoginConnect";
            lblLoginConnect.Size = new Size(91, 19);
            lblLoginConnect.TabIndex = 8;
            lblLoginConnect.Text = "Connect";
            lblLoginConnect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLoginCancel
            // 
            lblLoginCancel.BackColor = Color.Transparent;
            lblLoginCancel.ForeColor = Color.White;
            lblLoginCancel.Location = new Point(408, 268);
            lblLoginCancel.Name = "lblLoginCancel";
            lblLoginCancel.Size = new Size(80, 20);
            lblLoginCancel.TabIndex = 9;
            lblLoginCancel.Text = "Cancel";
            lblLoginCancel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mnuIPConfig
            // 
            mnuIPConfig.BackColor = Color.Black;
            mnuIPConfig.BackgroundImage = (Image)resources.GetObject("mnuIPConfig.BackgroundImage");
            mnuIPConfig.Controls.Add(lblIPConfigTitle);
            mnuIPConfig.Controls.Add(lblIPLabel);
            mnuIPConfig.Controls.Add(txtIP);
            mnuIPConfig.Controls.Add(lblPortLabel);
            mnuIPConfig.Controls.Add(txtPort);
            mnuIPConfig.Controls.Add(lblIPConfigSave);
            mnuIPConfig.Controls.Add(lblIPConfigCancel);
            mnuIPConfig.Location = new Point(0, 0);
            mnuIPConfig.Name = "mnuIPConfig";
            mnuIPConfig.Size = new Size(500, 300);
            mnuIPConfig.TabIndex = 12;
            mnuIPConfig.Visible = false;
            // 
            // lblIPConfigTitle
            // 
            lblIPConfigTitle.BackColor = Color.Transparent;
            lblIPConfigTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblIPConfigTitle.ForeColor = Color.White;
            lblIPConfigTitle.Location = new Point(208, 12);
            lblIPConfigTitle.Name = "lblIPConfigTitle";
            lblIPConfigTitle.Size = new Size(280, 25);
            lblIPConfigTitle.TabIndex = 0;
            lblIPConfigTitle.Text = "IP Config";
            lblIPConfigTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblIPLabel
            // 
            lblIPLabel.BackColor = Color.Transparent;
            lblIPLabel.ForeColor = Color.White;
            lblIPLabel.Location = new Point(208, 59);
            lblIPLabel.Name = "lblIPLabel";
            lblIPLabel.Size = new Size(49, 25);
            lblIPLabel.TabIndex = 1;
            lblIPLabel.Text = "IP :";
            lblIPLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtIP
            // 
            txtIP.BackColor = Color.White;
            txtIP.Font = new Font("Tahoma", 6.75F);
            txtIP.ForeColor = Color.Black;
            txtIP.Location = new Point(263, 64);
            txtIP.MaxLength = 20;
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(205, 18);
            txtIP.TabIndex = 2;
            // 
            // lblPortLabel
            // 
            lblPortLabel.BackColor = Color.Transparent;
            lblPortLabel.ForeColor = Color.White;
            lblPortLabel.Location = new Point(208, 84);
            lblPortLabel.Name = "lblPortLabel";
            lblPortLabel.Size = new Size(49, 25);
            lblPortLabel.TabIndex = 3;
            lblPortLabel.Text = "Port :";
            lblPortLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPort
            // 
            txtPort.BackColor = Color.White;
            txtPort.Font = new Font("Tahoma", 6.75F);
            txtPort.ForeColor = Color.Black;
            txtPort.Location = new Point(263, 89);
            txtPort.MaxLength = 20;
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(91, 18);
            txtPort.TabIndex = 4;
            // 
            // lblIPConfigSave
            // 
            lblIPConfigSave.BackColor = Color.Transparent;
            lblIPConfigSave.ForeColor = Color.White;
            lblIPConfigSave.Location = new Point(299, 149);
            lblIPConfigSave.Name = "lblIPConfigSave";
            lblIPConfigSave.Size = new Size(91, 19);
            lblIPConfigSave.TabIndex = 5;
            lblIPConfigSave.Text = "Save";
            lblIPConfigSave.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblIPConfigCancel
            // 
            lblIPConfigCancel.BackColor = Color.Transparent;
            lblIPConfigCancel.ForeColor = Color.White;
            lblIPConfigCancel.Location = new Point(408, 268);
            lblIPConfigCancel.Name = "lblIPConfigCancel";
            lblIPConfigCancel.Size = new Size(80, 20);
            lblIPConfigCancel.TabIndex = 6;
            lblIPConfigCancel.Text = "Cancel";
            lblIPConfigCancel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mnuGameOptions
            // 
            mnuGameOptions.BackColor = Color.Black;
            mnuGameOptions.BackgroundImage = (Image)resources.GetObject("mnuGameOptions.BackgroundImage");
            mnuGameOptions.Controls.Add(lblGameOptionsTitle);
            mnuGameOptions.Controls.Add(chkMusic);
            mnuGameOptions.Controls.Add(lblMusicLabel);
            mnuGameOptions.Controls.Add(chkSound);
            mnuGameOptions.Controls.Add(lblSoundLabel);
            mnuGameOptions.Controls.Add(txtFonts);
            mnuGameOptions.Controls.Add(lblFontLabel);
            mnuGameOptions.Controls.Add(cmbFonts);
            mnuGameOptions.Controls.Add(lblGameOptionsSave);
            mnuGameOptions.Controls.Add(lblGameOptionsCancel);
            mnuGameOptions.Location = new Point(0, 0);
            mnuGameOptions.Name = "mnuGameOptions";
            mnuGameOptions.Size = new Size(500, 300);
            mnuGameOptions.TabIndex = 13;
            mnuGameOptions.Visible = false;
            // 
            // lblGameOptionsTitle
            // 
            lblGameOptionsTitle.BackColor = Color.Transparent;
            lblGameOptionsTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblGameOptionsTitle.ForeColor = Color.White;
            lblGameOptionsTitle.Location = new Point(208, 12);
            lblGameOptionsTitle.Name = "lblGameOptionsTitle";
            lblGameOptionsTitle.Size = new Size(280, 25);
            lblGameOptionsTitle.TabIndex = 0;
            lblGameOptionsTitle.Text = "Game Options";
            lblGameOptionsTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chkMusic
            // 
            chkMusic.BackColor = Color.Transparent;
            chkMusic.ForeColor = Color.White;
            chkMusic.Location = new Point(289, 64);
            chkMusic.Name = "chkMusic";
            chkMusic.Size = new Size(25, 25);
            chkMusic.TabIndex = 1;
            chkMusic.UseVisualStyleBackColor = false;
            // 
            // lblMusicLabel
            // 
            lblMusicLabel.BackColor = Color.Transparent;
            lblMusicLabel.ForeColor = Color.White;
            lblMusicLabel.Location = new Point(222, 62);
            lblMusicLabel.Name = "lblMusicLabel";
            lblMusicLabel.Size = new Size(61, 24);
            lblMusicLabel.TabIndex = 2;
            lblMusicLabel.Text = "Music :";
            lblMusicLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkSound
            // 
            chkSound.BackColor = Color.Transparent;
            chkSound.ForeColor = Color.White;
            chkSound.Location = new Point(289, 90);
            chkSound.Name = "chkSound";
            chkSound.Size = new Size(25, 25);
            chkSound.TabIndex = 3;
            chkSound.UseVisualStyleBackColor = false;
            // 
            // lblSoundLabel
            // 
            lblSoundLabel.BackColor = Color.Transparent;
            lblSoundLabel.ForeColor = Color.White;
            lblSoundLabel.Location = new Point(216, 89);
            lblSoundLabel.Name = "lblSoundLabel";
            lblSoundLabel.Size = new Size(67, 24);
            lblSoundLabel.TabIndex = 4;
            lblSoundLabel.Text = "Sound :";
            lblSoundLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtFonts
            // 
            txtFonts.BackColor = Color.White;
            txtFonts.Font = new Font("Tahoma", 6.75F);
            txtFonts.ForeColor = Color.Black;
            txtFonts.Location = new Point(289, 120);
            txtFonts.Name = "txtFonts";
            txtFonts.ReadOnly = true;
            txtFonts.Size = new Size(195, 18);
            txtFonts.TabIndex = 5;
            // 
            // lblFontLabel
            // 
            lblFontLabel.BackColor = Color.Transparent;
            lblFontLabel.ForeColor = Color.White;
            lblFontLabel.Location = new Point(232, 115);
            lblFontLabel.Name = "lblFontLabel";
            lblFontLabel.Size = new Size(51, 24);
            lblFontLabel.TabIndex = 6;
            lblFontLabel.Text = "Font :";
            lblFontLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbFonts
            // 
            cmbFonts.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFonts.Location = new Point(289, 144);
            cmbFonts.Name = "cmbFonts";
            cmbFonts.Size = new Size(195, 23);
            cmbFonts.Sorted = true;
            cmbFonts.TabIndex = 7;
            // 
            // lblGameOptionsSave
            // 
            lblGameOptionsSave.BackColor = Color.Transparent;
            lblGameOptionsSave.ForeColor = Color.White;
            lblGameOptionsSave.Location = new Point(311, 195);
            lblGameOptionsSave.Name = "lblGameOptionsSave";
            lblGameOptionsSave.Size = new Size(91, 19);
            lblGameOptionsSave.TabIndex = 8;
            lblGameOptionsSave.Text = "Save";
            lblGameOptionsSave.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblGameOptionsCancel
            // 
            lblGameOptionsCancel.BackColor = Color.Transparent;
            lblGameOptionsCancel.ForeColor = Color.White;
            lblGameOptionsCancel.Location = new Point(408, 268);
            lblGameOptionsCancel.Name = "lblGameOptionsCancel";
            lblGameOptionsCancel.Size = new Size(80, 20);
            lblGameOptionsCancel.TabIndex = 9;
            lblGameOptionsCancel.Text = "Cancel";
            lblGameOptionsCancel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mnuMainMenu
            // 
            mnuMainMenu.BackColor = Color.Black;
            mnuMainMenu.BackgroundImage = (Image)resources.GetObject("mnuMainMenu.BackgroundImage");
            mnuMainMenu.BackgroundImageLayout = ImageLayout.None;
            mnuMainMenu.Controls.Add(lblMainMenuT);
            mnuMainMenu.Controls.Add(lblMainMenu_NewAccount);
            mnuMainMenu.Controls.Add(lblMainMenu_Exit);
            mnuMainMenu.Controls.Add(lblMainMenu_Login);
            mnuMainMenu.Controls.Add(lblMainMenu_GameOptions);
            mnuMainMenu.Controls.Add(lblMainMenu_IPConfig);
            mnuMainMenu.Location = new Point(0, 0);
            mnuMainMenu.Name = "mnuMainMenu";
            mnuMainMenu.Size = new Size(500, 300);
            mnuMainMenu.TabIndex = 21;
            // 
            // lblMainMenuT
            // 
            lblMainMenuT.BackColor = Color.Transparent;
            lblMainMenuT.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblMainMenuT.ForeColor = Color.White;
            lblMainMenuT.Location = new Point(208, 12);
            lblMainMenuT.Name = "lblMainMenuT";
            lblMainMenuT.Size = new Size(280, 25);
            lblMainMenuT.TabIndex = 21;
            lblMainMenuT.Text = "Main Menu";
            lblMainMenuT.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMainMenu_NewAccount
            // 
            lblMainMenu_NewAccount.BackColor = Color.Transparent;
            lblMainMenu_NewAccount.ForeColor = Color.White;
            lblMainMenu_NewAccount.Location = new Point(247, 78);
            lblMainMenu_NewAccount.Name = "lblMainMenu_NewAccount";
            lblMainMenu_NewAccount.Size = new Size(205, 19);
            lblMainMenu_NewAccount.TabIndex = 22;
            lblMainMenu_NewAccount.Text = "New Account";
            lblMainMenu_NewAccount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMainMenu_Exit
            // 
            lblMainMenu_Exit.BackColor = Color.Transparent;
            lblMainMenu_Exit.ForeColor = Color.White;
            lblMainMenu_Exit.Location = new Point(247, 222);
            lblMainMenu_Exit.Name = "lblMainMenu_Exit";
            lblMainMenu_Exit.Size = new Size(205, 19);
            lblMainMenu_Exit.TabIndex = 26;
            lblMainMenu_Exit.Text = "Exit Game";
            lblMainMenu_Exit.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMainMenu_Login
            // 
            lblMainMenu_Login.BackColor = Color.Transparent;
            lblMainMenu_Login.ForeColor = Color.White;
            lblMainMenu_Login.Location = new Point(247, 114);
            lblMainMenu_Login.Name = "lblMainMenu_Login";
            lblMainMenu_Login.Size = new Size(205, 19);
            lblMainMenu_Login.TabIndex = 23;
            lblMainMenu_Login.Text = "Login";
            lblMainMenu_Login.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMainMenu_GameOptions
            // 
            lblMainMenu_GameOptions.BackColor = Color.Transparent;
            lblMainMenu_GameOptions.ForeColor = Color.White;
            lblMainMenu_GameOptions.Location = new Point(247, 186);
            lblMainMenu_GameOptions.Name = "lblMainMenu_GameOptions";
            lblMainMenu_GameOptions.Size = new Size(205, 19);
            lblMainMenu_GameOptions.TabIndex = 25;
            lblMainMenu_GameOptions.Text = "Game Options";
            lblMainMenu_GameOptions.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMainMenu_IPConfig
            // 
            lblMainMenu_IPConfig.BackColor = Color.Transparent;
            lblMainMenu_IPConfig.ForeColor = Color.White;
            lblMainMenu_IPConfig.Location = new Point(247, 150);
            lblMainMenu_IPConfig.Name = "lblMainMenu_IPConfig";
            lblMainMenu_IPConfig.Size = new Size(205, 19);
            lblMainMenu_IPConfig.TabIndex = 24;
            lblMainMenu_IPConfig.Text = "IP Config";
            lblMainMenu_IPConfig.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNewAccountConnect
            // 
            lblNewAccountConnect.BackColor = Color.Transparent;
            lblNewAccountConnect.ForeColor = Color.White;
            lblNewAccountConnect.Location = new Point(311, 217);
            lblNewAccountConnect.Name = "lblNewAccountConnect";
            lblNewAccountConnect.Size = new Size(91, 19);
            lblNewAccountConnect.TabIndex = 10;
            lblNewAccountConnect.Text = "Connect";
            lblNewAccountConnect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainMenuForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImage = Properties.Resources.menu;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(500, 300);
            Controls.Add(mnuNewAccount);
            Controls.Add(mnuGameOptions);
            Controls.Add(mnuIPConfig);
            Controls.Add(mnuLogin);
            Controls.Add(mnuChars);
            Controls.Add(mnuMainMenu);
            Controls.Add(mnuNewCharacter);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainMenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MirageSource 5";
            mnuNewCharacter.ResumeLayout(false);
            mnuNewCharacter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPic).EndInit();
            mnuNewAccount.ResumeLayout(false);
            mnuNewAccount.PerformLayout();
            mnuChars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSelChar).EndInit();
            mnuLogin.ResumeLayout(false);
            mnuLogin.PerformLayout();
            mnuIPConfig.ResumeLayout(false);
            mnuIPConfig.PerformLayout();
            mnuGameOptions.ResumeLayout(false);
            mnuGameOptions.PerformLayout();
            mnuMainMenu.ResumeLayout(false);
            ResumeLayout(false);
        }
        private Label label1;
        private TextBox txtNewAcctPassword;
        private Label lblNewAccountConnect;
    }
}