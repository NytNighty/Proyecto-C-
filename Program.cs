using System;
using System.Collections.Generic;
using System.Linq;

namespace HeladeriaBon
{
    // ============================================
    // CLASE PRODUCTO
    // ============================================
    class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }

        public Producto(int id, string nombre, string categoria, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
        }

        public void Mostrar()
        {
            Console.WriteLine(
                $"{Id,-5} {Nombre,-25} {Categoria,-20} RD$ {Precio:N2}"
            );
        }
    }


    // ============================================
    // DETALLE DEL PEDIDO
    // ============================================
    class DetallePedido
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal()
        {
            return Producto.Precio * Cantidad;
        }
    }


    // ============================================
    // CLASE PEDIDO
    // ============================================
    class Pedido
    {
        public List<DetallePedido> Detalles { get; set; }

        public string TipoOrden { get; set; }

        public decimal CostoAdicional { get; set; }

        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }

        public void AgregarProducto(Producto producto, int cantidad)
        {
            DetallePedido detalleExistente = Detalles
                .FirstOrDefault(d => d.Producto.Id == producto.Id);

            if (detalleExistente != null)
            {
                detalleExistente.Cantidad += cantidad;
            }
            else
            {
                Detalles.Add(new DetallePedido
                {
                    Producto = producto,
                    Cantidad = cantidad
                });
            }
        }

        public decimal CalcularSubtotal()
        {
            decimal subtotal = 0;

            foreach (DetallePedido detalle in Detalles)
            {
                subtotal += detalle.Subtotal();
            }

            return subtotal;
        }

        public decimal CalcularTotal()
        {
            return CalcularSubtotal() + CostoAdicional;
        }

        public void MostrarPedido()
        {
            Console.WriteLine("\n========== PEDIDO ==========");

            foreach (DetallePedido detalle in Detalles)
            {
                Console.WriteLine(
                    $"{detalle.Producto.Nombre} x {detalle.Cantidad} = RD$ {detalle.Subtotal():N2}"
                );
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine($"Tipo de orden: {TipoOrden}");
            Console.WriteLine($"Subtotal: RD$ {CalcularSubtotal():N2}");
            Console.WriteLine($"Costo adicional: RD$ {CostoAdicional:N2}");
            Console.WriteLine($"TOTAL: RD$ {CalcularTotal():N2}");
        }
    }


    // ============================================
    // CLASE FACTURA
    // ============================================
    class Factura
    {
        public int Numero { get; set; }
        public Pedido Pedido { get; set; }
        public DateTime Fecha { get; set; }

        public Factura(int numero, Pedido pedido)
        {
            Numero = numero;
            Pedido = pedido;
            Fecha = DateTime.Now;
        }

        public void MostrarFactura()
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine("          HELADERÍA BON");
            Console.WriteLine("==================================");
            Console.WriteLine($"Factura #: {Numero}");
            Console.WriteLine($"Fecha: {Fecha}");

            foreach (DetallePedido detalle in Pedido.Detalles)
            {
                Console.WriteLine(
                    $"{detalle.Producto.Nombre} x{detalle.Cantidad} " +
                    $"RD$ {detalle.Subtotal():N2}"
                );
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Tipo de orden: {Pedido.TipoOrden}");
            Console.WriteLine($"Subtotal: RD$ {Pedido.CalcularSubtotal():N2}");
            Console.WriteLine($"Adicional: RD$ {Pedido.CostoAdicional:N2}");
            Console.WriteLine($"TOTAL: RD$ {Pedido.CalcularTotal():N2}");
            Console.WriteLine("==================================");
        }
    }


    // ============================================
    // SISTEMA PRINCIPAL
    // ============================================
    class SistemaVentas
    {
        // Diccionario de productos
        private Dictionary<int, Producto> productos;

        // Lista de facturas realizadas durante la sesión
        private List<Factura> facturas;

        private int numeroFactura = 1;

        public SistemaVentas()
        {
            productos = new Dictionary<int, Producto>();
            facturas = new List<Factura>();

            CargarProductos();
        }


        // ============================================
        // CARGAR PRODUCTOS
        // ============================================
        private void CargarProductos()
       
{
    // HELADOS
    productos.Add(1,
        new Producto(1, "Chocolate", "Helado", 100));

    productos.Add(2,
        new Producto(2, "Vainilla", "Helado", 100));

    productos.Add(3,
        new Producto(3, "Fresa", "Helado", 100));

    // MILKSHAKES
    productos.Add(4,
        new Producto(4, "Chocolate", "Milkshake", 180));

    productos.Add(5,
        new Producto(5, "Fresa", "Milkshake", 180));

    productos.Add(6,
        new Producto(6, "Vainilla", "Milkshake", 180));

    // POSTRES FRÍOS
    productos.Add(7,
        new Producto(7, "Cheesecake frío", "Postre frío", 200));

    productos.Add(8,
        new Producto(8, "Tres leches frío", "Postre frío", 220));

    productos.Add(9,
        new Producto(9, "Brownie con helado", "Postre frío", 250));
}


        // ============================================
        // MOSTRAR PRODUCTOS
        // ============================================
        private void MostrarProductosPorCategoria(string categoria)
{
    Console.WriteLine($"\n========== {categoria.ToUpper()} ==========");

    foreach (Producto producto in productos.Values)
    {
        if (producto.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
        {
            producto.Mostrar();
        }
    }

    Console.WriteLine("================================");
}

        private void VerProductos()
{
        bool salir = false;

        while (!salir)
            
        {
            Console.Clear();

            Console.WriteLine("================================");
        Console.WriteLine("         VER PRODUCTOS");
        Console.WriteLine("================================");
        Console.WriteLine("1. Helados");
        Console.WriteLine("2. Milkshakes");
        Console.WriteLine("3. Postres fríos");
        Console.WriteLine("4. Volver");
        Console.WriteLine("================================");

        Console.Write("Seleccione una categoría: ");
        int opcion = LeerEntero();

        switch (opcion)
        {
            case 1:
                Console.Clear();
                MostrarProductosPorCategoria("Helado");
                Pausar();
                break;

            case 2:
                Console.Clear();
                MostrarProductosPorCategoria("Milkshake");
                Pausar();
                break;

            case 3:
                Console.Clear();
                MostrarProductosPorCategoria("Postre frío");
                Pausar();
                break;

            case 4:
                salir = true;
                break;

            default:
                Console.WriteLine("\nOpción inválida.");
                Pausar();
                break;
        }
    }
}
        // ============================================
        // CREAR PEDIDO
        // ============================================
        private void CrearPedido()
{
    Pedido pedido = new Pedido();

    bool continuarPedido = true;

    while (continuarPedido)
    {
        Console.Clear();

        Console.WriteLine("================================");
        Console.WriteLine("          NUEVO PEDIDO");
        Console.WriteLine("================================");
        Console.WriteLine("1. Helados");
        Console.WriteLine("2. Milkshakes");
        Console.WriteLine("3. Postres fríos");
        Console.WriteLine("4. Finalizar pedido");
        Console.WriteLine("================================");

        Console.Write("Seleccione una categoría: ");
        int categoria = LeerEntero();

        string categoriaSeleccionada = "";

        switch (categoria)
        {
            case 1:
                categoriaSeleccionada = "Helado";
                break;

            case 2:
                categoriaSeleccionada = "Milkshake";
                break;

            case 3:
                categoriaSeleccionada = "Postre frío";
                break;

            case 4:
                continuarPedido = false;
                continue;

            default:
                Console.WriteLine("Categoría inválida.");
                Pausar();
                continue;
        }


        // ==========================================
        // SEGUNDO CICLO
        // PRODUCTOS DE LA CATEGORÍA
        // ==========================================

        bool continuarCategoria = true;

        while (continuarCategoria)
        {
            Console.Clear();

            MostrarProductosPorCategoria(categoriaSeleccionada);

            Console.WriteLine("\n0. Volver a categorías");

            Console.Write("\nSeleccione el producto: ");
            int id = LeerEntero();

            if (id == 0)
            {
                continuarCategoria = false;
                continue;
            }

            if (!productos.ContainsKey(id))
            {
                Console.WriteLine("Producto no encontrado.");
                Pausar();
                continue;
            }

            Producto producto = productos[id];

            // Verificar que el producto pertenece
            // a la categoría seleccionada
            if (!producto.Categoria.Equals(categoriaSeleccionada,StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "Ese producto no pertenece a la categoría seleccionada."
                );

                Pausar();
                continue;
            }

            Console.Write($"Cantidad de {producto.Nombre}: ");
            int cantidad = LeerEntero();

            if (cantidad <= 0)
            {
                Console.WriteLine("La cantidad debe ser mayor que 0.");
                Pausar();
                continue;
            }

            pedido.AgregarProducto(producto, cantidad);

            Console.WriteLine(
                $"\nSe agregaron {cantidad} x {producto.Nombre}."
            );

            Console.Write("\n¿Desea agregar otro producto de esta categoría? (S/N): ");
            string respuesta = Console.ReadLine().ToUpper();

            if (respuesta != "S")
            {
                continuarCategoria = false;
            }
        }
    }

    // Si no se agregó ningún producto
    if (pedido.Detalles.Count == 0)
    {
        Console.WriteLine("\nNo se agregaron productos.");
        Pausar();
        return;
    }

    // ==========================================
    // TIPO DE ORDEN
    // ==========================================

    SeleccionarTipoOrden(pedido);

    pedido.MostrarPedido();

    Console.Write("\n¿Confirmar pedido? (S/N): ");
    string confirmar = Console.ReadLine().ToUpper();

    if (confirmar == "S")
    {
        Factura factura = new Factura(numeroFactura, pedido);

        facturas.Add(factura);

        numeroFactura++;

        Console.Clear();

        factura.MostrarFactura();

        Console.WriteLine("\nVenta registrada correctamente.");
    }
    else
    {
        Console.WriteLine("\nPedido cancelado.");
    }

    Pausar();
}


        // ============================================
        // TIPO DE ORDEN
        // ============================================
        private void SeleccionarTipoOrden(Pedido pedido)
        {
            Console.WriteLine("\n========== TIPO DE ORDEN ==========");
            Console.WriteLine("1. Consumo en local");
            Console.WriteLine("2. Para llevar");

            Console.Write("Seleccione una opción: ");
            int opcion = LeerEntero();

            switch (opcion)
            {
                case 1:
                    pedido.TipoOrden = "Consumo en local";
                    pedido.CostoAdicional = 0;
                    break;

                case 2:
                    pedido.TipoOrden = "Para llevar";
                    pedido.CostoAdicional = 50;
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    SeleccionarTipoOrden(pedido);
                    break;
            }
        }


        // ============================================
        // MENÚ ADMINISTRADOR
        // ============================================
        private void MenuAdministrador()
        {
            Console.Clear();

            Console.WriteLine("========== ADMINISTRADOR ==========");

            Console.Write("Ingrese la contraseña: ");
            string password = Console.ReadLine();

            if (password != "1234")
            {
                Console.WriteLine("\nContraseña incorrecta.");
                Pausar();
                return;
            }

            bool salir = false;

            while (!salir)
            {
                Console.Clear();

                Console.WriteLine("========== REPORTES ==========");
                Console.WriteLine("1. Total de ingresos");
                Console.WriteLine("2. Cantidad de facturas");
                Console.WriteLine("3. Ver facturas");
                Console.WriteLine("4. Volver");
                Console.Write("\nSeleccione una opción: ");

                int opcion = LeerEntero();

                switch (opcion)
                {
                    case 1:
                        MostrarIngresos();
                        break;

                    case 2:
                        MostrarCantidadFacturas();
                        break;

                    case 3:
                        MostrarFacturas();
                        break;

                    case 4:
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        Pausar();
                        break;
                }
            }
        }


        // ============================================
        // REPORTE DE INGRESOS
        // ============================================
        private void MostrarIngresos()
        {
            decimal totalIngresos = 0;

            foreach (Factura factura in facturas)
            {
                totalIngresos += factura.Pedido.CalcularTotal();
            }

            Console.WriteLine("\n========== INGRESOS ==========");
            Console.WriteLine($"Total acumulado: RD$ {totalIngresos:N2}");

            Pausar();
        }


        // ============================================
        // CANTIDAD DE FACTURAS
        // ============================================
        private void MostrarCantidadFacturas()
        {
            Console.WriteLine("\n========== FACTURAS ==========");
            Console.WriteLine($"Facturas emitidas: {facturas.Count}");

            Pausar();
        }


        // ============================================
        // MOSTRAR TODAS LAS FACTURAS
        // ============================================
        private void MostrarFacturas()
        {
            Console.WriteLine("\n========== FACTURAS EMITIDAS ==========");

            if (facturas.Count == 0)
            {
                Console.WriteLine("No existen facturas durante esta sesión.");
            }
            else
            {
                foreach (Factura factura in facturas)
                {
                    factura.MostrarFactura();
                }
            }

            Pausar();
        }


        // ============================================
        // MENÚ PRINCIPAL
        // ============================================
        public void Ejecutar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();

                Console.WriteLine("=======================================");
                Console.WriteLine("          HELADERÍA BON");
                Console.WriteLine("      SISTEMA DE VENTAS");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Nueva venta");
                Console.WriteLine("2. Ver productos");
                Console.WriteLine("3. Administrador / Reportes");
                Console.WriteLine("4. Salir");
                Console.WriteLine("=======================================");

                Console.Write("Seleccione una opción: ");

                int opcion = LeerEntero();

                switch (opcion)
                {
                    case 1:
                        CrearPedido();
                        break;

                    case 2:
                        VerProductos();
                        break;

                    case 3:
                        MenuAdministrador();
                        break;

                    case 4:
                        salir = true;
                        Console.WriteLine("\nGracias por utilizar el sistema.");
                        break;

                    default:
                        Console.WriteLine("\nOpción inválida.");
                        Pausar();
                        break;
                }
            }
        }


        // ============================================
        // LEER ENTERO
        // ============================================
        private int LeerEntero()
        {
            int numero;

            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.Write("Ingrese un número válido: ");
            }

            return numero;
        }


        // ============================================
        // PAUSAR
        // ============================================
        private void Pausar()
        {
            Console.WriteLine("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }
    }


    // ============================================
    // PROGRAM
    // ============================================
    class Program
    {
        static void Main(string[] args)
        {
            SistemaVentas sistema = new SistemaVentas();

            sistema.Ejecutar();
        }
    }
}

       
       