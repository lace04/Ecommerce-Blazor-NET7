using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {
        private ILocalStorageService _localStorageService;
        private ISyncLocalStorageService _syncLocalStorageService;
        private IToastService _toastService;

        public CarritoServicio(
            ILocalStorageService localStorageService,
            ISyncLocalStorageService syncLocalStorageService,
            IToastService toastService)
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;
        }

        public event Action MostrarItems;

        public async Task AgregarCarrito(CarritoDTO modelo)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito == null)
                    carrito = new List<CarritoDTO>();

                var encontrado = carrito.FirstOrDefault(c => c.Producto.IdProducto == modelo.Producto.IdProducto);
                if (encontrado != null)
                    carrito.Remove(encontrado);

                carrito.Add(modelo);
                await _localStorageService.SetItemAsync("carrito", carrito);

                if (encontrado != null)
                    _toastService.ShowSuccess("Se ha actualizado el producto en el carrito");
                else
                    _toastService.ShowSuccess("Se ha agregado el producto al carrito");

                MostrarItems?.Invoke();
            }
            catch (Exception)
            {
                _toastService.ShowError("Ha ocurrido un error al agregar el producto al carrito");
            }
        }

        public int CantidadProductos()
        {
            try
            {
                var carrito = _syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");
                return carrito == null ? 0 : carrito.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito == null)
                    carrito = new List<CarritoDTO>();

                return carrito;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito != null)
                {
                    var elemento = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                    if (elemento != null)
                    {
                        carrito.Remove(elemento);
                        await _localStorageService.SetItemAsync("carrito", carrito);
                        _toastService.ShowSuccess("Se ha eliminado el producto del carrito");
                        MostrarItems?.Invoke();
                    }
                }
            }
            catch (Exception)
            {
                _toastService.ShowError("Ha ocurrido un error al eliminar el producto del carrito");
            }
        }

        public async Task LimpiarCarrito()
        {
            try
            {
                await _localStorageService.RemoveItemAsync("carrito");
                MostrarItems?.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
