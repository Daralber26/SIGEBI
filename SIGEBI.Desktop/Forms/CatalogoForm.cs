using SIGEBI.Contracts.Resources;
using SIGEBI.Desktop.Modules.Catalogo.Interfaces;

namespace SIGEBI.Desktop.Forms
{
    public partial class CatalogoForm : Form
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoForm(ICatalogoService catalogoService)
        {
            InitializeComponent();
            _catalogoService = catalogoService;

            ConfigurarGrid();

            this.Load += CatalogoForm_Load;
        }

        private void ConfigurarGrid()
        {
            dgvCatalogo.AutoGenerateColumns = false;
            dgvCatalogo.Columns.Clear();

            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTitulo",
                HeaderText = "Título",
                DataPropertyName = "Titulo"
            });

            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAutor",
                HeaderText = "Autor",
                DataPropertyName = "Autor"
            });

            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIsbn",
                HeaderText = "ISBN",
                DataPropertyName = "Isbn"
            });

            dgvCatalogo.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDisponibles",
                HeaderText = "Disponibles",
                DataPropertyName = "CopiasDisponibles"
            });

            dgvCatalogo.ReadOnly = true;
            dgvCatalogo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCatalogo.MultiSelect = false;
        }

        private async void CatalogoForm_Load(object? sender, EventArgs e)
        {
            try
            {
                var items = await _catalogoService.ListarAsync();
                dgvCatalogo.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}