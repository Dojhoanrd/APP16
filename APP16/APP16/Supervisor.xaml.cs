using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APP16
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Supervisor : ContentPage
    {
        bool isUserLoggedIn = false;

        public Supervisor()
        {
            InitializeComponent();
            string srvrdbname = "LOGIN_2023";
            string srvrname = "";
            string srvrusername = "JUANCITO";
            string srvrpassword = "123456";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";

            // Inicialización de la conexión a la base de datos
            sqlConnection = new SqlConnection(sqlconn);
        }

        // Método para validar la conexión a la base de datos
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open(); // Abre la conexión
                await App.Current.MainPage.DisplayAlert("Alert", "Conexión Establecida Correctamente!✔", "Ok");
                sqlConnection.Close(); // Cierra la conexión
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public class MyTableList
        {
            public int Id_usuario { get; set; }
            public string Nombre_user { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
        }

        // Objeto para manejar la conexión a la base de datos
        SqlConnection sqlConnection;

        

        private void LimpiarButton_Clicked(object sender, EventArgs e)
        {
            id_usuario.Text = string.Empty;
            nombre_user.Text = string.Empty;
            telefono.Text = string.Empty;
            email.Text = string.Empty;
            resultadoBusqueda.Text = string.Empty;

        }

        private async void getbutton_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<MyTableList> myTableLists = new List<MyTableList>(); // Lista para almacenar los datos obtenidos
                sqlConnection.Open(); // Abre la conexión

                string queryString = "Select * from dbo.usuario"; // Consulta SQL
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader(); // Ejecuta la consulta

                while (reader.Read()) // Recorre las filas obtenidas
                {
                    // Crea un objeto MyTableList y asigna sus propiedades
                    myTableLists.Add(new MyTableList
                    {
                        Id_usuario = Convert.ToInt32(reader["id_usuario"]),
                        Nombre_user = reader["nombre_user"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Email = reader["email"].ToString(),
                    });
                }

                reader.Close();
                sqlConnection.Close(); // Cierra la conexión

                MyListView.ItemsSource = myTableLists; // Asigna los datos a la ListView
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "Ok");
                throw;
            }

        }

        private async void Buscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<MyTableList> myTableLists = new List<MyTableList>(); // Lista para almacenar los datos obtenidos
                sqlConnection.Open(); // Abre la conexión

                string queryString = "Select * from dbo.usuario"; // Consulta SQL
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader(); // Ejecuta la consulta

                while (reader.Read()) // Recorre las filas obtenidas
                {
                    // Crea un objeto MyTableList y asigna sus propiedades
                    myTableLists.Add(new MyTableList
                    {
                        Id_usuario = Convert.ToInt32(reader["id_usuario"]),
                        Nombre_user = reader["nombre_user"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Email = reader["email"].ToString(),
                    });
                }

                reader.Close();
                sqlConnection.Close(); // Cierra la conexión

                MyListView.ItemsSource = myTableLists; // Asigna los datos a la ListView
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "Ok");
                throw;
            }
            SqlDataReader registro = command.ExecuteReader();

            if (registro.Read())
            {
                nombre_user.Text = registro["nombre_user"].ToString();
                telefono.Text = registro["telefono"].ToString();
                email.Text = registro["email"].ToString();
                resultadoBusqueda.Text = "Resultado de la búsqueda:";
            }
            else
            {
                resultadoBusqueda.Text = "No se encontraron resultados.";
                resultadoBusqueda.TextColor = Color.Red;
                resultadoBusqueda.HorizontalTextAlignment = TextAlignment.Center;
                resultadoBusqueda.FontAttributes = FontAttributes.Bold;
                resultadoBusqueda.FontSize = 22;
            }

            sqlConnection.Close();

        }

        private async void Salir_Clicked(object sender, EventArgs e)
        {
            if (!isUserLoggedIn)
            {
                var respuesta = await DisplayAlert("Confirmación", "¿Seguro que deseas cerrar sesión?", "Sí", "No");

                if (respuesta)
                {
                    isUserLoggedIn = false;

                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    await Navigation.PushAsync(new MainPage());
                }
            }
        }
    }

}
    
