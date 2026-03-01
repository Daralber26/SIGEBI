using SIGEBI.Contracts.Auth;
using SIGEBI.Desktop.Modules.Auth.Interfaces;

namespace SIGEBI.Desktop.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _auth;
        private readonly MainForm _main;

        public LoginForm(IAuthService auth, MainForm main)
        {
            InitializeComponent();
            _auth = auth;
            _main = main;

            btnLogin.Click += async (_, __) => await Login();
        }

        private async Task Login()
        {
            try
            {
                var request = new LoginRequest
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text
                };

                await _auth.LoginAsync(request);

                Hide();
                _main.FormClosed += (_, __) => Close();
                _main.Show();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}