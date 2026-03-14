using System.Collections.ObjectModel;
using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{

    public ObservableCollection<Produto> Produtos { get; set; }

    public ObservableCollection<Produto> ProdutosFiltrados { get; set; }

    public ListaProduto()
    {
        InitializeComponent();

        Produtos = new ObservableCollection<Produto>
        {
            new Produto { Descricao="Arroz", Quantidade=1, Preco=25.90 },
            new Produto { Descricao="Feijão", Quantidade=2, Preco=8.50 },
            new Produto { Descricao="Macarrão", Quantidade=3, Preco=4.30 },
            new Produto { Descricao="Café", Quantidade=1, Preco=14.00 },
            new Produto { Descricao="Leite", Quantidade=5, Preco=6.50 }
        };

        ProdutosFiltrados = new ObservableCollection<Produto>(Produtos);

        BindingContext = this;
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        FiltrarProdutos(e.NewTextValue);
    }

    void FiltrarProdutos(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            ProdutosFiltrados.Clear();

            foreach (var produto in Produtos)
                ProdutosFiltrados.Add(produto);

            return;
        }

        var resultado = Produtos
            .Where(p => p.Descricao.ToLower().Contains(texto.ToLower()));

        ProdutosFiltrados.Clear();

        foreach (var produto in resultado)
            ProdutosFiltrados.Add(produto);
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Opa! :(", ex.Message, "OK");
        }
    }

}
