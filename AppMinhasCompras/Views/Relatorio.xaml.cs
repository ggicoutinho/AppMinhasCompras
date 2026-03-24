using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views;

public partial class Relatorio : ContentPage
{
    public Relatorio()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        try
        {
            var produtos = await App.Db.GetAll();

            var relatorio = produtos
                .GroupBy(p => p.Categoria)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.Total)
                })
                .ToList();

            lst_relatorio.ItemsSource = relatorio;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}