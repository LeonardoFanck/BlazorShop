using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public class GerenciaCarrinhoItensLocalStorageService : IGerenciaCarrinhoItensLocalStorageService
    {
        const string key = "CarrinhoItemCollection";
        private readonly ILocalStorageService _storageService;
        private readonly ICarrinhoCompraService _compraService;

        public GerenciaCarrinhoItensLocalStorageService(ILocalStorageService storageService, ICarrinhoCompraService compraService)
        {
            _storageService = storageService;
            _compraService = compraService;
        }

        public async Task<List<CarrinhoItemDto>> GetCollection()
        {
            return await _storageService.GetItemAsync<List<CarrinhoItemDto>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _storageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CarrinhoItemDto> carrinhoItemDto)
        {
            await _storageService.SetItemAsync(key, carrinhoItemDto);
        }

        // OBTEM OS DADOS DO SERVIDOR E ARMAZENA NO LOCALSTORAGE
        private async Task<List<CarrinhoItemDto>> AddCollection()
        {
            var carrinhoCompraCollection = await this._compraService.GetItens(UsuarioLogado.UsuarioId);

            if (carrinhoCompraCollection != null)
            {
                await this._storageService.SetItemAsync(key, carrinhoCompraCollection);
            }

            return carrinhoCompraCollection;
        }
    }
}
