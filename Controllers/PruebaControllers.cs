using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba.Models;
using System;

using Microsoft.AspNetCore.Cors;
namespace Prueba.Controllers
{
    [EnableCors("ReglasCORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        public readonly PruebaContext _dbcontext;
        public PruebaController(PruebaContext _context)
        {
            _dbcontext = _context;
        }
        //categoria

        [HttpGet]
        [Route("listaCat")]
        public IActionResult ListaCat() {

            List<Categoria> lista = new List<Categoria>();
            try
            {
                lista = _dbcontext.Categoria.Include(c => c.Productos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }

        }



        [HttpPost]
        [Route("GuardarCat")]
        public IActionResult guardarCat([FromBody] Categoria objetoC)
        {
            if (string.IsNullOrEmpty(objetoC.Nombre))
            {
                return BadRequest("ingrese un nombre porfavor");

            }
            try
            {
                _dbcontext.Categoria.Add(objetoC);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria agregada" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }




        [HttpPut]
        [Route("EditarCat")]
        public IActionResult editarCat([FromBody] Categoria objetoC)
        {
            Categoria objCat = _dbcontext.Categoria.Find(objetoC.IdCat);
            if (objCat == null)
            {
                return BadRequest("no existe esa categoriaa");
            }

            try
            {
                objCat.Nombre = objetoC.Nombre is null ? objCat.Nombre : objetoC.Nombre;
                objCat.Descripcion = objetoC.Descripcion is null ? objCat.Descripcion : objetoC.Descripcion;

                _dbcontext.Categoria.Update(objCat);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria actualizado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("EliminarCat/{idCategoria:int}")]
        public IActionResult eliminarCat(int idCategoria)
        {
   
            Categoria objCat = _dbcontext.Categoria.Find(idCategoria);
            Categoria cate = _dbcontext.Categoria.Include(c => c.Productos).FirstOrDefault(c => c.IdCat == idCategoria);
            if (objCat == null)
            {
                return BadRequest("no existe esa categoria");
            }
            if (cate.Productos.Count>0)
            {
                return BadRequest("hay productos con esta categoria, no debe estar enlazada a ningun producto para poder eliminar");

            }  

       
             try
             {

                _dbcontext.Categoria.Remove(objCat);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "categoria eliminada" });
             }
             catch (Exception ex)
             {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
             }
          
        }




        //productos
        [HttpGet]
        [Route("listaProd")]
        public IActionResult ListaProd() {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = _dbcontext.Productos.Include(c => c.oCat).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }


        [HttpGet]
        [Route("obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto objProd = _dbcontext.Productos.Find(idProducto);
            if (objProd == null)
            {
                return BadRequest("no existe ese producto");
            }

            try
            {
                objProd = _dbcontext.Productos.Include(c => c.oCat).Where(p => p.IdProd == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = objProd });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = objProd });
            }
        }



        [HttpPost]
        [Route("GuardarProd")]
        public IActionResult guardarProd([FromBody] Producto objeto) {

            Categoria objetoC= _dbcontext.Categoria.Find(objeto.IdCat);
      
          
            if (objetoC == null)
            {
                return BadRequest("ingrese una categoria valida porfavor");

            }
            if (string.IsNullOrEmpty(objeto.Nombre))
            {
                return BadRequest("ingrese un nombre porfavor");

            }
            try
            {
                _dbcontext.Productos.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "producto agregado" });
          
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }



        [HttpPut]
        [Route("EditarProd")]
        public IActionResult editarProd([FromBody] Producto objeto)
        {
            Producto objProd = _dbcontext.Productos.Find(objeto.IdProd);
            if (objProd == null)
            {
                return BadRequest("no existe ese producto");
            }

            try
            {
                objProd.Nombre = objeto.Nombre is null ? objProd.Nombre : objeto.Nombre;
                objProd.Descripcion = objeto.Descripcion is null ? objProd.Descripcion : objeto.Descripcion;
                objProd.IdCat = objeto.IdCat is null ? objProd.IdCat : objeto.IdCat;

                _dbcontext.Productos.Update(objProd);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "producto actualizado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }




        [HttpDelete]
        [Route("EliminarProd/{idProducto:int}")]
        public IActionResult eliminarProd(int idProducto)
        {
            Producto objProd = _dbcontext.Productos.Find(idProducto);
            if (objProd == null)
            {
                return BadRequest("no existe ese producto");
            }

            try
            { 

                _dbcontext.Productos.Remove(objProd);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "producto eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
       
      
    }
}
