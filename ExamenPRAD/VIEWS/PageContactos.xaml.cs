using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenPRAD.MODELS;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenPRAD.VIEWS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContactos : ContentPage
    {
        public PageContactos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    txtlatitud.Text = Convert.ToString(location.Latitude);
                    txtlongitud.Text = Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
            }

        }

        public async void SalvarContactos_Clicked(object sender, EventArgs e)
        {
            if (validaciondatos())
            {
                Contactos contactoModels = new Contactos
                {
                    nombres = txtnombre.Text,
                    apellidos = txtapellidos.Text,
                    edad = Convert.ToDouble(txtedad.Text),
                    pais = cbpais.SelectedItem.ToString(),
                    nota = txtnota.Text,
                    latitud = Convert.ToDouble(txtlatitud.Text),
                    longitud = Convert.ToDouble(txtlongitud.Text),



                };
                await App.sqlitedb.SaveContact(contactoModels);
                txtnombre.Text = "";
                txtapellidos.Text = "";
                txtedad.Text = "";
               
                txtnota.Text = "";
                await DisplayAlert("Mensaje", "Su Registro se guardo correctamente", "De Acuerdo");
                Datos();

            }
            else
            {
                await DisplayAlert("Mensaje", "Ingrese todos los datos", "De Acuerdo");
            }
        }

        public async void Datos()
        {
            var contactolist = await App.sqlitedb.GetContactoAsync();
            if (contactolist != null)
            {
                listcontactos.ItemsSource = contactolist;
            }
        }
        public bool validaciondatos()
        {
            bool res;
            if (string.IsNullOrEmpty(txtnombre.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtapellidos.Text))
            {
                res = false;
            }
            else if (string.IsNullOrEmpty(txtedad.Text))
            {
                res = false;
            }
            //else if (string.IsNullOrEmpty(txtcorreo.Text))
            //{
            //    res = false;
            //}
            else if (string.IsNullOrEmpty(txtnota.Text))
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public async void Actualizar_Clicked(object sender, EventArgs e)
        {
            Contactos contactos = new Contactos()
            {

                nombres = txtnombre.Text,
                apellidos = txtapellidos.Text,
                edad = Convert.ToDouble(txtedad.Text),
                pais = cbpais.SelectedItem.ToString(),
                nota = txtnota.Text,
                latitud=Convert.ToDouble( txtlatitud.Text),
                longitud= Convert.ToDouble (txtlongitud.Text),
            };
            await App.sqlitedb.SaveContact(contactos);
            await DisplayAlert("Registro de Contacto", "Sus datos se actualizaron Correctamente", "De Acuerdo");


            txtnombre.Text = "";
            txtapellidos.Text = "";
            txtedad.Text = "";
           
            txtnota.Text = "";

            txtid.IsVisible = false;
            Actualizar.IsVisible = false;
            SalvarContactos.IsVisible = true;
            Datos();

        }

       

        public async void listcontactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var relist = (Contactos)e.SelectedItem;
            SalvarContactos.IsVisible = false;
            txtid.IsVisible = true;
            Actualizar.IsVisible = true;
            Eliminar.IsVisible = true;


            if (!string.IsNullOrEmpty(relist.id.ToString()))
            {
                var contacto = await App.sqlitedb.GetPersonaByIdAsync(relist.id);
                if (contacto != null)
                {
                    txtid.Text = contacto.id.ToString();
                    txtnombre.Text = contacto.nombres;
                    txtapellidos.Text = contacto.apellidos;
                    txtedad.Text = contacto.edad.ToString();
                    //cbpais.TextTransform = contacto.pais.ToString();
                    txtnota.Text = contacto.nota;
                   

                }
            }
        }
        public async void Eliminar_Clicked(object sender, EventArgs e)
        {
            var contacto = await App.sqlitedb.GetPersonaByIdAsync(Convert.ToInt32(txtid.Text));
            if (contacto != null)
            {
                await App.sqlitedb.deletecontactosAsync(contacto);
                await DisplayAlert("Mensaje", "El registro de contacto fue eliminado correctamente", "De acuerdo");
                Datos();
                txtid.IsVisible = false;
                Actualizar.IsVisible = false;
                Eliminar.IsVisible = false;
                SalvarContactos.IsVisible = true;

            }
        }

       
    }
}