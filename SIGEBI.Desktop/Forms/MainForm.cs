using Microsoft.Extensions.DependencyInjection;

namespace SIGEBI.Desktop.Forms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _sp;

        public MainForm(IServiceProvider sp)
        {
            InitializeComponent();
            _sp = sp;
        }

        private void btnCatalogo_Click(object sender, EventArgs e)
        {
            var form = _sp.GetRequiredService<CatalogoForm>();
            form.ShowDialog();
        }
    }
}