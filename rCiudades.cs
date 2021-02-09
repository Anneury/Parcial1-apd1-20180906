using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parcial1_apd1_20180906.BLL;
using Parcial1_apd1_20180906.Dal;
using Parcial1_apd1_20180906.Entidades;

namespace Parcial1_apd1_20180906
{
    public partial class rCiudades : Form
    {
        public rCiudades()
        {
            InitializeComponent();
        }

        public bool Validar()
        {
            bool paso = true;

            if(NombreTextBox.Text == string.Empty)
            {
                ErrorProvider.SetError(NombreTextBox, "No puedes dejar este campo vacio!");
                NombreTextBox.Focus();
                paso = false;
            }
            if (CiudadBLL.ExisteCiudad(NombreTextBox.Text))
            {
                ErrorProvider.SetError(NombreTextBox, "Esta ciudad ya existe en la base de Datos.");
                NombreTextBox.Focus();
                paso = false;
            }

            return paso;
        }
        public void Limpiar()
        {
            NombreTextBox.Clear();
            CiudadIdNumericUpDown.Value = 0;
            ErrorProvider.Clear();
        }

        private Ciudad LlenaClase()
        {
            Ciudad ciudades = new Ciudad();
            ciudades.CiudadId = Convert.ToInt32(CiudadIdNumericUpDown.Value);
            ciudades.Nombre = NombreTextBox.Text;

            return ciudades;
        }

        private void LlenaCampo(Ciudad ciudades)
        {
            CiudadIdNumericUpDown.Value = ciudades.CiudadId;
            NombreTextBox.Text = ciudades.Nombre;
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Ciudad ciudades = CiudadBLL.Buscar((int)CiudadIdNumericUpDown.Value);

            return (ciudades != null);
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Ciudad ciudades;
            bool paso = false;

            if (!Validar())
                return;

            ciudades = LlenaClase();

            if (CiudadIdNumericUpDown.Value == 0)
                paso = CiudadBLL.Guardar(ciudades);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se ha podido modificar, Ciudad inexistente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                paso = CiudadBLL.Modificar(ciudades);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado Correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo guardar...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(CiudadIdNumericUpDown.Text, out id);

            Limpiar();

            if (CiudadBLL.Existe(id))
            {
                if (MessageBox.Show("Deseas Eliminar esta ciudad?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (CiudadBLL.Eliminar(id))
                        MessageBox.Show("Ciudad eliminada con exito!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("No se pudo eliminar dicha Ciudad...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    return;
            }
            else
            {
                MessageBox.Show("Esta ciudad no existe, no puedes eliminarla!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
               
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            Ciudad ciudades = new Ciudad();
            int.TryParse(CiudadIdNumericUpDown.Text, out id);

            Limpiar();

            ciudades = CiudadBLL.Buscar(id);

            if(ciudades != null)
            {
                LlenaCampo(ciudades);
                MessageBox.Show("Ciudad encontrada!","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);                
            }
            else
            {
                MessageBox.Show("Ciudad no encontrada!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
